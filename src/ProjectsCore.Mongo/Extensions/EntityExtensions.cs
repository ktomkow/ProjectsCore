using ProjectsCore.Models;
using System;
using System.Reflection;

namespace ProjectsCore.Mongo.Extensions
{
    internal static class EntityExtensions
    {
        internal static void SetId<TKey, TEntity>(this TEntity entity, TKey key) where TKey : struct where TEntity : Entity<TKey>
        {
            Type entityType = entity.GetType();

            PropertyInfo propertyInfo = entityType.GetProperty("Id");
            if (propertyInfo == null)
            {
                throw new Exception($"Id setter does not exists for type: {entityType.Name}");
            }
            propertyInfo.SetValue(entity, key);
        }
    }
}
