// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: bgs/low/pb/client/club_types.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021, 8981
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace WowPacketParserModule.V10_0_0_46181.Protos.V10_2_0_52649.Bgs.Protocol.Club.V1 {

  /// <summary>Holder for reflection information generated from bgs/low/pb/client/club_types.proto</summary>
  public static partial class ClubTypesReflection {

    #region Descriptor
    /// <summary>File descriptor for bgs/low/pb/client/club_types.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static ClubTypesReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "CiJiZ3MvbG93L3BiL2NsaWVudC9jbHViX3R5cGVzLnByb3RvEhRiZ3MucHJv",
            "dG9jb2wuY2x1Yi52MRotYmdzL2xvdy9wYi9jbGllbnQvY2x1Yl9tZW1iZXJz",
            "aGlwX3R5cGVzLnByb3RvGiFiZ3MvbG93L3BiL2NsaWVudC9jbHViX2VudW0u",
            "cHJvdG8aIWJncy9sb3cvcGIvY2xpZW50L2NsdWJfcm9sZS5wcm90bxomYmdz",
            "L2xvdy9wYi9jbGllbnQvY2x1Yl9yYW5nZV9zZXQucHJvdG8aIWJncy9sb3cv",
            "cGIvY2xpZW50L2NsdWJfY29yZS5wcm90bxojYmdzL2xvdy9wYi9jbGllbnQv",
            "Y2x1Yl9tZW1iZXIucHJvdG8aJ2Jncy9sb3cvcGIvY2xpZW50L2NsdWJfaW52",
            "aXRhdGlvbi5wcm90bxogYmdzL2xvdy9wYi9jbGllbnQvY2x1Yl9iYW4ucHJv",
            "dG8aI2Jncy9sb3cvcGIvY2xpZW50L2NsdWJfc3RyZWFtLnByb3RvGiFiZ3Mv",
            "bG93L3BiL2NsaWVudC9jbHViX3R5cGUucHJvdG8aIGJncy9sb3cvcGIvY2xp",
            "ZW50L2NsdWJfdGFnLnByb3RvGitiZ3MvbG93L3BiL2NsaWVudC9jbHViX25h",
            "bWVfZ2VuZXJhdG9yLnByb3RvGjViZ3MvbG93L3BiL2NsaWVudC9hcGkvY2xp",
            "ZW50L3YyL2F0dHJpYnV0ZV90eXBlcy5wcm90bxolYmdzL2xvdy9wYi9jbGll",
            "bnQvYWNjb3VudF90eXBlcy5wcm90bxooYmdzL2xvdy9wYi9jbGllbnQvZXZl",
            "bnRfdmlld190eXBlcy5wcm90bxooYmdzL2xvdy9wYi9jbGllbnQvaW52aXRh",
            "dGlvbl90eXBlcy5wcm90bxolYmdzL2xvdy9wYi9jbGllbnQvbWVzc2FnZV90",
            "eXBlcy5wcm90bxohYmdzL2xvdy9wYi9jbGllbnQvZXRzX3R5cGVzLnByb3Rv",
            "GiNiZ3MvbG93L3BiL2NsaWVudC92b2ljZV90eXBlcy5wcm90bxohYmdzL2xv",
            "dy9wYi9jbGllbnQvcnBjX3R5cGVzLnByb3RvQgJIAVAAUAFQAlADUARQBVAG",
            "UAdQCFAJUApQC1AMUA1QDlAPUBBQEVASUBM="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { WowPacketParserModule.V10_0_0_46181.Protos.V10_2_0_52649.Bgs.Protocol.Club.V1.ClubMembershipTypesReflection.Descriptor, WowPacketParserModule.V10_0_0_46181.Protos.V10_2_0_52649.Bgs.Protocol.Club.V1.ClubEnumReflection.Descriptor, WowPacketParserModule.V10_0_0_46181.Protos.V10_2_0_52649.Bgs.Protocol.Club.V1.ClubRoleReflection.Descriptor, WowPacketParserModule.V10_0_0_46181.Protos.V10_2_0_52649.Bgs.Protocol.Club.V1.ClubRangeSetReflection.Descriptor, WowPacketParserModule.V10_0_0_46181.Protos.V10_2_0_52649.Bgs.Protocol.Club.V1.ClubCoreReflection.Descriptor, WowPacketParserModule.V10_0_0_46181.Protos.V10_2_0_52649.Bgs.Protocol.Club.V1.ClubMemberReflection.Descriptor, WowPacketParserModule.V10_0_0_46181.Protos.V10_2_0_52649.Bgs.Protocol.Club.V1.ClubInvitationReflection.Descriptor, WowPacketParserModule.V10_0_0_46181.Protos.V10_2_0_52649.Bgs.Protocol.Club.V1.ClubBanReflection.Descriptor, WowPacketParserModule.V10_0_0_46181.Protos.V10_2_0_52649.Bgs.Protocol.Club.V1.ClubStreamReflection.Descriptor, WowPacketParserModule.V10_0_0_46181.Protos.V10_2_0_52649.Bgs.Protocol.Club.V1.ClubTypeReflection.Descriptor, WowPacketParserModule.V10_0_0_46181.Protos.V10_2_0_52649.Bgs.Protocol.Club.V1.ClubTagReflection.Descriptor, WowPacketParserModule.V10_0_0_46181.Protos.V10_2_0_52649.Bgs.Protocol.Club.V1.ClubNameGeneratorReflection.Descriptor, WowPacketParserModule.V10_0_0_46181.Protos.V10_2_0_52649.Bgs.Protocol.V2.AttributeTypesReflection.Descriptor, WowPacketParserModule.V10_0_0_46181.Protos.V10_2_0_52649.Bgs.Protocol.Account.V1.AccountTypesReflection.Descriptor, WowPacketParserModule.V10_0_0_46181.Protos.V10_2_0_52649.Bgs.Protocol.EventViewTypesReflection.Descriptor, WowPacketParserModule.V10_0_0_46181.Protos.V10_2_0_52649.Bgs.Protocol.InvitationTypesReflection.Descriptor, WowPacketParserModule.V10_0_0_46181.Protos.V10_2_0_52649.Bgs.Protocol.MessageTypesReflection.Descriptor, WowPacketParserModule.V10_0_0_46181.Protos.V10_2_0_52649.Bgs.Protocol.EtsTypesReflection.Descriptor, WowPacketParserModule.V10_0_0_46181.Protos.V10_2_0_52649.Bgs.Protocol.VoiceTypesReflection.Descriptor, WowPacketParserModule.V10_0_0_46181.Protos.V10_2_0_52649.Bgs.Protocol.RpcTypesReflection.Descriptor, },
          new pbr::GeneratedClrTypeInfo(null, null, null));
    }
    #endregion

  }
}

#endregion Designer generated code