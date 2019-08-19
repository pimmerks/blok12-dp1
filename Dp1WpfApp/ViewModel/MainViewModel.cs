using DP1.Library;
using DP1.Library.Factories;
using DP1.Library.File;
using DP1.Library.Nodes;
using DP1.Library.Simulation;
using Dp1WpfApp.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using System.Collections.Generic;
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
            }
        }

        public string Title { get; set; } = "DP1";

        public string CurrentFilename { get; set; } = "";

        public NodeSimulation CurrentSimulation { get; set; }

        public List<InputNode> InputNodes => this.CurrentSimulation?.GetInputNodes();

        public Dictionary<string, State> OutputState => this.CurrentSimulation?.GetOutputState();

        public List<NodeConnection> NodeConnections => this.CurrentSimulation?.NodeConnections;

        public string PropegationDelay => $"Delay time: {this.CurrentSimulation?.DelayTime} * 15ns = {this.CurrentSimulation?.DelayTime * 15}ns";

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

            if (this.LoadFile.CanExecute(null))
            {
                this.LoadFile.Execute(null);
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

            var sim = NodeSimulationBuilder.GetBuilder()
                        .AddNodeConnections(nodeConnections)
                        .Build();

            var check = sim.ValidSimulationCheck();
            if (!string.IsNullOrWhiteSpace(check))
            {
                MessageBox.Show(check);
                this.CurrentSimulation = null;
                return;
            }

            this.CurrentSimulation = sim;
        }, 
            () =>
        {
            if (string.IsNullOrWhiteSpace(this.CurrentFilename))
            {
                return false;
            }
            return true;
        });

        public ICommand ResetAndStart => new RelayCommand(() =>
        {
            this.CurrentSimulation.ResetSimulation();
            this.CurrentSimulation.RunSimulation();
            this.RaisePropertyChanged(nameof(this.OutputState));
        }, () =>
        {
            return this.CurrentSimulation != null;
        });

        /// <summary>
        /// Sets the new state
        /// </summary>
        public ICommand SetNewState => new RelayCommand<InputNode>((node) =>
        {
            // Open dialog to define new state.
            this.CurrentSimulation.SetInputs(new Dictionary<string, State>() { { node.NodeId, new State(!node.CurrentState.LogicState) } });
            this.RaisePropertyChanged(nameof(this.InputNodes));
        });
    }
}