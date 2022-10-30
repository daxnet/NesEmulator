using FormsUI.Windows;
using FormsUI.Workspaces;
using NesEmulator.NesInspector.Properties;
using WeifenLuo.WinFormsUI.Docking;

namespace NesEmulator.NesInspector
{
    public partial class FrmMain : AppWindow
    {
        #region Private Fields

        private ToolAction? _newTool;
        private ToolAction? _openTool;

        #endregion Private Fields

        #region Public Constructors

        public FrmMain()
        {
            InitializeComponent();
        }

        #endregion Public Constructors

        #region Public Properties

        public override DockPanel DockArea => dockPanel;

        #endregion Public Properties

        #region Protected Methods

        protected override Workspace CreateWorkspace() => new InspectorWorkspace();

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            RegisterTools();
            RegisterToolWindows();

        }

        protected override void OnWorkspaceCreated(object sender, WorkspaceCreatedEventArgs e)
        {
            WindowManager.GetFirstWindow<FrmNesExplorer>()?.Show();
        }

        protected override void OnWorkspaceOpened(object sender, WorkspaceOpenedEventArgs e)
        {
            //InspectorModel model = e.Model as InspectorModel ?? throw new Exception();
            //var window = WindowManager.CreateWindow<FrmInspectorWindow>(model);
            //window.Text = Path.GetFileName(model.FileName);
            //window.Show(DockArea, DockState.Document);
        }

        #endregion Protected Methods

        #region Private Methods

        private void Action_New(ToolAction action)
        {
            Workspace.New(model =>
            {
                var inspectorModel = model as InspectorModel ?? new InspectorModel();
                if (openNESFileDialog.ShowDialog(this) == DialogResult.OK)
                {
                    try
                    {
                        inspectorModel.FileName = openNESFileDialog.FileName;
                        return (true, inspectorModel);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return (false, default);
                    }
                }
                return (false, default);
            });
        }

        private void Action_Open(ToolAction action)
        {
            Workspace.Open();
        }

        private void RegisterTools()
        {
            _newTool = RegisterTool(
                "newTool",
                "&New...",
                new ToolStripItem[] { mnuNew, tbtnNew },
                Action_New,
                tooltipText: "Create a new NES Inspector workspace.",
                image: Resources.page_white,
                shortcutKeys: Keys.Control | Keys.N);

            _openTool = RegisterTool(
                "openTool",
                "&Open...",
                new ToolStripItem[] { mnuOpen, tbtnOpen },
                Action_Open,
                tooltipText: "Opens an existing workspace.",
                image: Resources.folder,
                shortcutKeys: Keys.Control | Keys.O);
        }

        private void RegisterToolWindows()
        {
            RegisterToolWindow<FrmNesExplorer>(new ToolStripItem[] { mnuNESExplorer, tbtnNESExplorer },
                "NES Explorer",
                Resources.application_side_tree,
                DockState.DockLeft, true, "Show or hide the NES explorer.", Keys.F12);
        }

        #endregion Private Methods
    }
}