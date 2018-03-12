using Autofac.BindingByConvention.FluentSyntax;
using Autofac.Builder;
using Autofac.Features.Scanning;

namespace Autofac.BindingByConvention
{
    /// <summary>
    /// The entry point for the fluent notation allowing for registration by convention.
    /// </summary>
    public static class RegistratrionBuilderExtensions
    {
        /// <summary>
        /// The entry point for the fluent notation allowing for registration by convention.
        /// </summary>
        /// <param name="self">The registration builder.</param>
        /// <returns>The fluent connector for a proper fluent syntax.</returns>        
        public static FluentRoot ByConvention(this IRegistrationBuilder<object, ScanningActivatorData, DynamicRegistrationStyle> self)
        {
            return new FluentRoot(self);            
        }
    }
}