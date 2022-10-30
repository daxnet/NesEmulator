using FormsUI.Workspaces;
using NesEmulator.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesEmulator.NesInspector
{
    public sealed class InspectorModel : IWorkspaceModel
    {
        private string _fileName = string.Empty;

        public InspectorModel()
        {
            
        }

        public InspectorModel(string fileName)
        {
            FileName = fileName;
        }

        public WorkspaceModelVersion Version { get; set; } = WorkspaceModelVersion.One;

        public event PropertyChangedEventHandler? PropertyChanged;

        public string FileName
        {
            get { return _fileName; }
            set
            {
                _fileName = value;
                Cartridge = new Cartridge(_fileName);
            }
        }

        [JsonIgnore]
        public Cartridge? Cartridge { get; set; }
    }
}
