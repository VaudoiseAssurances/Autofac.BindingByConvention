namespace Autofac.BindingByConvention.FluentSyntax
{
    using System;
    using System.Collections.Generic;

    class FluentExceptTypes : IFluentExceptTypes
    {
        private readonly FluentTypeFilter fluentTypeFilter;

        private List<Func<Type, bool>> conditionsToFulfill;

        public FluentExceptTypes(FluentTypeFilter fluentTypeFilter, Func<Type, bool>[] predicates)
        {
            this.fluentTypeFilter = fluentTypeFilter;

            this.conditionsToFulfill = new List<Func<Type, bool>>(predicates);
        }

        public bool CheckAllFiltersSatisfied(Type implementationType)
        {
            throw new NotImplementedException();
        }
    }
}