namespace Library.Node
{
    using System.Collections.Generic;

    public abstract class Node
    {

        public System.Collections.Generic.List<Output> Inputs { get; set; }

        public string Id { get; set; }

        public List<Output> Output { get; set; }

        public State Calculate()
        {
            throw new System.NotImplementedException();
        }
    }
}