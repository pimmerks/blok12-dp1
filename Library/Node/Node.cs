namespace Library.Node
{
    using System;
    using System.Collections.Generic;

    public abstract class Node
    {
        /// <summary>
        /// Creates a new <see cref="Node"/>.
        /// </summary>
        /// <param name="id">The id of the node.</param>
        /// <param name="inputs">A list of inputs.</param>
        protected Node(string id, List<Output> inputs)
        {
            this.Id = id;
            this.Inputs = inputs;
        }

        /// <summary>
        /// The id of this node.
        /// </summary>
        public string Id { get; set; }
        
        /// <summary>
        /// Calculates the <see cref="Outputs"/> based on the <see cref="Inputs"/>.
        /// </summary>
        protected abstract void CalculateCurrentNode();

        /// <summary>
        /// Calculates this and all previous nodes.
        /// </summary>
        public void Calculate()
        {
            Console.WriteLine($"[Node: {this.Id}] calculates");
            // Get inputs
            // ...

            this.CalculateCurrentNode();

            Console.WriteLine($"[Node: {this.Id}] returns TODO");
        }

        /// <summary>
        /// A list of outputs from all input nodes.
        /// </summary>
        public List<Output> Inputs { get; set; }
        
        /// <summary>
        /// A list of outputs from the current node.
        /// This list is updated after calling <see cref="Calculate"/>.
        /// </summary>
        public List<Output> Outputs { get; set; }
    }
}