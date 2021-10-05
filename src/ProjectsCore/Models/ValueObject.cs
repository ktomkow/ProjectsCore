namespace ProjectsCore.Models
{
    public abstract class ValueObject<T> where T : class
    {
        public abstract bool Equals(T other);
        public abstract override int GetHashCode();

        public override bool Equals(object obj)
        {
            if (!(obj is T other))
            {
                return false;
            }

            return this.Equals(other);
        }
    }
}
