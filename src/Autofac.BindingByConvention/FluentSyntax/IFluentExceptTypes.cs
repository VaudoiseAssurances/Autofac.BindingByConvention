namespace Autofac.BindingByConvention.FluentSyntax
{
    using System;

    public interface IFluentExceptTypes
    {
        bool CheckAllFiltersSatisfied(Type implementationType);
    }
}