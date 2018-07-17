namespace Autofac.BindingByConvention.FluentSyntax
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Autofac.Builder;
    using Autofac.Features.Scanning;

    public class FluentSelfTypeSelector

    {
        private readonly IRegistrationBuilder<object, ScanningActivatorData, DynamicRegistrationStyle> builder;

        public FluentSelfTypeSelector(
            IRegistrationBuilder<object, ScanningActivatorData, DynamicRegistrationStyle> builder)

        {
            this.builder = builder;
        }

        public FluentTypeFilter IsAssignableTo<T>()

        {
            return new FluentTypeFilter(this.builder, new Func<Type, bool>[] { type => type.IsAssignableTo<T>() });
        }

        public FluentTypeFilter MatchingPredicate(Func<Type, bool> predicate)

        {
            return new FluentTypeFilter(this.builder, new[] { predicate });
        }

        public FluentContractFilter NameMatching(Func<string, string, bool> predicate)

        {
            this.builder.Where<object, ScanningActivatorData, DynamicRegistrationStyle>(
                (Func<Type, bool>)(type => ((IEnumerable<Type>)type.GetInterfaces()).Any<Type>(
                                          (Func<Type, bool>)(i => predicate(i.Name, type.Name)))));

            return new FluentContractFilter(
                this.builder,
                new Func<Type, Type, bool>[1]
                    {
                        (Func<Type, Type, bool>)((contract, implementation) =>
                                                        predicate(contract.Name, implementation.Name))
                    });
        }
    }
}