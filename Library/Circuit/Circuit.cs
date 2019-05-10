namespace Library.Circuit
{
    using System.Collections.Generic;
    using Node;

    public class Circuit : Node
    {
        public List<Node> InputNodes { get; set; }

        public List<Node> OutputNodes { get; set; }

        public void Calculate()
        {
            throw new System.NotImplementedException();
        }
    }
}