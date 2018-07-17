namespace Autofac.BindingByConvention.FluentSyntax
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <inheritdoc />
    public class FluentExceptInterfaces : IFluentExceptInterfaces
    {
        /// <summary>
        /// The conditions to fulfill for a type not to be excluded.
        /// </summary>
        private readonly List<Func<Type, Type, bool>> conditionsToFulfill;

        private readonly FluentContractFilter fluentContractFilter;

        /// <inheritdoc />
        public FluentExceptInterfaces(
            FluentContractFilter fluentContractFilter,
            params Func<Type, Type, bool>[] predicates)
        {
            this.fluentContractFilter = fluentContractFilter;
            this.conditionsToFulfill = new List<Func<Type, Type, bool>>(predicates);
        }

        /// <summary>
        /// Checks that all conditionsToFulfill are satisfied.
        /// </summary>
        /// <param name="interfaceType">Type of the interface.</param>
        /// <param name="implementationType">Type of the implementation.</param>
        /// <returns><c>True</c> if all defined conditionsToFulfill are satisfied; <c>False</c> otherwise.</returns>
        public bool CheckAllFiltersSatisfied(Type interfaceType, Type implementationType)
        {
            return this.conditionsToFulfill.All(filter => filter(interfaceType, implementationType));
        }

        /// <inheritdoc />
        public FluentContractFilter ContractsMarkedWith<T>()
            where T : Attribute
        {
            this.conditionsToFulfill.Add(
                (interfaceType, implementationType) => !interfaceType.IsDefined(typeof(T), false));
            return this.fluentContractFilter;
        }

        public FluentContractFilter InheritsFrom<TAncestor>()
        {
            this.conditionsToFulfill.Add(
                (interfaceType, implementationType) =>
                    !this.InheritsFromInternal(implementationType, typeof(TAncestor)));
            return this.fluentContractFilter;
        }

        /// <inheritdoc />
        public FluentContractFilter Types(IEnumerable<Type> contractsToIgnore)
        {
            this.conditionsToFulfill.Add(
                (interfaceType, implementationType) => !contractsToIgnore.Contains(interfaceType));
            return this.fluentContractFilter;
        }

        public FluentContractFilter TypesMatching(Func<Type, Type, bool> predicate)
        {
            this.conditionsToFulfill.Add(predicate);
            return this.fluentContractFilter;
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