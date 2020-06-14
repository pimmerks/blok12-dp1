namespace DP1.Library
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

        public override string ToString()
        {
            return $"State: {this.LogicState}";
        }
    }
}