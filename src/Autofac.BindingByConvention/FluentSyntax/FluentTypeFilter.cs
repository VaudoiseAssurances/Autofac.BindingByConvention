namespace Autofac.BindingByConvention.FluentSyntax
{
    using System;
    using System.Linq;
    using System.Reflection;

    using Autofac.BindingByConvention.RegistrationOptions;
    using Autofac.Builder;
    using Autofac.Features.Scanning;

    public class FluentTypeFilter
    {
        private readonly IRegistrationBuilder<object, ScanningActivatorData, DynamicRegistrationStyle> builder;

        public FluentTypeFilter(
            IRegistrationBuilder<object, ScanningActivatorData, DynamicRegistrationStyle> builder,
            Func<Type, bool>[] predicates)
        {
            this.builder = builder;

            this.Except = new FluentExceptTypes(this, predicates);
        }

        protected IFluentExceptTypes Except { get; set; }

        public void Instanciated<T>()
            where T : RegistrationStrategyBase, new()
        {
            T instance = Activator.CreateInstance<T>();

            this.builder.AsSelf();

            instance.Apply(this.builder);            
        }
    }
}