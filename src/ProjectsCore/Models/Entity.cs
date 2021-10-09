namespace ProjectsCore.Models
{
    /// <summary>
    /// General usage entity class based on IEntity interface
    /// </summary>
    /// <typeparam name="T">Id type</typeparam>
    public abstract class Entity<T> : IEntity<T> where T : struct
    {
        protected T id;

        public T Id => this.Id;
    }
}
