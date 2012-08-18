using System;
using PacketParser.Processing;
using PacketParser.DataStructures;
using PacketViewer.Forms;
using XPTable.Models;
using System.Windows.Forms;
using PacketParser.Misc;
using System.Threading;

namespace PacketViewer.Processing
{
    public class TextOutput : IDetailsViewPacketProcessor
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
            bool init = ((PacketFileViewer)proc).FileOpenDetails.checkBoxShowTextOutput.Checked;
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
            texts.ChangeBlock(packet.ParseID, PacketFileProcessor.Current.GetProcessor<TextBuilder>().LastPacket);
            texts.EndBlocksUpdate();
            texts.UnCacheBlocksAfter(packet.ParseID);
            textMutex.ReleaseMutex();
        }

        public void GetDetailsViewControl(int packetUID, DetailsView detailsView, Cell cell)
        {
            textMutex.WaitOne();
            var c = new RichTextBox();
            c.Text = texts.GetBlock(packetUID);
            detailsView.AddView(c);
            texts.UnCacheBlock(packetUID);
            textMutex.ReleaseMutex();
        }
    }
}
