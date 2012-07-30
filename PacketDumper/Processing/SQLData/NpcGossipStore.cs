using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PacketParser.Enums;
using PacketParser.Enums.Version;
using PacketParser.Misc;
using PacketParser.Processing;
using PacketParser.SQL;
using PacketDumper.Enums;
using PacketDumper.Misc;
using PacketParser.DataStructures;
using PacketDumper.DataStructures;

namespace PacketDumper.Processing.SQLData
{
    public class NpcGossipStore : IPacketProcessor
    {
        public bool LoadOnDepend { get { return false; } }
        public Type[] DependsOn { get { return null; } }

        public ProcessPacketEventHandler ProcessAnyPacketHandler {get {return ProcessPacket;} }
        public ProcessedPacketEventHandler ProcessedAnyPacketHandler  {get {return null;} }
        public ProcessDataEventHandler ProcessAnyDataHandler { get { return null; } }
        public ProcessedDataNodeEventHandler ProcessedAnyDataNodeHandler { get { return null; } }

        // Gossips (MenuId, TextId)
        public static readonly TimeSpanDictionary<Tuple<uint, uint>, Gossip> Gossips = new TimeSpanDictionary<Tuple<uint, uint>, Gossip>();

        public bool Init(PacketFileProcessor file)
        {
            return Settings.SQLOutput.HasFlag(SQLOutputFlags.Gossip);
        }

        public void ProcessPacket(Packet packet)
        {
            if (packet.Status != ParsedStatus.Success)
                return;

            if (Opcode.SMSG_GOSSIP_MESSAGE == Opcodes.GetOpcode(packet.Opcode))
            {
                var menuId = packet.GetData().GetNode< UInt32 >("Menu Id");
                var textId = packet.GetData().GetNode< UInt32 >("Text Id");

                Gossips.Add(Tuple.Create(menuId, textId), packet.GetNode<Gossip>("GossipObject"), packet.TimeSpan);
            }
        }

        public void Finish()
        {

        }

