namespace ProjectsCore.Models
{
    /// <summary>
    /// General usage entity class based on IEntity interface
    /// </summary>
    /// <typeparam name="T">Id type</typeparam>
    public abstract class Entity<T> where T : struct
    {
        public string IdAsString => this.Id.ToString();

        public T Id { get; protected set; }
    }
}
