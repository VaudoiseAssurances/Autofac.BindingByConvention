using Autofac.Builder;
using Autofac.Features.Scanning;

namespace Autofac.BindingByConvention.FluentSyntax
{
    /// <summary>
    /// The root connector for the fluent syntax. Allows to select the primary selector for the convention. (based on the contract or based on the implementation).
    /// </summary>
    public class FluentRoot
    {
        private readonly IRegistrationBuilder<object, ScanningActivatorData, DynamicRegistrationStyle> builder;

        /// <summary>
        /// Initializes a new instance of the <see cref="FluentRoot"/> class.
        /// </summary>
        /// <param name="builder">The registration builder.</param>
        public FluentRoot(IRegistrationBuilder<object, ScanningActivatorData, DynamicRegistrationStyle> builder)
        {
            this.builder = builder;
        }

        /// <summary>
        /// Starts a convention based on the interface type as a primary selector.
        /// </summary>
        /// <returns>The fluent connector for a proper fluent syntax.</returns>
        public FluentImplementationTypeSelector ToTheContractsWith()
        {
            return new FluentImplementationTypeSelector(this.builder);
        }
    }
}