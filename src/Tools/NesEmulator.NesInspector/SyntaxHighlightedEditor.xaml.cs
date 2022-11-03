using ICSharpCode.AvalonEdit.Highlighting.Xshd;
using ICSharpCode.AvalonEdit.Highlighting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UserControl = System.Windows.Controls.UserControl;
using System.Xml;

namespace NesEmulator.NesInspector
{
    /// <summary>
    /// Interaction logic for SyntaxHighlightedEditor.xaml
    /// </summary>
    public partial class SyntaxHighlightedEditor : UserControl
    {
        public SyntaxHighlightedEditor()
        {
            InitializeComponent();
        }

        public bool IsReadOnly
        {
            get => avalonTextEditor.IsReadOnly;
            set => avalonTextEditor.IsReadOnly = value;
        }

        public string Text
        {
            get => avalonTextEditor.Text;
            set => avalonTextEditor.Text = value;
        }

        private string? syntaxHighlightingDefinitionResourceName;

        public string? SyntaxHighlightingDefinitionResourceName
        {
            get => syntaxHighlightingDefinitionResourceName;
            set
            {
                if (!string.IsNullOrEmpty(value) && !string.Equals(syntaxHighlightingDefinitionResourceName, value))
                {
                    syntaxHighlightingDefinitionResourceName = value;
                    using var stream = Assembly.GetAssembly(this.GetType())?.GetManifestResourceStream(value);
                    if (stream != null)
                    {
                        using var xmlReader = XmlReader.Create(stream);
                        avalonTextEditor.SyntaxHighlighting = HighlightingLoader.Load(xmlReader, HighlightingManager.Instance);
                    }
                }
            }
        }
    }
}
