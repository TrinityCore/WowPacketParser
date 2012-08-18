using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace PacketViewer.Forms
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialogNewPacketFile.ShowDialog() != DialogResult.OK)
                return;
            var file = openFileDialogNewPacketFile.FileName;
            var dial = new FormFileOpenDetails(file);
            if (dial.ShowDialog(this) != System.Windows.Forms.DialogResult.OK)
            {
                dial.Dispose();
                return;
            }
            var newTab = new TabPageFile(file, new PacketFileTab(file, dial));
            tabControlFiles.Controls.Add(newTab);
            tabControlFiles.SelectedTab = newTab;
        }

        private void contextMenuStripTabs_Opening(object sender, CancelEventArgs e)
        {

        }

        private void closeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            tabControlFiles.SelectedTab.Dispose();
            GC.Collect();
        }

        private void closeAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            while(tabControlFiles.TabCount > 0)
                tabControlFiles.TabPages[0].Dispose();
            GC.Collect();
        }

        private void closeAllButThisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < tabControlFiles.TabCount;)
            {
                if (i != tabControlFiles.SelectedIndex)
                    tabControlFiles.TabPages[i].Dispose();
                else
                    ++i;
            }
            GC.Collect();
        }

        private void tabControlFiles_TabIndexChanged(object sender, EventArgs e)
        {
        }

        private void tabControlFiles_Selecting(object sender, TabControlCancelEventArgs e)
        {
            for (int i = 0; i < tabControlFiles.TabCount; ++i)
            {
                if (((TabPageFile)tabControlFiles.TabPages[i]).FileTab.Selected)
                {
                    ((TabPageFile)tabControlFiles.TabPages[i]).FileTab.BecameUnSelected();
                    break;
                }
            }
            if (e.TabPage != null)
                ((TabPageFile)e.TabPage).FileTab.BecameSelected();
        }

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void statusStripInfo_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
