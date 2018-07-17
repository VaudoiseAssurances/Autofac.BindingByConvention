namespace Autofac.BindingByConvention.FluentSyntax
{
    using System;
    using System.Collections.Generic;

    using Autofac.BindingByConvention.Attributes;

    /// <summary>
    /// Exposes some filtering methods that will be able to add exceptions to the type registrations to add to the IoC container, based on implementation and interface types.
    /// </summary>
    public interface IFluentExceptInterfaces
    {
        /// <summary>
        /// Exclude the types from the binding convention if they are decorated with the specified attribute <typeparamref name="TAttribute"/>.
        /// </summary>
        /// <typeparam name="TAttribute">The attribute that marks interface types that must be ignored by the described convention. For instance, <see cref="NoBindingByConventionAttribute"/></typeparam>
        /// <returns>The fluent connector for a proper fluent syntax.</returns>
        FluentContractFilter ContractsMarkedWith<TAttribute>()
            where TAttribute : Attribute;

        /// <summary>
        /// Exclude the types from the binding convention if they inherit from the specified ancestor <typeparamref name="TAncestor"/>.
        /// </summary>
        /// <typeparam name="TAncestor">The type of the ancestor; all classes inheriting from it will be ignored.</typeparam>
        /// <returns>The fluent connector for a proper fluent syntax.</returns>
        FluentContractFilter InheritsFrom<TAncestor>();

        /// <summary>
        /// Exclude the types from the binding convention if they are contained in the specified <paramref name="contractsToIgnore" />.
        /// </summary>
        /// <param name="contractsToIgnore">The interface types to ignore.</param>
        /// <returns>The fluent connector for a proper fluent syntax.</returns>
        FluentContractFilter Types(IEnumerable<Type> contractsToIgnore);

        /// <summary>
        /// Exclude the types from the binding convention if they match the specified <paramref name="predicate"/>.
        /// </summary>
        /// <param name="predicate">The predicate; all classes matching it will be ignored.</param>
        /// <returns>The fluent connector for a proper fluent syntax.</returns>
        FluentContractFilter TypesMatching(Func<Type, Type, bool> predicate);
    }
}