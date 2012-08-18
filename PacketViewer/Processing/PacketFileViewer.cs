using System;
using PacketParser.Processing;
using PacketParser.Misc;
using PacketParser.Enums;
using PacketParser.Loading;
using PacketParser.DataStructures;
using PacketParser.Parsing;
using XPTable.Models;

using System.ComponentModel;

using PacketViewer.Forms;

namespace PacketViewer.Processing
{
    public class PacketFileViewer : PacketFileProcessor, IDisposable
    {
        public PacketFileTab Tab;

        private IPacketReader _reader;
        private string _readerType;
        public FormFileOpenDetails FileOpenDetails;

        private ClientVersionBuild _build;

        public GetDetailsViewControlHandler GetDetailsViewControlHandler;

        private const int minPacketsForProgressUpdate = 600;

        public PacketFileViewer(string fileName, PacketFileTab tab, FormFileOpenDetails fileOpenDetails, Tuple<int, int> number = null)
            : base(fileName, number)
        {
            Tab = tab;
            _build = (ClientVersionBuild)fileOpenDetails.comboBoxClientVersion.SelectedItem;
            _readerType = fileOpenDetails.comboBoxReaderType.SelectedItem.ToString();
            _reader = Reader.GetReader(_readerType, fileName);
            FileOpenDetails = fileOpenDetails;
        }

        public string GetFileInfoString()
        {
            return "File Info: " + FileName + "  Version: " + GetBuild().ToString() + " Expansion: " + ClientVersion.GetExpansion(_build).ToString() + " ReaderType: " + _readerType;
        }

        public ClientVersionBuild GetBuild()
        {
            return _build;
        }

        public void Process(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            Current = this;
            ClientVersion.SetVersion(GetBuild());
            var packetNum = 0;
            // initialize processors
            InitProcessors();

            uint oldPct = 0;
            uint progressCheckPackets = 0;
            worker.ReportProgress((int)oldPct);
            while (_reader.CanRead())
            {
                ++progressCheckPackets;
                var packet = _reader.Read(packetNum, FileName);

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
                    var newPct = _reader.GetProgress();
                    if (newPct != oldPct)
                    {
                        worker.ReportProgress((int)newPct);
                        oldPct = newPct;
                    }
                    progressCheckPackets = 0;
                }
            }

            FinishProcessors();

            _reader.Dispose();
            worker.ReportProgress(100);
            GC.Collect();
        }

        protected override void InitProcessors()
        {
            base.InitProcessors();
            foreach (var p in Processors)
            {
                if (p.Value is IDetailsViewPacketProcessor)
                    GetDetailsViewControlHandler += ((IDetailsViewPacketProcessor)p.Value).GetDetailsViewControl;
            }
            if (GetDetailsViewControlHandler == null)
                GetDetailsViewControlHandler += GetDetailsViewControlHandlerStub;
        }

        public void GetDetailsViewControlHandlerStub(int packetUID, DetailsView detailsView, Cell cell)
        {
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
            FileOpenDetails.Dispose();
            FileOpenDetails = null;
        }
    }
}
