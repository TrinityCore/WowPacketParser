using System;
using System.Collections.Generic;
using System.Configuration;
using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Hotfix
{
    public class HotfixSettings
    {
        public static HotfixSettings Instance { get; } = new HotfixSettings();

        private HotfixSection Section => (HotfixSection)ConfigurationManager.GetSection("Hotfix");
        private IReadOnlySet<string> _hashes;

        public void LoadHashes()
        {
            var enabledTables = new HashSet<string>();

            foreach (HotfixElement sectionFileHash in Section.FileHashes)
                if (sectionFileHash.Enabled)
                    enabledTables.Add(sectionFileHash.FileHash);

            _hashes = enabledTables;
        }

        public bool ShouldLog(DB2Hash fileHash)
        {
            if (Settings.ParseAllHotfixes)
                return true;

            return _hashes.Contains(fileHash.ToString());
        }

        public bool ShouldLog()
        {
            if (Settings.ParseAllHotfixes)
                return true;

            return _hashes.Count > 0;
        }
    }

    public class HotfixSection : ConfigurationSection
    {
        [ConfigurationProperty("FileHashes")]
        public HotfixElementCollection FileHashes
        {
            get { return (HotfixElementCollection)base["FileHashes"]; }
            set { base["FileHashes"] = value; }
        }
    }

    [ConfigurationCollection(typeof(HotfixElement))]
    public class HotfixElementCollection : ConfigurationElementCollection
    {
        private const string PropertyName = "HotfixElement";

        public override ConfigurationElementCollectionType CollectionType => ConfigurationElementCollectionType.BasicMapAlternate;
        protected override string ElementName => PropertyName;

        protected override bool IsElementName(string elementName) => elementName.Equals(PropertyName, StringComparison.InvariantCultureIgnoreCase);

        public override bool IsReadOnly() => false;

        protected override ConfigurationElement CreateNewElement()
        {
            return new HotfixElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((HotfixElement)element).FileHash;
        }

        public HotfixElement this[int index] => (HotfixElement) BaseGet(index);
    }

    public class HotfixElement : ConfigurationElement
    {
        [ConfigurationProperty("FileHash", DefaultValue = "", IsKey = true, IsRequired = true)]
        public string FileHash
        {
            get { return (string) base["FileHash"]; }
            set { base["FileHash"] = value; }
        }

        [ConfigurationProperty("Enabled", DefaultValue = "true", IsKey = true, IsRequired = false)]
        public bool Enabled
        {
            get { return (bool) base["Enabled"]; }
            set { base["Enabled"] = value; }
        }
    }
}
