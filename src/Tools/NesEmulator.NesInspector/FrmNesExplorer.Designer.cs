namespace NesEmulator.NesInspector
{
    partial class FrmNesExplorer
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
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmNesExplorer));
            this.tv = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.childToolStrip = new System.Windows.Forms.ToolStrip();
            this.tbtnBinaryViewer = new System.Windows.Forms.ToolStripButton();
            this.tbtnDasm = new System.Windows.Forms.ToolStripButton();
            this.childToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // tv
            // 
            this.tv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tv.ImageIndex = 0;
            this.tv.ImageList = this.imageList1;
            this.tv.Location = new System.Drawing.Point(0, 25);
            this.tv.Name = "tv";
            this.tv.SelectedImageIndex = 0;
            this.tv.Size = new System.Drawing.Size(369, 355);
            this.tv.TabIndex = 1;
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // childToolStrip
            // 
            this.childToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbtnBinaryViewer,
            this.tbtnDasm});
            this.childToolStrip.Location = new System.Drawing.Point(0, 0);
            this.childToolStrip.Name = "childToolStrip";
            this.childToolStrip.Size = new System.Drawing.Size(369, 25);
            this.childToolStrip.TabIndex = 2;
            this.childToolStrip.Text = "toolStrip1";
            // 
            // tbtnBinaryViewer
            // 
            this.tbtnBinaryViewer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtnBinaryViewer.Image = global::NesEmulator.NesInspector.Properties.Resources.document_binary;
            this.tbtnBinaryViewer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnBinaryViewer.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.tbtnBinaryViewer.MergeIndex = 5;
            this.tbtnBinaryViewer.Name = "tbtnBinaryViewer";
            this.tbtnBinaryViewer.Size = new System.Drawing.Size(23, 22);
            this.tbtnBinaryViewer.Text = "Binary Viewer";
            this.tbtnBinaryViewer.Click += new System.EventHandler(this.Action_OpenBinaryViewer);
            // 
            // tbtnDasm
            // 
            this.tbtnDasm.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtnDasm.Image = global::NesEmulator.NesInspector.Properties.Resources.processor;
            this.tbtnDasm.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnDasm.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.tbtnDasm.MergeIndex = 6;
            this.tbtnDasm.Name = "tbtnDasm";
            this.tbtnDasm.Size = new System.Drawing.Size(23, 22);
            this.tbtnDasm.Text = "Disassemble";
            this.tbtnDasm.Click += new System.EventHandler(this.Action_OpenDisassembler);
            // 
            // FrmNesExplorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(369, 380);
            this.Controls.Add(this.tv);
            this.Controls.Add(this.childToolStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmNesExplorer";
            this.Text = "NES Explorer";
            this.childToolStrip.ResumeLayout(false);
            this.childToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TreeView tv;
        private ImageList imageList1;
        private ToolStrip childToolStrip;
        private ToolStripButton tbtnDasm;
        private ToolStripButton tbtnBinaryViewer;
    }
}