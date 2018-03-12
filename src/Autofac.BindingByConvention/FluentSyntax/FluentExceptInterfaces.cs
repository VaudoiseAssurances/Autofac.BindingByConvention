using System;
using System.Collections.Generic;
using System.Linq;

namespace Autofac.BindingByConvention.FluentSyntax
{
    /// <inheritdoc />
    public class FluentExceptInterfaces : IFluentExceptInterfaces
    {
        private readonly FluentContractFilter fluentContractFilter;

        private readonly List<Func<Type, Type, bool>> filters;

        /// <inheritdoc />
        public FluentExceptInterfaces(FluentContractFilter fluentContractFilter, params Func<Type, Type, bool>[] predicates)
        {
            this.fluentContractFilter = fluentContractFilter;
            this.filters = new List<Func<Type, Type, bool>>(predicates);
        }

        /// <inheritdoc />
        public FluentContractFilter Types(IEnumerable<Type> contractsToIgnore)
        {
            this.filters.Add((interfaceType, implementationType) => !contractsToIgnore.Contains(interfaceType));           
            return this.fluentContractFilter;
        }

        /// <inheritdoc />
        public FluentContractFilter ContractsMarkedWith<T>() where T : Attribute
        {
            this.filters.Add((interfaceType, implementationType) => !interfaceType.IsDefined(typeof(T), false));           
            return this.fluentContractFilter;
        }

        /// <summary>
        /// Checks that all filters are satisfied.
        /// </summary>
        /// <param name="interfaceType">Type of the interface.</param>
        /// <param name="implementationType">Type of the implementation.</param>
        /// <returns><c>True</c> if all defined filters are satisfied; <c>False</c> otherwise.</returns>
        public bool CheckAllFiltersSatisfied(Type interfaceType, Type implementationType)
        {
            return this.filters.All(filter => filter(interfaceType, implementationType));
        }
    }
}