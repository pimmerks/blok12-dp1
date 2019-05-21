using System.Collections.Generic;

namespace DP1.Library.File
{
    public class NodeConnectionDefinition : ParsedLine
    {
        public string InputNode { get; }

        public List<string> OutputNodes { get; }

        public NodeConnectionDefinition(string inputNode, List<string> outputNodes)
        {
            this.InputNode = inputNode;
            this.OutputNodes = outputNodes;
        }
    }
}
