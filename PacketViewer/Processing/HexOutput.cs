using System;
using PacketParser.Processing;
using PacketParser.DataStructures;
using PacketViewer.Forms;
using XPTable.Models;
using System.Windows.Forms;
using PacketParser.Misc;
using System.Text;
using System.Threading;

namespace PacketViewer.Processing
{
    public class HexOutput : IDetailsViewPacketProcessor
    {
        public bool LoadOnDepend { get { return false; } }
        public Type[] DependsOn { get { return new Type[] { typeof(TextBuilder) }; } }

        public ProcessPacketEventHandler ProcessAnyPacketHandler { get { return null; } }
        public ProcessedPacketEventHandler ProcessedAnyPacketHandler { get { return ProcessedPacket; } }
        public ProcessDataEventHandler ProcessAnyDataHandler { get { return null; } }
        public ProcessedDataNodeEventHandler ProcessedAnyDataNodeHandler { get { return null; } }

        CacheFileManager<string> texts;
        Mutex textMutex = new Mutex();

        public bool Init(PacketFileProcessor proc)
        {
            bool init = ((PacketFileViewer)proc).FileOpenDetails.checkBoxShowHexOutput.Checked;
            if (init)
            {
                texts = new CacheFileManager<string>();
            }
            return init;
        }

        public void Finish()
        {
        }

        public void ProcessedPacket(Packet packet)
        {
            textMutex.WaitOne();
            texts.BeginBlocksUpdate(packet.ParseID);
            texts.ChangeBlock(packet.ParseID, BuildHexString(packet));
            texts.EndBlocksUpdate();
            texts.UnCacheBlocksAfter(packet.ParseID);
            textMutex.ReleaseMutex();
        }

        public void GetDetailsViewControl(int packetUID, DetailsView detailsView, Cell cell)
        {
            textMutex.WaitOne();
            var c = new RichTextBox();
            c.Text = texts.GetBlock(packetUID);
            c.Width = 530;
            c.WordWrap = false;
            c.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            detailsView.AddView(c);
            texts.UnCacheBlock(packetUID);
            textMutex.ReleaseMutex();
        }

        public string BuildHexString(Packet packet)
        {
            StringBuilder result = new StringBuilder();
            var length = packet.BaseStream.Length;
            var byteBuff = packet.GetStream(0);
            var offset = 0;
            for (var i = 0; i < length; i += 0x10)
            {
                var bytes = new StringBuilder();
                var chars = new StringBuilder();

                for (var j = 0; j < 0x10; ++j)
                {
                    if (offset < length)
                    {
                        int c = byteBuff[offset];
                        offset++;

                        bytes.AppendFormat("{0,-3:X2}", c);
                        chars.Append((c >= 0x20 && c < 0x80) ? (char)c : '.');
                    }
                    else
                    {
                        bytes.Append("   ");
                        chars.Append(' ');
                    }
                }

                result.AppendLine(i.ToString("X4") + ": " + bytes + ": " + chars);
            }
            return result.ToString();
        }
    }
}