        public string Build()
        {
            // TODO: This should be rewritten
            if (Gossips.IsEmpty())
                return String.Empty;

            var names = PacketFileProcessor.Current.GetProcessor<NameStore>();

            var result = "";

            // `gossip`
            if (SQLConnector.Enabled)
            {
                var query = new StringBuilder(string.Format("SELECT `entry`,`text_id` FROM {0}.`gossip_menu` WHERE ", ParserSettings.MySQL.TDBDB));
                foreach (Tuple<uint, uint> gossip in Gossips.Keys())
                {
                    query.Append("(`entry`=").Append(gossip.Item1).Append(" AND ");
                    query.Append("`text_id`=").Append(gossip.Item2).Append(") OR ");
                }
                query.Remove(query.Length - 4, 4).Append(";");

                var rows = new List<QueryBuilder.SQLInsertRow>();
                using (var reader = SQLConnector.ExecuteQuery(query.ToString()))
                {
                    if (reader != null)
                        while (reader.Read())
                        {
                            var values = new object[2];
                            var count = reader.GetValues(values);
                            if (count != 2)
                                break; // error in query

                            var entry = Convert.ToUInt32(values[0]);
                            var textId = Convert.ToUInt32(values[1]);

                            // our table is small, 2 fields and both are PKs; no need for updates
                            if (!Gossips.ContainsKey(Tuple.Create(entry, textId)))
                            {
                                var row = new QueryBuilder.SQLInsertRow();
                                row.AddValue("entry", entry);
                                row.AddValue("text_id", textId);
                                row.Comment = names.GetName(StoreNameType.Unit, // BUG: GOs can send gossips too
                                                                   (int)entry, false);
                                rows.Add(row);
                            }
                        }
                }
                result += new QueryBuilder.SQLInsert("gossip_menu", rows, 2).Build();
            }
            else
            {
                var rows = new List<QueryBuilder.SQLInsertRow>();
                foreach (var gossip in Gossips)
                {
                    var row = new QueryBuilder.SQLInsertRow();

                    row.AddValue("entry", gossip.Key.Item1);
                    row.AddValue("text_id", gossip.Key.Item2);
                    row.Comment = names.GetName(Utilities.ObjectTypeToStore(gossip.Value.Item1.ObjectType),
                                                       (int)gossip.Value.Item1.ObjectEntry, false);

                    rows.Add(row);
                }

                result += new QueryBuilder.SQLInsert("gossip_menu", rows, 2).Build();
            }

            // `gossip_menu_option`
            if (SQLConnector.Enabled)
            {
                var rowsIns = new List<QueryBuilder.SQLInsertRow>();
                var rowsUpd = new List<QueryBuilder.SQLUpdateRow>();

                foreach (var gossip in Gossips)
                {
                    if (gossip.Value.Item1.GossipOptions == null) continue;
                    foreach (var gossipOption in gossip.Value.Item1.GossipOptions)
                    {
                        var query =       //         0     1       2         3         4        5         6
                            string.Format("SELECT menu_id,id,option_icon,box_coded,box_money,box_text,option_text " +
                                          "FROM {2}.gossip_menu_option WHERE menu_id={0} AND id={1};", gossip.Key.Item1,
                                          gossipOption.Index, ParserSettings.MySQL.TDBDB);
                        using (var reader = SQLConnector.ExecuteQuery(query))
                        {
                            if (reader.HasRows) // possible update
                            {
                                while (reader.Read())
                                {
                                    var row = new QueryBuilder.SQLUpdateRow();

                                    if (!Utilities.EqualValues(reader.GetValue(2), gossipOption.OptionIcon))
                                        row.AddValue("option_icon", gossipOption.OptionIcon);

                                    if (!Utilities.EqualValues(reader.GetValue(3), gossipOption.Box))
                                        row.AddValue("box_coded", gossipOption.Box);

                                    if (!Utilities.EqualValues(reader.GetValue(4), gossipOption.RequiredMoney))
                                        row.AddValue("box_money", gossipOption.RequiredMoney);

                                    if (!Utilities.EqualValues(reader.GetValue(5), gossipOption.BoxText))
                                        row.AddValue("box_text", gossipOption.BoxText);

                                    if (!Utilities.EqualValues(reader.GetValue(6), gossipOption.OptionText))
                                        row.AddValue("option_text", gossipOption.OptionText);

                                    row.AddWhere("menu_id", gossip.Key.Item1);
                                    row.AddWhere("id", gossipOption.Index);

                                    row.Comment =
                                        names.GetName(Utilities.ObjectTypeToStore(gossip.Value.Item1.ObjectType),
                                                             (int)gossip.Value.Item1.ObjectEntry, false);

                                    row.Table = "gossip_menu_option";

                                    if (row.ValueCount != 0)
                                        rowsUpd.Add(row);
                                }
                            }
                            else // insert
                            {
                                var row = new QueryBuilder.SQLInsertRow();

                                row.AddValue("menu_id", gossip.Key.Item1);
                                row.AddValue("id", gossipOption.Index);
                                row.AddValue("option_icon", gossipOption.OptionIcon);
                                row.AddValue("option_text", gossipOption.OptionText);
                                row.AddValue("box_coded", gossipOption.Box);
                                row.AddValue("box_money", gossipOption.RequiredMoney);
                                row.AddValue("box_text", gossipOption.BoxText);

                                row.Comment = names.GetName(Utilities.ObjectTypeToStore(gossip.Value.Item1.ObjectType),
                                                           (int)gossip.Value.Item1.ObjectEntry, false);

                                rowsIns.Add(row);
                            }
                        }
                    }
                }
                result += new QueryBuilder.SQLInsert("gossip_menu_option", rowsIns, 2).Build() +
                          new QueryBuilder.SQLUpdate(rowsUpd).Build();
            }
            else
            {
                var rows = new List<QueryBuilder.SQLInsertRow>();
                foreach (var gossip in Gossips)
                {
                    if (gossip.Value.Item1.GossipOptions != null)
                        foreach (var gossipOption in gossip.Value.Item1.GossipOptions)
                        {
                            var row = new QueryBuilder.SQLInsertRow();

                            row.AddValue("menu_id", gossip.Key.Item1);
                            row.AddValue("id", gossipOption.Index);
                            row.AddValue("option_icon", gossipOption.OptionIcon);
                            row.AddValue("option_text", gossipOption.OptionText);
                            row.AddValue("box_coded", gossipOption.Box);
                            row.AddValue("box_money", gossipOption.RequiredMoney);
                            row.AddValue("box_text", gossipOption.BoxText);

                            row.Comment = names.GetName(Utilities.ObjectTypeToStore(gossip.Value.Item1.ObjectType),
                                                       (int)gossip.Value.Item1.ObjectEntry, false);

                            rows.Add(row);
                        }
                }

                result += new QueryBuilder.SQLInsert("gossip_menu_option", rows, 2).Build();
            }

            return result;
        }
    }
}
