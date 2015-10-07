using System;
using System.Text;
using WowPacketParser.Enums;
using WowPacketParser.Loading;
using WowPacketParser.Misc;

namespace WowPacketParser.SQL
{
    /// <summary>
    /// Field/column name
    /// Can be used with basic types and enums or
    ///  arrays ("dmg_min1, dmg_min2, dmg_min3" -> name = dmg_min, count = 3, startAtZero = false)
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    sealed public class DBFieldNameAttribute : Attribute
    {
        /// <summary>
        /// Column name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// True if field is a primary key
        /// </summary>
        public bool IsPrimaryKey { get; set; }

        /// <summary>
        /// Number of fields
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// True if counting should include 0
        /// </summary>
        public readonly bool StartAtZero;

        /// <summary>
        /// True if cctor that accepts multiple fields was used (name + count)
        /// </summary>
        private readonly bool _multipleFields;

        /// <summary>
        /// matches any version
        /// </summary>
        /// <param name="name">database field name</param>
        /// <param name="isPrimaryKey">true if field is a primary key</param>
        public DBFieldNameAttribute(string name, bool isPrimaryKey = false)
        {
            Name = name;
            IsPrimaryKey = isPrimaryKey;
            Count = 1;
        }

        /// <summary>
        /// [addedInVersion, +inf[
        /// </summary>
        /// <param name="name">database field name</param>
        /// <param name="locale">initial locale</param>
        /// <param name="isPrimaryKey">true if field is a primary key</param>
        public DBFieldNameAttribute(string name, LocaleConstant locale, bool isPrimaryKey = false)
        {
            if (BinaryPacketReader.GetLocale() == locale)
            {
                Name = name;
                IsPrimaryKey = isPrimaryKey;
                Count = 1;
            }
            else
            {
                Name = null;
                IsPrimaryKey = false;
                Count = 0;
            }
        }

        /// <summary>
        /// [addedInVersion, +inf[
        /// </summary>
        /// <param name="name">database field name</param>
        /// <param name="addedInVersion">initial version</param>
        /// <param name="isPrimaryKey">true if field is a primary key</param>
        public DBFieldNameAttribute(string name, ClientVersionBuild addedInVersion, bool isPrimaryKey = false)
        {
            if (ClientVersion.AddedInVersion(addedInVersion))
            {
                Name = name;
                IsPrimaryKey = isPrimaryKey;
                Count = 1;
            }
            else
            {
                Name = null;
                IsPrimaryKey = false;
                Count = 0;
            }
        }

        /// <summary>
        /// [addedInVersion, +inf[
        /// </summary>
        /// <param name="name">database field name</param>
        /// <param name="addedInVersion">initial version</param>
        /// <param name="locale">initial locale</param>
        /// <param name="isPrimaryKey">true if field is a primary key</param>
        public DBFieldNameAttribute(string name, ClientVersionBuild addedInVersion, LocaleConstant locale, bool isPrimaryKey = false)
        {
            if (ClientVersion.AddedInVersion(addedInVersion) && BinaryPacketReader.GetLocale() == locale)
            {
                Name = name;
                IsPrimaryKey = isPrimaryKey;
                Count = 1;
            }
            else
            {
                Name = null;
                IsPrimaryKey = false;
                Count = 0;
            }
        }

        /// <summary>
        /// [addedInVersion, removedInVersion[
        /// </summary>
        /// <param name="name">database field name</param>
        /// <param name="addedInVersion">initial version</param>
        /// <param name="removedInVersion">final version</param>
        /// <param name="isPrimaryKey">true if field is a primary key</param>
        public DBFieldNameAttribute(string name, ClientVersionBuild addedInVersion, ClientVersionBuild removedInVersion, bool isPrimaryKey = false)
        {
            if (ClientVersion.AddedInVersion(addedInVersion) && ClientVersion.RemovedInVersion(removedInVersion))
            {
                Name = name;
                IsPrimaryKey = isPrimaryKey;
                Count = 1;
            }
            else
            {
                Name = null;
                IsPrimaryKey = false;
                Count = 0;
            }
        }

        /// <summary>
        /// [addedInVersion, removedInVersion[
        /// </summary>
        /// <param name="name">database field name</param>
        /// <param name="addedInVersion">initial version</param>
        /// <param name="removedInVersion">final version</param>
        /// <param name="locale">initial locale</param>
        /// <param name="isPrimaryKey">true if field is a primary key</param>
        public DBFieldNameAttribute(string name, ClientVersionBuild addedInVersion, ClientVersionBuild removedInVersion, LocaleConstant locale, bool isPrimaryKey = false)
        {
            if (ClientVersion.AddedInVersion(addedInVersion) && ClientVersion.RemovedInVersion(removedInVersion) && BinaryPacketReader.GetLocale() == locale)
            {
                Name = name;
                IsPrimaryKey = isPrimaryKey;
                Count = 1;
            }
            else
            {
                Name = null;
                IsPrimaryKey = false;
                Count = 0;
            }
        }

        /// <summary>
        /// matches any version
        /// </summary>
        /// <param name="name">database field name</param>
        /// <param name="count">number of fields</param>
        /// <param name="startAtZero">true if fields name start at 0</param>
        /// <param name="isPrimaryKey">true if field is a primary key</param>
        public DBFieldNameAttribute(string name, int count, bool startAtZero = false, bool isPrimaryKey = false)
        {
            Name = name;
            IsPrimaryKey = isPrimaryKey;
            Count = count;
            StartAtZero = startAtZero;
            _multipleFields = true;
        }

        /// <summary>
        /// [addedInVersion, +inf[
        /// </summary>
        /// <param name="name">database field name</param>
        /// <param name="addedInVersion">initial version</param>
        /// <param name="count">number of fields</param>
        /// <param name="startAtZero">true if fields name start at 0</param>
        /// <param name="isPrimaryKey">true if field is a primary key</param>
        public DBFieldNameAttribute(string name, ClientVersionBuild addedInVersion, int count, bool startAtZero = false, bool isPrimaryKey = false)
        {
            if (ClientVersion.AddedInVersion(addedInVersion))
            {
                Name = name;
                IsPrimaryKey = isPrimaryKey;
                Count = count;
            }
            else
            {
                Name = null;
                IsPrimaryKey = false;
                Count = 0;
            }

            StartAtZero = startAtZero;
            _multipleFields = true;
        }

        /// <summary>
        /// [addedInVersion, removedInVersion[
        /// </summary>
        /// <param name="name">database field name</param>
        /// <param name="addedInVersion">initial version</param>
        /// <param name="removedInVersion">final version</param>
        /// <param name="count">number of fields</param>
        /// <param name="startAtZero">true if fields name start at 0</param>
        /// <param name="isPrimaryKey">true if field is a primary key</param>
        public DBFieldNameAttribute(string name, ClientVersionBuild addedInVersion, ClientVersionBuild removedInVersion, int count, bool startAtZero = false, bool isPrimaryKey = false)
        {
            if (ClientVersion.AddedInVersion(addedInVersion) && ClientVersion.RemovedInVersion(removedInVersion))
            {
                Name = name;
                IsPrimaryKey = isPrimaryKey;
                Count = count;
            }
            else
            {
                Name = null;
                IsPrimaryKey = false;
                Count = 0;
            }

            StartAtZero = startAtZero;
            _multipleFields = true;
        }

        /// <summary>
        /// String representation of the field or group of fields
        /// </summary>
        /// <returns>name</returns>
        public override string ToString()
        {
            if (Name == null)
                return null;

            if (!_multipleFields)
                return SQLUtil.AddBackQuotes(Name);

            var result = new StringBuilder();
            for (var i = 1; i <= Count; i++)
            {
                result.Append(SQLUtil.AddBackQuotes(Name + (StartAtZero ? i - 1 : i)));
                if (i != Count)
                    result.Append(",");
            }
            return result.ToString();
        }
    }
}
