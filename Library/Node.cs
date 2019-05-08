using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library
{
    public abstract class Node
    {

        public System.Collections.Generic.List<Library.Output> Inputs { get; set; }

        public string Id
        {
            get => default(string);
            set
            {
            }
        }

        public List<Library.Output> Output
        {
            get => default(Output);
            set
            {
            }
        }

        public State Calculate()
        {
            throw new System.NotImplementedException();
        }
    }
}