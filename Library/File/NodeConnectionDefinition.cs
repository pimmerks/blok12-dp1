using System;
using System.Collections.Generic;
using System.Linq;

namespace DP1.Library.File
{
    using Exceptions;

    public class NodeConnectionDefinition : ParsedLine
    {
        public string InputNode { get; }

        public List<string> OutputNodes { get; }

        public NodeConnectionDefinition(string inputNode, List<string> outputNodes)
        {
            if (outputNodes.GroupBy(x => x).Any(g => g.Count() > 1))
            {
                // Output nodes contains a duplicate
                throw new DuplicateNodeException("The output contains duplicate nodes!");
            }

            this.InputNode = inputNode;
            this.OutputNodes = outputNodes;
        }
    }
}
