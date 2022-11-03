using FormsUI.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Integration;

namespace NesEmulator.NesInspector
{
    public partial class FrmBinaryViewer : DocumentWindow
    {
        private readonly BinaryEditor _binaryEditor = new();
        private readonly ElementHost _elementHost = new();

        public FrmBinaryViewer(IAppWindow appWindow, InspectorModel model)
            : base(appWindow)
        {
            InitializeComponent();

            SuspendLayout();
            _elementHost.Location = new Point(236, 156); //location
            _elementHost.Name = "elementHost1";
            _elementHost.Size = new Size(200, 100);
            _elementHost.TabIndex = 0;
            _elementHost.Text = "elementHost1";
            _elementHost.Child = _binaryEditor;
            _elementHost.Dock = DockStyle.Fill;
            Controls.Add(_elementHost);
            ResumeLayout(false);

            Model = model;

        }

        public InspectorModel Model { get; init; }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (Model?.Cartridge != null)
            {
                _binaryEditor.Editor.ReadOnlyMode = true;
                _binaryEditor.Editor.FileName = Model.FileName;
            }
        }
    }
}
