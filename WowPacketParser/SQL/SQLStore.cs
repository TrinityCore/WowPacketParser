using System;
using System.Collections.Generic;
using System.IO;

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

        public void WriteToFile()
        {
            if (_file == null)
                return;

            foreach (var sql in _sqls)
                _file.WriteLine(sql);
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
