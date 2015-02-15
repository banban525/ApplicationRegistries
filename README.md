ApplicationRegistries
======================

[To Japanese](https://github.com/banban525/ApplicationRegistries/blob/master/README.ja.md)

ApplicaitonRegistries is a .NET library to abstract the configuration of the application.

## Description

Most of the applications are designed to change the behavior in accordance with external configurations, for example the registry and environment variables.
However, in order to easier to perform unit tests, these external configurations should be able to dynamically change the settings.
This library solve the problem by abstracting the registry and environment variables as objects.
According to the definition described in XML, ApplicaitonRegistries create a wrapper class that accesses the external Configuration.
If you use this wrapper class, you can change the external configuration without changing the application code and the actual external configuration.

## Demo

![Demo Animation](https://github.com/banban525/ApplicationRegistries/blob/master/Samples/ReadmeContents/Readme_images.gif?raw=true) 


## Requirement

ApplicationRegistries.dll refer to .NET Framework 4.0 only.

ApplicationRegistries.Generator.exe is refer to follows:

* Antlr4.StringTemplate
* Command Line Parser Library


## Usage

Examples of XML definition

    <?xml version="1.0" encoding="utf-8" ?>
    <ApplicationRegistryDefine
      xmlns="https://github.com/banban525/ApplicationRegistries/schemas/1.0.0/ApplicationRegistryDefine.xsd">
      <Entry id="InstallDir" Type="string">
        <Description>Installed Directory</Description>
        <Registory>
          <Key>HKLM\SOFTWARE\banban525\ApplicationRegistries\Install</Key>
          <Name>Directory</Name>
          <DefaultValue>None</DefaultValue>
        </Registory>
      </Entry>
      <Entry id="ApplicationName" Type="string">
        <Description>Application Name</Description>
        <StaticValue>
          <Value>ApplicationRegistries</Value>
        </StaticValue>
      </Entry>
      <Entry id="IsDebug" Type="bool">
        <Description>Debug Mode</Description>
        <CommandLineArgument ignoreCase="true">
          <ArgumentName>/Debug</ArgumentName>
          <DefaultValue>0</DefaultValue>
        </CommandLineArgument>
      </Entry>
      <Entry id="LogginUser" Type="string">
        <Description>Loggined user</Description>
        <EnvironmentVariable>
          <VariableName>USERNAME</VariableName>
          <DefaultValue>Unknown</DefaultValue>
        </EnvironmentVariable>
      </Entry>
    </ApplicationRegistryDefine>

How to generate a wrapper class:

    ApplicationRegistries.Generator.exe --mode Code --input define.xml --output Registries.cs

Generated wrapper class is as follows:

     public class Registries
    {
        public Registries()
        {
            ...
        }

        /// <summary>
        /// Installed Directory
        /// </summary>
        public String InstallDir
        {
            get
            {
                ...
            }
        }
    ...
    
    }


### The generator tool Usage

    Usage: ApplicationRegistries.Generator.exe <options>
    
    options:
    
      -m, --mode         Required. (Default: Code) Select Mode for generation type.
      -o, --output       Required. Output file path.
      -i, --input        Required. Input file path.
      -c, --classname    (Default: Registries) class name for *.cs Code
      -n, --namespace    (Default: ApplicationRegistries) namespace for *.cs Code.
      -t, --template     (Default: ) template name for --Mode Other
      --help             Display this help screen.

## Install

You can install with nuget. The command is follows:

    PM> Install-Package ApplicationRegistries

## Contribution

1. Fork it ( https://github.com/banban525/ApplicationRegistries/fork )
2. Create your feature branch (git checkout -b my-new-feature)
3. Commit your changes (git commit -am 'Add some feature')
4. Push to the branch (git push origin my-new-feature)
5. Create new Pull Request


## Licence

[MIT](https://github.com/tcnksm/tool/blob/master/LICENCE)

## Author

[banban525](https://github.com/banban525)
