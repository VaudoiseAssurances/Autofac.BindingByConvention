namespace Autofac.BindingByConvention.FluentSyntax
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class FluentExceptTypes : IFluentExceptTypes
    {
        private readonly FluentTypeFilter fluentTypeFilter;

        private readonly List<Func<Type, bool>> conditionsToFulfill;

        public FluentExceptTypes(FluentTypeFilter fluentTypeFilter, Func<Type, bool>[] predicates)
        {
            this.fluentTypeFilter = fluentTypeFilter;

            this.conditionsToFulfill = new List<Func<Type, bool>>(predicates);
        }

        public FluentTypeFilter Types(IEnumerable<Type> typesToIgnore)
        {
            this.conditionsToFulfill.Add(type => !typesToIgnore.Contains(type));
            return this.fluentTypeFilter;
        }

        public FluentTypeFilter TypesMatching(Func<Type, bool> predicate)
        {
            this.conditionsToFulfill.Add(predicate);
            return this.fluentTypeFilter;
        }

        public bool CheckAllFiltersSatisfied(Type implementationType)
        {
            return this.conditionsToFulfill.All(filter => filter(implementationType));
        }

        public FluentTypeFilter InheritsFrom<TAncestor>()
        {
            this.conditionsToFulfill.Add(type => !this.InheritsFromInternal(type, typeof(TAncestor)));
            return this.fluentTypeFilter;
        }

        private bool InheritsFromInternal(Type type, Type baseType)
        {
            // null does not have base type
            if (type == null)
            {
                return false;
            }

            // only interface can have null base type
            if (baseType == null)
            {
                return type.IsInterface;
            }

            // check implemented interfaces
            if (baseType.IsInterface)
            {
                return type.GetInterfaces().Contains(baseType);
            }

            // check all base types
            var currentType = type;
            while (currentType != null)
            {
                if (currentType.BaseType == baseType)
                {
                    return true;
                }

                currentType = currentType.BaseType;
            }

            return false;
        }
    }
}