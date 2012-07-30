using System;
using PacketParser.Processing;
using PacketParser.Misc;
using PacketParser.Loading;
using PacketParser.DataStructures;
using PacketParser.Parsing;

using System.ComponentModel;

using PacketViewer.Forms;

namespace PacketViewer.Processing
{
    public class PacketFileViewer : PacketFileProcessor, IDisposable
    {
        public PacketFileTab Tab;

        private IPacketReader reader;
        private string readerType = "";

        private const int minPacketsForProgressUpdate = 600;

        public PacketFileViewer(string fileName, Tuple<int, int> number = null, PacketFileTab tab = null)
            : base(fileName, number)
        {
            Tab = tab;
            readerType = Reader.GetReaderTypeByFileName(fileName);
            reader = Reader.GetReader(readerType, fileName);
        }

        public string GetFileInfoString()
        {
            return "File Info: " + FileName + "  Version: " + reader.GetBuild().ToString() +" Expansion: " + ClientVersion.GetExpansion(reader.GetBuild()).ToString() + " ReaderType: " + readerType;
        }

        public void Process(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            Current = this;
            ClientVersion.SetVersion(reader.GetBuild());
            var packetNum = 0;
            // initialize processors
            InitProcessors();

            uint oldPct = 0;
            uint progressCheckPackets = 0;
            worker.ReportProgress((int)oldPct);
            while (reader.CanRead())
            {
                ++progressCheckPackets;
                var packet = reader.Read(packetNum, FileName);

                // read error
                if (packet == null)
                    continue;

                ++packetNum;

                if (worker.CancellationPending == true)
                {
                    e.Cancel = true;
                    return;
                }

                ProcessPacket(packet);

                if (progressCheckPackets >= minPacketsForProgressUpdate)
                {
                    var newPct = reader.GetProgress();
                    if (newPct != oldPct)
                    {
                        worker.ReportProgress((int)newPct);
                        oldPct = newPct;
                    }
                    progressCheckPackets = 0;
                }
            }

            FinishProcessors();

            reader.Dispose();
            worker.ReportProgress(100);
            GC.Collect();
        }

        private void ProcessPacket(Packet packet)
        {
            // Parse the packet, read the data into StoreData tree
            Handler.Parse(packet);

            ProcessData(packet);

            // Close Writer, Stream - Dispose
            packet.ClosePacket();
        }

        public void Dispose()
        {
            Tab = null;
        }
    }
}
