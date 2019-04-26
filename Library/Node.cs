using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library
{
    public abstract class Node
    {
        public List<Node> Inputs { get; set; }

        public string Id
        {
            get => default(int);
            set
            {
            }
        }

        public State GetState()
        {
            throw new System.NotImplementedException();
        }
    }
}