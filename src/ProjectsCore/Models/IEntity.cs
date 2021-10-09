namespace ProjectsCore.Models
{
    /// <summary>
    /// Base entity interface
    /// </summary>
    /// <typeparam name="T">Id type</typeparam>
    public interface IEntity<T> where T : struct
    {
        public T Id { get; }

        public string IdAsString => this.Id.ToString();
    }
}
