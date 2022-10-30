using FormsUI.Windows;
using NesEmulator.Core;
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
    public partial class FrmDisassemble : DocumentWindow
    {
        private ElementHost _elementHost = new ElementHost();

        public FrmDisassemble(IAppWindow appWindow, InspectorModel model)
        {
            InitializeComponent();

            SuspendLayout();
            _elementHost.Location = new System.Drawing.Point(236, 156); //location
            _elementHost.Name = "elementHost1";
            _elementHost.Size = new System.Drawing.Size(200, 100);
            _elementHost.TabIndex = 0;
            _elementHost.Text = "elementHost1";
            _elementHost.Dock = DockStyle.Fill;
            ResumeLayout(false);

            Model = model;

        }

        public InspectorModel Model { get; init; }

        private void InitElementHost()
        {
            
        }
    }
}
