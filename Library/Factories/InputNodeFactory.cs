namespace DP1.Library.Factories
{
    using System;
    using DP1.Library.Nodes;

    public class InputNodeFactory
    {
        private static InputNodeFactory instance;
        public static InputNodeFactory Instance =>
            InputNodeFactory.instance ?? (InputNodeFactory.instance = new InputNodeFactory());
        
        public InputNode CreateNode(string id, string nodeType)
        {
            switch (nodeType)
            {
                case "INPUT_LOW":
                    return new InputNode(id, new State(false));
                case "INPUT_HIGH":
                    return new InputNode(id, new State(true));
                default:
                    throw new ArgumentException($"Node type not found. Error thrown for node: {id}.");
            }
        }

        public bool ContainsType(string type)
        {
            return type == "INPUT_LOW" || type == "INPUT_HIGH";
        }
    }
}
