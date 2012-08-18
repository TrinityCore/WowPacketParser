using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace PacketParser.Misc
{
    // this class stores packet data in a file, provides read/write access for blocks of data
    public class CacheFileManager<BlockType> : IDisposable where BlockType : class 
    {
        protected List<long> _dataBlocksFileOffset = new List<long>();
        protected List<BlockType> _dataBlocks = new List<BlockType>();
        protected List<bool> _dataBlocksCached = new List<bool>();

        protected FileStream _file;
        protected BinaryFormatter bFormatter = new BinaryFormatter();
        protected string _fileName;

        protected int _dataUpdateFirstBlockIndex = -1;
        protected long _dataBlockUpdateFirstBlockOffset = 0;

        public CacheFileManager()
        {
            _fileName = Path.GetTempFileName();
            _file = new FileStream(_fileName, FileMode.OpenOrCreate);
        }

        public bool BlockExists(int blockIndex)
        {
            if (GetBlocksCount() <= blockIndex)
                return false;
            return _dataBlocksFileOffset[blockIndex] != -1 || (IsDuringBlocksUpdate() && blockIndex >= _dataUpdateFirstBlockIndex && _dataBlocksCached[blockIndex]);
        }

        protected void EnsureBlockExists(int blockIndex)
        {
            for (int i = GetBlocksCount(); i <= blockIndex; ++i)
            {
                _dataBlocksFileOffset.Add(-1);
                _dataBlocks.Add(null);
                _dataBlocksCached.Add(false);
            }
        }

        public BlockType GetBlock(int blockIndex)
        {
            if (!BlockExists(blockIndex))
                return null;
            CacheBlock(blockIndex);
            return _dataBlocks[blockIndex];
        }

        public int GetBlocksCount()
        {
            return _dataBlocksFileOffset.Count;
        }

        public void BeginBlocksUpdate(int startingBlock)
        {
            if (IsDuringBlocksUpdate())
                throw new Exception("Incorrect BeginBlocksUpdate() function use");
            for (int i = startingBlock; i < GetBlocksCount(); ++i)
                CacheBlock(i);
            _dataUpdateFirstBlockIndex = startingBlock;
            EnsureBlockExists(startingBlock);
            _dataBlockUpdateFirstBlockOffset = _dataBlocksFileOffset[startingBlock];
            // check if block wasn't used before - need to set position in that case
            if (_dataBlockUpdateFirstBlockOffset == -1)
                _dataBlockUpdateFirstBlockOffset = _file.Seek(0, SeekOrigin.End);
        }

        public void EndBlocksUpdate()
        {
            if (!IsDuringBlocksUpdate())
                throw new Exception("Incorrect EndBlocksUpdate() function use");
            _file.Seek(_dataBlockUpdateFirstBlockOffset, SeekOrigin.Begin);
            for (int i = _dataUpdateFirstBlockIndex; i < GetBlocksCount(); ++i)
            {
                if (_dataBlocks[i] == null)
                    continue;
                _dataBlocksFileOffset[i] = _file.Position;
                bFormatter.Serialize(_file, _dataBlocks[i]);
            }
            _dataUpdateFirstBlockIndex = -1;
        }

        public bool IsDuringBlocksUpdate()
        {
            return _dataUpdateFirstBlockIndex != -1;
        }

        public void AddBlock(BlockType block)
        {
            var blockIndex = GetBlocksCount();
            EnsureBlockExists(blockIndex);
            _dataBlocks[blockIndex] = block;
            if (!IsDuringBlocksUpdate())
            {
                _file.Seek(0, SeekOrigin.End);
                _dataBlocksFileOffset[blockIndex] = _file.Position;
                bFormatter.Serialize(_file, block);
            }
            _dataBlocksCached[blockIndex] = true;
        }

        public void ChangeBlock(int blockIndex, BlockType block)
        {
            if (!IsDuringBlocksUpdate() || blockIndex < _dataUpdateFirstBlockIndex)
                throw new Exception("Can't change block when IsDuringBlocksUpdate() isn't set for block!");
            EnsureBlockExists(blockIndex);
            _dataBlocks[blockIndex] = block;
            _dataBlocksCached[blockIndex] = true;
        }

        public void CacheBlock(int blockIndex)
        {
            if (!BlockExists(blockIndex))
                return;
            if (_dataBlocksCached[blockIndex])
                return;
            if (IsDuringBlocksUpdate() && blockIndex >= _dataUpdateFirstBlockIndex)
                throw new Exception("Can't cache block during block update!");
            _dataBlocksCached[blockIndex] = true;
            _file.Seek(_dataBlocksFileOffset[blockIndex], SeekOrigin.Begin);
            var block = (BlockType)bFormatter.Deserialize(_file);
            _dataBlocks[blockIndex] = block;
        }

        public void UnCacheBlock(int blockIndex)
        {
            if (!BlockExists(blockIndex))
                return;
            if (!_dataBlocksCached[blockIndex])
                return;
            _dataBlocksCached[blockIndex] = false;
            _dataBlocks[blockIndex] = null;
        }

        public void CacheAllBlocks()
        {
            var count = GetBlocksCount();
            for (int i = 0; i < count; ++i)
                CacheBlock(i);
        }

        public void UnCacheAllBlocks()
        {
            var count = GetBlocksCount();
            for (int i = 0; i < count; ++i)
                UnCacheBlock(i);
        }

        public void UnCacheBlocksAfter(int firstBlockId)
        {
            var count = GetBlocksCount();
            for (int i = firstBlockId; i < count; ++i)
                UnCacheBlock(i);
        }

        public void Dispose()
        {
            _file.Close();
            _file.Dispose();
            File.Delete(_fileName);
        }
    }
}
