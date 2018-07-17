namespace Autofac.BindingByConvention.FluentSyntax
{
    using System;
    using System.Linq;

    using Autofac.BindingByConvention.RegistrationOptions;
    using Autofac.Builder;
    using Autofac.Features.Scanning;

    /// <summary>
    /// A connector for the fluent syntax of type registration by convention. Exposes a way to add exceptions to the already-configured types to consider for registration, and allows to define the 
    /// </summary>
    public class FluentContractFilter
    {
        private readonly IRegistrationBuilder<object, ScanningActivatorData, DynamicRegistrationStyle> builder;

        /// <summary>
        /// Initializes a new instance of the <see cref="FluentContractFilter"/> class.
        /// </summary>
        /// <param name="builder">The registration builder.</param>
        /// <param name="predicates">The predicates to apply in any case.</param>
        public FluentContractFilter(
            IRegistrationBuilder<object, ScanningActivatorData, DynamicRegistrationStyle> builder,
            params Func<Type, Type, bool>[] predicates)
        {
            this.builder = builder;
            this.Except = new FluentExceptInterfaces(this, predicates);
        }

        /// <summary>
        /// Gets the fluent connector to add exceptions to the interface types being considered for binding by convention.
        /// </summary>
        /// <value>
        /// The connector for a proper fluent syntax.
        /// </value>
        public IFluentExceptInterfaces Except { get; }

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
                                ((FluentExceptInterfaces)this.Except).CheckAllFiltersSatisfied(
                                    interfaceType,
                                    implementationType)).ToArray();
                        return enumerable;
                    });

            strategy.Apply(this.builder);
        }
    }
}