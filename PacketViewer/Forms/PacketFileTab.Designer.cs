using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Threading;

using XPTable.Models;
using XPTable.Events;

using PacketViewer.DataStructures;

namespace PacketViewer.Forms
{
    partial class PacketFileTab
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (PacketProcessor != null)
            {
                PacketProcessor.Dispose();
                PacketProcessor = null;
            }
            if (tablePackets != null)
            {
                tablePackets.Dispose();
                tablePackets = null;
            }
            if (backgroundWorkerProcessPackets != null)
            {
                backgroundWorkerProcessPackets.Dispose();
                backgroundWorkerProcessPackets = null;
            }
            if (dataManager != null)
            {
                dataManager.Dispose();
                dataManager = null;
            }
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            XPTable.Models.DataSourceColumnBinder dataSourceColumnBinder1 = new XPTable.Models.DataSourceColumnBinder();
            XPTable.Renderers.DragDropRenderer dragDropRenderer1 = new XPTable.Renderers.DragDropRenderer();
            this.tablePackets = new XPTable.Models.Table();
            this.backgroundWorkerProcessPackets = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorkerTableVirtualData = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.tablePackets)).BeginInit();
            this.SuspendLayout();
            // 
            // tablePackets
            // 
            this.tablePackets.BorderColor = System.Drawing.Color.Black;
            this.tablePackets.DataMember = null;
            this.tablePackets.DataSourceColumnBinder = dataSourceColumnBinder1;
            this.tablePackets.Dock = System.Windows.Forms.DockStyle.Fill;
            dragDropRenderer1.ForeColor = System.Drawing.Color.Red;
            this.tablePackets.DragDropRenderer = dragDropRenderer1;
            this.tablePackets.GridLinesContrainedToData = false;
            this.tablePackets.HiddenSubRows = 0;
            this.tablePackets.HiddenSubRowsAboveTop = 0;
            this.tablePackets.Location = new System.Drawing.Point(0, 0);
            this.tablePackets.Name = "tablePackets";
            this.tablePackets.Size = new System.Drawing.Size(583, 364);
            this.tablePackets.TabIndex = 0;
            this.tablePackets.Text = "table1";
            this.tablePackets.UnfocusedBorderColor = System.Drawing.Color.Black;
            // 
            // backgroundWorkerProcessPackets
            // 
            this.backgroundWorkerProcessPackets.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerProcessPackets_DoWork);
            this.backgroundWorkerProcessPackets.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorkerProcessPackets_ProgressChanged);
            this.backgroundWorkerProcessPackets.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerProcessPackets_RunWorkerCompleted);
            // 
            // backgroundWorkerTableVirtualData
            // 
            this.backgroundWorkerTableVirtualData.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerTableVirtualData_DoWork);
            // 
            // PacketFileTab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tablePackets);
            this.Name = "PacketFileTab";
            this.Size = new System.Drawing.Size(583, 364);
            this.Load += new System.EventHandler(this.PacketFileTab2_Load);
            this.ControlRemoved += new System.Windows.Forms.ControlEventHandler(this.PacketFileTab_ControlRemoved);
            ((System.ComponentModel.ISupportInitialize)(this.tablePackets)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private XPTable.Models.Table tablePackets;
        private BackgroundWorker backgroundWorkerProcessPackets;
        private BackgroundWorker backgroundWorkerTableVirtualData;
    }
}
