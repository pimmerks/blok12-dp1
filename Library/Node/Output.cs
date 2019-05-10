namespace Library.Node
{
    public class Output
    {
        public Output(Node node)
        {
            this.Node = node;
        }

        public void SetState(State newState)
        {
            this.state = newState;
        }

        public State GetState()
        {
            if (this.state == null)
            {
                this.Node.Calculate();
            }

            return this.state;
        }

        private State state;

        private Node Node { get; set; }
    }
}