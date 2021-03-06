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
    using System;

    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            if (IsInDesignMode)
            {
                this.CurrentFilename = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "");
                this.RaisePropertyChanged(nameof(CurrentFilename));
            }
            else
            {
            }
        }

        public string Title { get; set; } = "DP1";

        public string CurrentFilename { get; set; } = "";

        public NodeSimulation CurrentSimulation { get; set; }

        public List<InputNode> InputNodes => this.CurrentSimulation?.GetInputNodes();

        public Dictionary<string, State> OutputState => this.CurrentSimulation?.GetOutputState();

        public List<NodeConnection> NodeConnections => this.CurrentSimulation?.NodeConnections;

        public string PropagationDelay => $"Delay time: {this.CurrentSimulation?.DelayTime} * 15ns = {this.CurrentSimulation?.DelayTime * 15}ns";

        public bool RunSimulationOnUpdate { get; set; } = true;

        /// <summary>
        /// Opens a file browser to display the files.
        /// </summary>
        public ICommand BrowseCommand => new RelayCommand(() => 
        {
            this.Title = "File chooser...";
            var dg = new OpenFileDialog
            {
                DefaultExt = ".txt",
                Filter = "Circuit files (*.txt)|*.txt|All files (*.*)|*.*",
                InitialDirectory = Directory.GetCurrentDirectory()
            };

            var result = dg.ShowDialog();
            
            if (result == true)
            {
                this.CurrentFilename = dg.FileName;
                this.Title = $"DP1 - {this.CurrentFilename}";
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
            // if (!File.Exists(this.CurrentFilename))
            // {
            //     MessageBox.Show($"The file {this.CurrentFilename} does not exist!");
            //     this.CurrentSimulation = null;
            //     return;
            // }
            //
            // var path = this.CurrentFilename;
            //
            // var fp = new FileParser();
            // var lines = fp.ReadFileLines(path);
            // var (nodeDefinitions, nodeConnectionDefinitions) = fp.ParseLines(lines);
            //
            // var nodeFactory = NodeFactory.Instance;
            // var nodeConFactory = new NodeConnectionFactory();
            //
            // // Convert definitions to nodes
            // var nodes = nodeDefinitions.Select(nodeFactory.CreateNode).ToList();
            //
            // // And node connections
            // var nodeConnections =
            //     nodeConFactory.Convert(nodes, nodeConnectionDefinitions);

            var nodeConnections = new List<NodeConnection>();
            
            try
            {
                nodeConnections = FileFacade.GetNodeConnectionsFromFile(this.CurrentFilename);
            }
            catch (Exception e)
            {
                MessageBox.Show($"The file {this.CurrentFilename} does not exist!");
                this.CurrentSimulation = null;
                return;
            }

            var sim = NodeSimulationBuilder.GetBuilder()
                        .AddNodeConnections(nodeConnections)
                        .Build();

            try
            {
                sim.ValidSimulationCheck();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                MessageBox.Show(e.Message, e.GetType().Name);
                this.CurrentSimulation = null;
                return;
            }

            this.CurrentSimulation = sim;

        }, () => !string.IsNullOrWhiteSpace(this.CurrentFilename));

        public ICommand ResetAndStart => new RelayCommand(() =>
        {
            this.CurrentSimulation.ResetSimulation();
            this.CurrentSimulation.RunSimulation();
            this.RaisePropertyChanged(nameof(this.OutputState));
        }, () => this.CurrentSimulation != null);

        /// <summary>
        /// Sets the new state
        /// </summary>
        public ICommand SetNewState => new RelayCommand<InputNode>((node) =>
        {
            this.CurrentSimulation.SetInputs(new Dictionary<string, State>() { { node.NodeId, new State(!node.CurrentState.LogicState) } });
            this.RaisePropertyChanged(nameof(this.InputNodes));
            this.RaisePropertyChanged(nameof(this.NodeConnections));

            if (this.RunSimulationOnUpdate && this.ResetAndStart.CanExecute(null))
            {
                this.ResetAndStart.Execute(null);
            }
        });
    }
}