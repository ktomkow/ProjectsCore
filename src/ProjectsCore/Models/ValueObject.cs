using System;
using System.Linq;

namespace ProjectsCore.Models
{
    public abstract class ValueObject<T> : IEquatable<T>
        where T : class
    {
        public abstract bool Equals(T other);
        public override int GetHashCode()
        {
            Type type = typeof(T);

            var properties = type.GetProperties()
                .Where(x => x.PropertyType.IsPublic);

            unchecked
            {
                int hash = 8627;

                foreach (var property in properties)
                {
                    hash = hash * 12413 + property.GetValue(this).GetHashCode();
                }

                return hash;
            }
        }

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
