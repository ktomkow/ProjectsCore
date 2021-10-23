using System;

namespace ProjectsCore.Mongo.Interfaces
{
    public interface ICollectionNameResolver
    {
        string Resolve(object @object);

        string Resolve(Type type);
    }
}
