using System;

namespace Autofac.BindingByConvention.Conventions
{
    /// <summary>
    /// A class exposing conventions based on the name of the type of an interface.
    /// </summary>
    public static class InterfaceTypeName
    {
        /// <summary>
        /// Determines whether the specified <paramref name="interfaceTypeName"/> is the same as the specified <paramref name="implementationTypeName"/>, only with a leading "I".
        /// </summary>
        /// <param name="interfaceTypeName">Name of the interface type.</param>
        /// <param name="implementationTypeName">Name of the implementation type.</param>
        /// <returns>
        ///   <c>true</c> if it matches the implementation type with the addition of a leading i; otherwise, <c>false</c>.
        /// </returns>
        public static bool HasLeadingI(string interfaceTypeName, string implementationTypeName)
        {
            return interfaceTypeName.Equals($"I{implementationTypeName}", StringComparison.OrdinalIgnoreCase);
        }
    }
}