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
using DockState = WeifenLuo.WinFormsUI.Docking.DockState;

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
            _windowTools = new WindowTools(new ToolStripMerge(childToolStrip, true));
        }

        protected override WindowTools WindowTools => _windowTools;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            tbtnDasm.Enabled = false;
            tbtnBinaryViewer.Enabled = false;
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
            tbtnDasm.Enabled = true;
            tbtnBinaryViewer.Enabled = true;
        }

        private void Action_OpenDisassembler(object sender, EventArgs e)
        {
            CreateOrOpenWindow<FrmDisassembler>();
        }

        private void Action_OpenBinaryViewer(object sender, EventArgs e)
        {
            CreateOrOpenWindow<FrmBinaryViewer>();
        }

        private void CreateOrOpenWindow<T>()
            where T : DockableWindow
        {
            var window = AppWindow.WindowManager.GetFirstWindow<T>();
            if (window != null)
            {
                window.Show();
            }
            else
            {
                window = AppWindow.WindowManager.CreateWindow<T>(_model);
                window.ShowIcon = true;
                window.Show(AppWindow.DockArea, DockState.Document);
            }
        }
    }
}
