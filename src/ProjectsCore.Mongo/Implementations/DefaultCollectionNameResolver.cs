using ProjectsCore.Mongo.Interfaces;
using System;

namespace ProjectsCore.Mongo.Implementations
{
    public class DefaultCollectionNameResolver : ICollectionNameResolver
    {
        public string Resolve(object @object)
        {
            return @object.GetType().Name;
        }

        public string Resolve(Type type)
        {
            return type.Name;
        }
    }
}
