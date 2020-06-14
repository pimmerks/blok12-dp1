namespace DP1.Library.Interfaces
{
    public interface IVisitable
    {
        void Accept(IVisitor visitor);
    }
}
