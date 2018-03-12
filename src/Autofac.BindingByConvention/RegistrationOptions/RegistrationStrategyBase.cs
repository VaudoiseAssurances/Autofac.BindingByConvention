using Autofac.Builder;
using Autofac.Features.Scanning;

namespace Autofac.BindingByConvention.RegistrationOptions
{
    /// <summary>
    /// The base class for all classes that describe a registration strategy, such as "once per dependency", or "singleton"
    /// </summary>
    public abstract class RegistrationStrategyBase
    {
        /// <summary>
        /// Applies the strategy to the specified registration builder.
        /// </summary>
        /// <param name="builder">The registration builder.</param>
        internal abstract void Apply(IRegistrationBuilder<object, ScanningActivatorData, DynamicRegistrationStyle> builder);
    }
}