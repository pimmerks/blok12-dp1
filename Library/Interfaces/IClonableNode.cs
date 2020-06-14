namespace DP1.Library.Interfaces
{
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
}
