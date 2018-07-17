namespace Autofac.BindingByConvention.FluentSyntax
{
    using System;
    using System.Linq;

    using Autofac.BindingByConvention.RegistrationOptions;
    using Autofac.Builder;
    using Autofac.Features.Scanning;

    public class FluentSelfTypeSelector
    {
        private readonly IRegistrationBuilder<object, ScanningActivatorData, DynamicRegistrationStyle> builder;

        public FluentSelfTypeSelector(
            IRegistrationBuilder<object, ScanningActivatorData, DynamicRegistrationStyle> builder)
        {
            this.builder = builder;
        }

        public FluentTypeFilter IfTypeIsAssignableTo<T>()
        {
            return new FluentTypeFilter(this.builder, this,  new Func<Type, bool>[] { type => type.IsAssignableTo<T>() });
        }

        public FluentTypeFilter MatchingPredicate(Func<Type, bool> predicate)
        {
            return new FluentTypeFilter(this.builder, this,  new[] { predicate });
        }

        public void Instanciated<T>()
            where T : RegistrationStrategyBase, new()
        {
            T instance = Activator.CreateInstance<T>();

            this.builder.AsSelf();

            instance.Apply(this.builder);
        }
    }
}