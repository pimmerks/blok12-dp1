namespace Library.Node
{
    public class State
    {
        private State()
        {
        }

        public bool LogicState { get; set; } = false;

        public static State Make(bool logicState)
        {
            return new State
            {
                LogicState = logicState
            };
        }
    }
}