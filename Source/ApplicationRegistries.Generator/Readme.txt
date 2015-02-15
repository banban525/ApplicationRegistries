#####################
ApplicationRegistries
#####################

ApplicaitonRegistries is a .NET library to abstract the configuration of the application.

Description
===========

Most of the applications are designed to change the behavior in accordance with external configurations, for example the registry and environment variables.
However, in order to easier to perform unit tests, these external configurations should be able to dynamically change the settings.
This library solve the problem by abstracting the registry and environment variables as objects.
According to the definition described in XML, ApplicaitonRegistries create a wrapper class that accesses the external Configuration.
If you use this wrapper class, you can change the external configuration without changing the application code and the actual external configuration.

Usage
=====

Examples of XML definition:

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


The generator tool Usage
=========================

    Usage: ApplicationRegistries.Generator.exe <options>
    
    options:
    
      -m, --mode         Required. (Default: Code) Select Mode for generation type.
      -o, --output       Required. Output file path.
      -i, --input        Required. Input file path.
      -c, --classname    (Default: Registries) class name for *.cs Code
      -n, --namespace    (Default: ApplicationRegistries) namespace for *.cs Code.
      -t, --template     (Default: ) template name for --Mode Other
      --help             Display this help screen.

Licence
=======

MIT https://github.com/banban525/ApplicationRegistries/blob/master/LICENSE

Author
======

banban525 (https://github.com/banban525)


dependent on
============

This tool is dependent on follows:

Antlr4.StringTemplate
----------------------

[The BSD License]
Copyright (c) 2012 Terence Parr and Sam Harwell
All rights reserved.

Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:

    Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.
    Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.
    Neither the name of the author nor the names of its contributors may be used to endorse or promote products derived from this software without specific prior written permission. 

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.


Command Line Parser Library
----------------------------

Command Line Parser Library

Author:
  Giacomo Stelluti Scala (gsscoder@gmail.com)

Copyright (c) 2005 - 2013 Giacomo Stelluti Scala & Contributors

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.