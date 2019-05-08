using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library
{
    public abstract class Node
    {

        public System.Collections.Generic.List<Library.Outputt> Inputs { get; set; }

        public string Id
        {
            get => default(int);
            set
            {
            }
        }

        public List<Library.Outputt> Outputt
        {
            get => default(Outputt);
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