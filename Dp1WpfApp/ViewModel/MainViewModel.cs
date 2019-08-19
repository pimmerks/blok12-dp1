using DP1.Library.Factories;
using DP1.Library.File;
using DP1.Library.Simulation;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Shapes;

namespace Dp1WpfApp.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
            }
            else
            {
                // Code runs "for real"
                this.CurrentFilename = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "advanced-input.txt");
            }
        }

        public string Title { get; set; } = "DP1";

        public string CurrentFilename { get; set; } = "";

        public NodeSimulation CurrentSimulation { get; set; }

        /// <summary>
        /// Opens a file browser to display the files.
        /// </summary>
        public ICommand BrowseCommand => new RelayCommand(() => 
        {
            this.Title = "File chooser...";
            var dg = new OpenFileDialog();

            dg.DefaultExt = ".txt";
            dg.Filter = "Circuit files (*.txt)|*.txt|All files (*.*)|*.*";
            dg.InitialDirectory = Directory.GetCurrentDirectory();

            var result = dg.ShowDialog();
            
            if (result == true)
            {
                this.CurrentFilename = dg.FileName;
                this.Title = $"DP1 {this.CurrentFilename}";
            }
        });

        /// <summary>
        /// Loads the file specified in <see cref="CurrentFilename"/>.
        /// </summary>
        public ICommand LoadFile => new RelayCommand(() =>
        {
            if (!File.Exists(this.CurrentFilename))
            {
                MessageBox.Show($"The file {this.CurrentFilename} does not exist!");
                this.CurrentSimulation = null;
                return;
            }

            var path = this.CurrentFilename;

            var fp = new FileParser();
            var lines = fp.ReadFileLines(path);
            var (nodeDefinitions, nodeConnectionDefinitions) = fp.ParseLines(lines);

            var nodeFactory = new NodeFactory();
            var nodeConFactory = new NodeConnectionFactory();

            // Convert definitions to nodes
            var nodes = nodeDefinitions.Select(nodeFactory.CreateNode).ToList();

            // And node connections
            var nodeConnections =
                nodeConFactory.Convert(nodes, nodeConnectionDefinitions);

            this.CurrentSimulation = new NodeSimulation(nodeConnections);
        }, 
            () =>
        {
            if (string.IsNullOrWhiteSpace(this.CurrentFilename))
            {
                return false;
            }
            return true;
        });
    }
}