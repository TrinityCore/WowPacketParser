using NUnit.Framework;
using System;
using System.Collections.Generic;
using WowPacketParser.Enums;
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
                { 148, new UpdateField(0xFFFFFFFF) },
                { 149, new UpdateField(0xFFFFFFFF) }
            };

            Assert.Throws<ArgumentException>(() => updateFields.GetValue<PlayerField, ulong>(PlayerField.PLAYER_DUEL_ARBITER));
        }

        [Test]
        public void TestWithMissingValue()
        {
            var updateFields = new Dictionary<int, UpdateField>();

            Assert.That(0, Is.EqualTo(updateFields.GetValue<ObjectField, int>(ObjectField.OBJECT_FIELD_ENTRY)));
            Assert.That(0u, Is.EqualTo(updateFields.GetValue<ObjectField, uint>(ObjectField.OBJECT_FIELD_ENTRY)));
            Assert.That(0.0f, Is.EqualTo(updateFields.GetValue<ObjectField, float>(ObjectField.OBJECT_FIELD_ENTRY)));
            Assert.That(null, Is.EqualTo(updateFields.GetValue<ObjectField, int?>(ObjectField.OBJECT_FIELD_ENTRY)));
            Assert.That(null, Is.EqualTo(updateFields.GetValue<ObjectField, uint?>(ObjectField.OBJECT_FIELD_ENTRY)));
            Assert.That(null, Is.EqualTo(updateFields.GetValue<ObjectField, float?>(ObjectField.OBJECT_FIELD_ENTRY)));
        }
    }
}
