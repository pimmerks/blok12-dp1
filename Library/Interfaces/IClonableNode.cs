namespace DP1.Library.Interfaces
{
    using DP1.Library.Nodes;

    public interface IClonableNode<T>
    {
        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <param name="newId">The new id for the node.</param>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        T Clone(string newId);
    }
    
    public interface IVisitable
    {
        void Accept(IVisitor visitor);
    }
    
    /// <summary>
    /// All visitors should be declared in this file.
    /// </summary>
    public interface IVisitor
    {
        void Visit(NodeBase visitable);
    }
}
