namespace Autofac.BindingByConvention.FluentSyntax
{
    using Autofac.BindingByConvention.RegistrationOptions;
    using Autofac.Builder;
    using Autofac.Features.Scanning;

    public class PerLifetimeScope : RegistrationStrategyBase
    {
        internal override void Apply(IRegistrationBuilder<object, ScanningActivatorData, DynamicRegistrationStyle> builder)
        {
            builder.InstancePerLifetimeScope();
        }
    }
}