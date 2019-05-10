using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    using System.Runtime.CompilerServices;
    using Library.Node;
    using Library.Node.Nodes;

    class Program
    {
        static void Main(string[] args)
        {
            // Load text file
            // Parse text to node mapping

            // Set inputs
            // Run simulation
            // Get outputs

            var inputNode = new InputNode("IN_A", new List<Output>(), State.Make(true));

            var notNode = new NotNode("NOT_A", inputNode.Outputs);

            // var output = new OutputNode

            notNode.Calculate();

            Console.ReadLine();
        }
    }
}
