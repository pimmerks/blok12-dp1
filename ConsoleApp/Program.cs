namespace ConsoleApp
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Runtime.CompilerServices;
    using System.IO;
    using DP1.Library.Facades;

    class Program
    {
        const string fileName = "advanced-input.txt";

        static void Main(string[] args)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), fileName);
            Console.WriteLine($"Loading from path {filePath}");

            if (!File.Exists(filePath))
            {
                Console.WriteLine("Cannot find file!");

                Console.ReadLine();
                return;
            }

            var sim = new SimulationFacade().LoadSimulation(filePath);

            Console.WriteLine("Loaded simulation");
            Console.WriteLine("State before:");
            var stateBefore = sim.GetOutputState();
            foreach (var item in stateBefore)
            {
                Console.WriteLine($"{item.Key} -> {item.Value}");
            }

            sim.RunSimulation();


            Console.WriteLine("State after:");
            var stateAfter = sim.GetOutputState();

            foreach (var item in stateAfter)
            {
                Console.WriteLine($"{item.Key} -> {item.Value}");
            }

            Console.WriteLine("Simulation complete");
            Console.ReadLine();
        }
    }
}
