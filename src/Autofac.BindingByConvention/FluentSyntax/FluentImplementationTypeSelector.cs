﻿namespace Autofac.BindingByConvention.FluentSyntax
{
    using System;
    using System.Linq;

    using Autofac.BindingByConvention.Conventions;
    using Autofac.BindingByConvention.RegistrationOptions;
    using Autofac.Builder;
    using Autofac.Features.Scanning;

    /// <summary>
    /// A connector for the fluent syntax of type registration by convention.
    /// Selects the interfaces that need to be considered for the type binding, based on some criteria, exposed as methods.
    /// </summary>
    public class FluentImplementationTypeSelector
    {
        private readonly IRegistrationBuilder<object, ScanningActivatorData, DynamicRegistrationStyle> builder;

        private FluentContractFilter fluentContractFilter;

        /// <summary>
        /// Initializes a new instance of the <see cref="FluentImplementationTypeSelector"/> class.
        /// </summary>
        /// <param name="builder">The builder.</param>
        public FluentImplementationTypeSelector(IRegistrationBuilder<object, ScanningActivatorData, DynamicRegistrationStyle> builder)
        {
            this.builder = builder;
        }

        /// <summary>
        /// Names the matching.
        /// </summary>
        /// <param name="predicate">The predicate that need to be satisfied in order for the implementation to be considered.</param>
        /// <remarks>A number of helper class already provide some built-in <paramref name="predicate"/>s such as <see cref="InterfaceTypeName"/></remarks>
        /// <returns>The fluent connector for a proper fluent syntax.</returns>
        public FluentContractFilter NameMatching(Func<string, string, bool> predicate)
        {
            // The Where clause here is not a linq command. it adds a filter in the internal properties of the builder. The execution is not deferred.
            this.builder.Where(type => type.GetInterfaces().Any(i => predicate(i.Name, type.Name)));

            Func<Type, Type, bool> predicateInterfaceIsProperlyNamed = (contract, implementation) => predicate(contract.Name, implementation.Name);

            this.fluentContractFilter = new FluentContractFilter(this.builder, this, predicateInterfaceIsProperlyNamed);
            return this.fluentContractFilter;
        }

        /// <summary>
        /// Defines the registration strategy to apply on all types.
        /// </summary>
        /// <typeparam name="T">A class that identifies the registration strategy to apply.</typeparam>
        public void Instanciated<T>()
            where T : RegistrationStrategyBase, new()
        {
            var strategy = new T();
            this.builder.As(
                implementationType =>
                    {
                        var enumerable = implementationType.GetInterfaces().Where(
                            interfaceType =>
                                this.fluentContractFilter.Except.CheckAllFiltersSatisfied(
                                    interfaceType,
                                    implementationType)).ToArray();
                        return enumerable;
                    });

            strategy.Apply(this.builder);
        }
    }
}