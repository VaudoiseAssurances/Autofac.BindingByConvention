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
        private IRegistrationBuilder<object, ScanningActivatorData, DynamicRegistrationStyle> builder;

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

            // TODO: when moving to Autofac.BindingByConventions : replace with : instance.Apply(this.builder);
            var method = typeof(T).GetMethods(BindingFlags.Instance | BindingFlags.NonPublic)
                .Single(o => o.Name == "Apply");

            method.Invoke(instance, new[] { this.builder });
        }
    }
}