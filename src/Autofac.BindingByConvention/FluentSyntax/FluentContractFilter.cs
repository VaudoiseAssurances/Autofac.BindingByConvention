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
        /// <param name="selector"></param>
        /// <param name="predicates">The predicates to apply in any case.</param>
        public FluentContractFilter(
            IRegistrationBuilder<object, ScanningActivatorData, DynamicRegistrationStyle> builder,
            FluentImplementationTypeSelector selector,
            params Func<Type, Type, bool>[] predicates)
        {
            this.builder = builder;
            this.Except = new FluentExceptInterfaces(this, predicates);
            this.AndTypesWillBe = selector;
        }

        public FluentImplementationTypeSelector AndTypesWillBe { get; set; }

        /// <summary>
        /// Gets the fluent connector to add exceptions to the interface types being considered for binding by convention.
        /// </summary>
        /// <value>
        /// The connector for a proper fluent syntax.
        /// </value>
        public IFluentExceptInterfaces Except { get; }
    }
}