using Autofac.Builder;
using Autofac.Features.Scanning;

namespace Autofac.BindingByConvention.RegistrationOptions
{
    /// <summary>
    /// Defines that the registered types will be instantiated for each resolved dependency.
    /// </summary>
    /// <seealso cref="RegistrationStrategyBase" />
    public class PerDependency : RegistrationStrategyBase
    {
        /// <inheritdoc />
        internal override void Apply(IRegistrationBuilder<object, ScanningActivatorData, DynamicRegistrationStyle> builder)
        {
            builder.InstancePerDependency();
        }
    }
}