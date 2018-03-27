# Autofac.BindingByConvention
|master branch|develop|NuGet|
|:---:|:---:|:---:|
|![master badge state](https://ci.appveyor.com/api/projects/status/github/vaudoiseassurances/autofac.bindingbyconvention?branch=master&svg=true)|![develop badge state](https://ci.appveyor.com/api/projects/status/github/vaudoiseassurances/autofac.bindingbyconvention?branch=develop&svg=true)|[![NuGet version](https://img.shields.io/nuget/v/Autofac.bindingbyconvention.svg)](https://badge.fury.io/nu/Autofac.bindingbyconvention)|


Manually registering types in an IoC container can be a tedious task. This extension to Autofac aims to help automatically registering types with common, built-in and extensible conventions.

## How to use it
### Installation
Install with NuGet. See [NuGet package page](https://www.nuget.org/packages/Autofac.BindingByConvention) for more information. 

## Usage and examples

The following example will create a new ContainerBuilder and

- Register assembly types located in a list of assemblies by convention...
- ... to the contracts (interfaces) that have...
- ... a name matching a convention where the interface's type name has a leading ``I`` ...
- ... except if this interface type is listed as a specified list of interfaces to ignore ...
- ... and except if the contracts (interfaces) are marked with the ``NoBindingByConventionAttribute`` attribute...
- ... Instances will be instanciated per-dependency (each resolved type is a different instance.)




```csharp
var builder = new ContainerBuilder();

builder
    .RegisterAssemblyTypes(listOfAssemblies)
    .ByConvention().ToTheContractsWith()
        .NameMatching(InterfaceTypeName.HasLeadingI)
        .Except
            .Types(listOfInterfacesToIgnore)
        .Except
            .ContractsMarkedWith<NoBindingByConventionAttribute>()
        .Instanciated<PerDependency>();

```


## Contribute

### Build/Run
Build the project with dotnet standard 2
