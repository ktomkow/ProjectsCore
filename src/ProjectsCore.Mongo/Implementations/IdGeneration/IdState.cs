using System;

namespace ProjectsCore.Mongo.Implementations.IdGeneration
{
    public abstract class IdState<T> where T : struct
    {
        public string Id { get; protected set; }

        public T Value { get; protected set; }

        protected IdState() { }

        protected IdState(Type type)
        {
            Id = type.Name;
        }

        protected IdState(string typeName)
        {
            Id = typeName;
        }

        public abstract T Tick();
    }
}
