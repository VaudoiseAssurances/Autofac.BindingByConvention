﻿namespace Autofac.BindingByConvention.Attributes
{
    using System;

    /// <inheritdoc />
    /// <summary>
    /// Marks a class as not being subject to being registered in an IoC container by convention.
    /// </summary>
    /// <seealso cref="T:System.Attribute" />
    [AttributeUsage(AttributeTargets.Interface)]
    public class NoBindingByConventionAttribute : Attribute
    {
    }
}