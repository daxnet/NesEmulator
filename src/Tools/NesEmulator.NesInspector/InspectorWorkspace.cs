using FormsUI.Workspaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesEmulator.NesInspector
{
    internal sealed class InspectorWorkspace : Workspace
    {
        protected override string WorkspaceFileDescription => "NES Inspector Workspace";

        protected override string WorkspaceFileExtension => "niw";

        protected override IWorkspaceModel Create() => new InspectorModel();

        protected override IWorkspaceModel OpenFromFile(string fileName) => new InspectorModel(fileName);

        protected override void SaveToFile(IWorkspaceModel model, string fileName) { }
    }
}
