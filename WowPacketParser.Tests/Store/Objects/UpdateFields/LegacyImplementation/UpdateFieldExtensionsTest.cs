using NUnit.Framework;
using System;
using System.Collections.Generic;
using WowPacketParser.Misc;
using WowPacketParser.Store.Objects.UpdateFields.LegacyImplementation;

namespace WowPacketParser.Tests.Store.Objects.UpdateFields.LegacyImplementation
{
    [TestFixture]
    public class UpdateFieldExtensionsTest
    {
        [Test]
        public void TestGetLongValue()
        {
            var updateFields = new Dictionary<int, UpdateField>
            {
                { 4, new UpdateField(0xFFFFFFFF) },
                { 5, new UpdateField(0xFFFFFFFF) }
            };

            Assert.Throws<ArgumentException>(() => updateFields.GetValue<int, ulong>(4));
        }
    }
}
