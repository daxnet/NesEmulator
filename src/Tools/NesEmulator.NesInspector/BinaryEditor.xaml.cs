using System;
using System.Collections.Generic;
using System.Linq;
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
using WpfHexaEditor;
using UserControl = System.Windows.Controls.UserControl;

namespace NesEmulator.NesInspector
{
    /// <summary>
    /// Interaction logic for HexEditor.xaml
    /// </summary>
    public partial class BinaryEditor : UserControl
    {
        public BinaryEditor()
        {
            InitializeComponent();
            
        }

        public HexEditor Editor => hexEditor;
    }
}
