using FormsUI.Windows;
using FormsUI.Workspaces;
using NesEmulator.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NesEmulator.NesInspector
{
    public partial class FrmNesExplorer : ToolWindow
    {
        private readonly WindowTools _windowTools;
        private InspectorModel _model = new InspectorModel();
        private TreeNode? _rootNode;

        public FrmNesExplorer(IAppWindow appWindow)
            : base(appWindow)
        {
            InitializeComponent();
            _windowTools = new WindowTools(
                new ToolStripMerge(toolStrip1, true));
        }

        protected override WindowTools WindowTools => _windowTools;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            
        }

        protected override void OnWorkspaceCreated(object sender, WorkspaceCreatedEventArgs e)
        {
            _model = e.Model as InspectorModel ?? throw new InvalidOperationException("Model has not been initialized.");
            if (_model.Cartridge != null)
            {
                _rootNode = tv.Nodes.Add(Path.GetFileNameWithoutExtension(_model.FileName));
                _rootNode.Nodes.Add($"Version: {_model.Cartridge.Version}");
                _rootNode.Nodes.Add($"Mapper type: {_model.Cartridge.MapperType}");
                _rootNode.Nodes.Add($"PRG ROM size: {_model.Cartridge.PrgRomSize}");
                _rootNode.Nodes.Add($"CHR ROM size: {_model.Cartridge.ChrRomSize}");
                _rootNode.Nodes.Add($"PRG RAM size: {_model.Cartridge.PrgRamSize}");
            }
        }
    }
}
