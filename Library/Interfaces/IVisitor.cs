namespace DP1.Library.Interfaces
{
    using DP1.Library.Nodes;

    public interface IVisitor
    {
        void Visit(NodeBase visitable);
    }
}
