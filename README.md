ApplicationRegistries
======================

[To Japanese](https://github.com/banban525/ApplicationRegistries/blob/master/README.ja.md)

ApplicaitonRegistries is a .NET library that will proxy the application's external settings.

[![Build status](https://ci.appveyor.com/api/projects/status/a0004gg2p46y94ia?svg=true)](https://ci.appveyor.com/project/banban525/applicationregistries)
[![NuGet version](https://badge.fury.io/nu/ApplicationRegistries.svg)](https://badge.fury.io/nu/ApplicationRegistries)
[![NuGet Download](https://img.shields.io/nuget/dt/ApplicationRegistries.svg)](https://www.nuget.org/packages/ApplicationRegistries)
[![MIT License](https://img.shields.io/github/license/banban525/ApplicationRegistries.svg)](LICENSE)

## Description

When developing a Windows application, several parameters are implemented so that you can change it later.
For example, there are a timeout time which is a parameter affected by specs, and a port number which is a parameter utilizing resources of the entire PC.
The general method is as follows.
* setting file
* Environment variable
* Commandline arguments
* Registry (Windows case)
* Application configuration file(*.exe.config)

You can choose freely, but these are the same for the application and are "external settings".

However, the implementation method is different.
For example, you need to implement the commandline argument parser yourself.
A null check is required in the registry.
It is not easy to use.

Also, implementing the loading process in various places makes it difficult to list external settings.
Besides that, the implementation is not the same each time.

ApplicationRegistries acts as a proxy for the "External settings" listed above.
"External setting value" can be read with simple usage and with priority order.
You can also output a list report of "External settings".


## Quick Start

The following code is an example of usage

```csharp
[ApplicationRegistry]
public interface ISettings
{
  /// <Summary>
  /// Listen Port Number
  /// </Summary>
  [DefaultValue(80)]
  int PortNo { get; }

  /// <Summary>
  /// Backup to
  /// </Summary>
  string BackupFolder { get; }
}

class Program
{
  static void Main()
  {
    int portNo = ApplicationRegistry.Get<ISettings>().PortNo;
  }
}

```

## Dependency

ApplicationRegistries2.dll depends only on .NET Framework 4.5.

## How to install

You can install using nuget. (Unimplemented)

```
PM> Install-Package ApplicationRegistries2
```


## How to use

1. Reference ApplicationRegistries2.dll.
2. Declare the interface corresponding to the external setting value.
   The interface has the following restrictions.
    * Having only the get property.
    * Property type must be the following Type.
      * Int32
      * string
3. Attach the attribute [ApplicationRegistry] to the external setting value interface.
4. You can read external settings by accessing properties using `ApplicationRegistry.Get <T> ()`.
   `T` specifies an interface type declared with 2.

By default ApplicationRegistries loads external settings with the following priority.
* (1)Commandline argument
* (2)Environment Variable
* (3)Registry(HKCU)
* (4)XML File
* (5)Registry(HKLM)

By specifying the loading place in the argument of the [ApplicationRegistry] attribute
You can set the priority, enable / disable.


### Usage: Commandline arguments

By default, ApplicationRegistries reads external settings with the following commandline arguments.

```
--[Interface Name]_[Property Name]="[Value]"
```

Example
``` csharp
  [ApplicationRegistry]
  public interface ISettings
  {
    int PortNo {get;}
  }
```

```
--ISettings_PortNo=80
```

You can change the interface name and property name by specifying the attribute [CommandlineArgumentPrefix] and [CommandlineArgumentName].

Example
``` csharp
  [ApplicationRegistry]
  [CommandlineArgumentPrefix("Settings")]
  public interface ISettings
  {
    [CommandlineArgumentName("ListenPort")]
    int PortNo {get;}
  }
```

### Usage: Environment Valiable

By default, ApplicationRegistries reads external settings with the following Environment Valiable.

```
set [Assembly Name]_[Interface Name]_[Property Name]=[Value]
```

例
``` csharp
  // ApplicationRegistries.Sample.dll
  [ApplicationRegistry]
  public interface ISettings
  {
    int PortNo {get;}
  }
```

```
set ApplicationRegistries.Sample‗ISettings_PortNo=80
```

You can change the assembly name , the interface name and property name by specifying the attribute [EnvironmentVariablePrefix] and [EnvironmentVariableName].

例
``` csharp
  [ApplicationRegistry]
  [EnvironmentVariablePrefix("Settings")]
  public interface ISettings
  {
    [EnvironmentVariableName("ListenPort")]
    int PortNo {get;}
  }
```

### Usage: Registry

By default, ApplicationRegistries reads external settings with the following registries'.

In case of HKCU
```
[HKCU\Software\ApplicationRegistries\[Assembly Name]\[Interface Name]]
"Property Name"=[type]:[value]
```
In case of HKLM
```
[HKLM\Software\ApplicationRegistries\[Assembly Name]\[Interface Name]]
"Property Name"=[type]:[value]
```

Example
``` csharp
  // ApplicationRegistries.Sample.dll
  [ApplicationRegistry]
  public interface ISettings
  {
    int PortNo {get;}
  }
```

```
[HKCU\Software\ApplicationRegistries\ApplicationRegistries.Sample\ISettings]
"PortNo"=dword:00000050

[HKLM\Software\ApplicationRegistries\ApplicationRegistries.Sample\ISettings]
"PortNo"=dword:00000050
```

You can change the location of the registry by specifying the attribute [RegistryKey] and [RegistryName].

Example
``` csharp
  [ApplicationRegistry]
  [RegistryKey(@"Software\MyCompany\MySoftware")]
  public interface ISettings
  {
    [RegistryName("ListenPort")]
    int PortNo {get;}
  }
```

### Usage: XML File

By default, ApplicationRegistries reads external settings with the following Xml file'.

File Path: [ApplicationRegistries2.dll's Folder]\ApplicationRegisties.xml

``` xml
<?xml version="1.0" encoding="utf-8" ?>
<ApplicationRegisties>
  <[Interface Name]>
    <[Property Name]>Value</[Property Name]>
  </[Interface Name]>
</ApplicationRegisties>
```

Example
``` csharp
  // ApplicationRegistries.Sample.dll
  [ApplicationRegistry]
  public interface ISettings
  {
    int PortNo {get;}
  }
```

``` xml
<?xml version="1.0" encoding="utf-8" ?>
<ApplicationRegisties>
  <ISettings>
    <PortNo>80</PortNo>
  </ISettings>
</ApplicationRegisties>
```

You can change the xml file path and the xml file structure by specifying the attribute [XmlFile] and [XmlName].

Example
``` csharp
  [ApplicationRegistry]
  [XmlFile(@".\Settings.xml", "/Settings")]
  public interface ISettings
  {
    [XmlName("ListenPort")]
    int PortNo {get;}
  }
```

``` xml
<?xml version="1.0" encoding="utf-8" ?>
<Settings>
  <ListenPort>80</ListenPort>
</Settings>
```

### Usage: Default Value

You can specify a default value if all of the external settings can not be loaded.
You can specify it with the attribute [DefaultValue].

Example
``` csharp
  [ApplicationRegistry]
  public interface ISettings
  {
    [DefaultValue(80)]
    int PortNo {get;}
  }
```

### Priority and invalidation of external setting

The default priority order of external setting is as shown in the following table.
You can change the priority or invalidate by specifying the key as an array in the argument of [ApplicationRegistry].


| External Setting      | default priority   | Key                                   |
|-----------------------|--------------------|---------------------------------------|
| Commandline arguments | 1                  | BuiltInAccessors.CommandlineArguments |
| Environment variable  | 2                  | BuiltInAccessors.EnvironmenetVariable |
| Registry(HKCU)        | 3                  | BuiltInAccessors.UserRegistry         |
| XML File              | 4                  | BuiltInAccessors.XmlFile              |
| Registry(HKLM)        | 5                  | BuiltInAccessors.MachineRegistry      |

``` csharp
  [ApplicationRegistry(
    BuiltInAccessors.CommandlineArguments,
    BuiltInAccessors.EnvironmenetVariable,
    BuiltInAccessors.UserRegistry,
    BuiltInAccessors.MachineRegistry
  )]
  public interface ISettings
  {
    int PortNo {get;}
  }
```

### User defined external settings

If you want to read the setting values from application-specific database or configuration file, you can implement your own loading process.

1. Create a user defined class for reading external setting values implementing IAccessor.
2. Register user-defined classes with `ApplicationRegistry.RegistCustomAccessor()`.
   At this time, specify the key as the first argument.
``` csharp
  ApplicationRegistry.RegistCustomAccessor("CUSTOM", new CustomAccessor());
```
3. Specify the key registered in 2 as the argument of the attribute [ApplicationRegistry].
``` csharp
  [ApplicationRegistry(
    "CUSTOM",
    BuiltInAccessors.CommandlineArguments,
    BuiltInAccessors.EnvironmenetVariable,
    BuiltInAccessors.UserRegistry,
    BuiltInAccessors.MachineRegistry
  )]
  public interface ISettings
  {
    int PortNo {get;}
  }
```

### Reporting

(Coming Soon)

## Contribution

1. Fork it ( https://github.com/banban525/ApplicationRegistries/fork )
2. Create your feature branch (git checkout -b my-new-feature)
3. Commit your changes (git commit -am 'Add some feature')
4. Push to the branch (git push origin my-new-feature)
5. Create new Pull Request

## License

[MIT](https://github.com/banban525/ApplicationRegistries/blob/master/LICENSE)

## Author

[banban525](https://github.com/banban525)
