// ============================================================================
//       __ __   __
//  |\ ||_ (_   |_  _    | _ |_ _  _
//  | \||____)  |__||||_||(_||_(_)|
//
// Written by Sunny Chen (daxnet), 2022
// MIT License
// ============================================================================

using FormsUI.Windows;
using NesEmulator.Core;
using System.IO;
using System.Text;
using System.Windows.Forms.Integration;

namespace NesEmulator.NesInspector
{
    public partial class FrmDisassembler : DocumentWindow
    {
        private readonly SyntaxHighlightedEditor _editor = new();
        private readonly ElementHost _elementHost = new();
        private readonly Emulator _emulator = new();
        private readonly string _nesName;

        public FrmDisassembler(IAppWindow appWindow, InspectorModel model)
            : base(appWindow)
        {
            InitializeComponent();

            _editor.IsReadOnly = true;
            _editor.SyntaxHighlightingDefinitionResourceName = "NesEmulator.NesInspector.6502.xshd";

            SuspendLayout();
            _elementHost.Location = new Point(236, 156); //location
            _elementHost.Name = "elementHost1";
            _elementHost.Size = new Size(200, 100);
            _elementHost.TabIndex = 0;
            _elementHost.Text = "elementHost1";
            _elementHost.Child = _editor;
            _elementHost.Dock = DockStyle.Fill;
            Controls.Add(_elementHost);
            ResumeLayout(false);

            Model = model;

            _nesName = Path.GetFileName(Model.FileName);
            Text = _nesName;
        }

        public InspectorModel Model { get; init; }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (Model?.Cartridge != null)
            {
                var dasmStringBuilder = new StringBuilder();
                dasmStringBuilder.AppendLine($"; {_nesName}");
                dasmStringBuilder.AppendLine(";");
                dasmStringBuilder.AppendLine($"; PRG ROM SIZE: {Model.Cartridge.PrgRomSize:n0} bytes");
                dasmStringBuilder.AppendLine($"; CHR ROM SIZE: {Model.Cartridge.ChrRomSize:n0} bytes");
                dasmStringBuilder.AppendLine($"; PRG RAM SIZE: {Model.Cartridge.PrgRamSize:n0} bytes");
                dasmStringBuilder.AppendLine(";");
                dasmStringBuilder.AppendLine($"; Disassembled by NesEmulator @ {DateTime.Now}");
                dasmStringBuilder.AppendLine($"; Written by Sunny Chen (daxnet) 2022.");
                dasmStringBuilder.AppendLine($"; https://github.com/daxnet/NesEmulator");
                dasmStringBuilder.AppendLine();
                dasmStringBuilder.AppendLine(_emulator.Cpu.Disassemble(Model.Cartridge.PrgRom));
                _editor.Text = dasmStringBuilder.ToString();
            }
        }
    }
}