using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WowPacketParser.SQL
{
    public class SQLStore : IDisposable
    {
        private StreamWriter _file;

        private readonly List<string> _sqls = new List<string>();

        public SQLStore(string file)
        {
            File.Delete(file);
            _file = new StreamWriter(file, true);
        }

        ~SQLStore()
        {
            Dispose(false);
        }

        public void WriteData(string sql)
        {
            if (_file == null)
                return;

            if (_sqls.BinarySearch(sql) > -1)
                return;

            _sqls.Add(sql);
        }

        public bool WriteToFile()
        {
            if (_file == null)
                return false;

            var empty = _sqls.All(String.IsNullOrWhiteSpace);

            if (empty)
                return false;

            foreach (var sql in _sqls)
                _file.WriteLine(sql);

            return true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
                _sqls.Clear();

            if (_file != null)
            {
                _file.Flush();
                _file.Close();
                _file = null;
            }
        }
    }
}
