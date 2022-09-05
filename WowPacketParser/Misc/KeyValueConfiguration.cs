using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WowPacketParser.Misc
{
    internal class KeyValueConfiguration : IConfiguration
    {
        private KeyValueConfigurationCollection _collection;

        public KeyValueConfiguration(KeyValueConfigurationCollection collection)
        {
            _collection = collection;
        }
        public string this[string key] { 
            get => _collection[key].Value;
            set => _collection.Add(key, value); 
        }

        public IEnumerable<IConfigurationSection> GetChildren()
        {
            throw new NotImplementedException();
        }

        public IChangeToken GetReloadToken()
        {
            throw new NotImplementedException();
        }

        public IConfigurationSection GetSection(string key)
        {
            throw new NotImplementedException();
        }
    }
}
