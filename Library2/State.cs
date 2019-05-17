namespace Library2
{
    public class State
    {
        public State()
        {
        }

        public State(bool logicState)
        {
            this.LogicState = logicState;
        }

        public bool LogicState { get; set; }
    }
}