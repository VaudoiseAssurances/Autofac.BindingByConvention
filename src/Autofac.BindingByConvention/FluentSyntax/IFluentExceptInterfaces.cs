using System;
using System.Collections.Generic;

using Autofac.BindingByConvention.Attributes;

namespace Autofac.BindingByConvention.FluentSyntax
{
    /// <summary>
    /// Exposes some filtering methods that will be able to add exceptions to the type registrations to add to the IoC container, based on implementation and interface types.
    /// </summary>
    public interface IFluentExceptInterfaces
    {
        /// <summary>
        /// Ignores the interface types if they are contained in the specified <paramref name="contractsToIgnore" />.
        /// </summary>
        /// <param name="contractsToIgnore">The interface types to ignore.</param>
        /// <returns>The fluent connector for a proper fluent syntax.</returns>
        FluentContractFilter Types(IEnumerable<Type> contractsToIgnore);

        /// <summary>
        /// Ignores the interface types if they are decorated with the specified attribute <typeparamref name="TAttribute"/>.
        /// </summary>
        /// <typeparam name="TAttribute">The attribute that marks interface types that must be ignored by the described convention. For instance, <see cref="NoBindingByConventionAttribute"/></typeparam>
        /// <returns>The fluent connector for a proper fluent syntax.</returns>
        FluentContractFilter ContractsMarkedWith<TAttribute>() where TAttribute : Attribute;

        FluentContractFilter InheritsFrom<TAncestor>();
        FluentContractFilter TypesMatching(Func<Type,Type,bool> predicate);
    }
}