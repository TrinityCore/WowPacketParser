using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PacketViewer.Forms
{
    public class TabPageFile : TabPage
    {
        public PacketFileTab FileTab { get; private set; }
        public TabPageFile(string text, PacketFileTab fileTab)
            : base(text)
        {
            FileTab = fileTab;
            FileTab.Dock = DockStyle.Fill;
            Controls.Add(fileTab);
        }

        protected override void Dispose(bool disposing)
        {
            FileTab.Dispose();
            base.Dispose(disposing);
        }
    }
}
