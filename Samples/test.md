# Abstract

This document is described for the specifications of the external setting.


# Registries

## ID: InstallDir

Installed Directory

* Type: String
* Key: HKEY_CURRENT_USER\\SOFTWARE\\banban525\\ApplicationRegistries\\Install
* Name: Directory
* Default: None



# Environment Variables

## ID: Proxy

Proxy for http access.

* Type: String
* Name: PROXY
* Default: localhost



# Commandline Arguments

## ID: IsDebug

Debug Mode

* Type: Boolean
* Ignore Case: true
* Default: 0
* Name: /Debug
* ParseType: If the Name contains in commandline arguments, the value is true.

## ID: InputFiles

Soufce files.

* Type: StringArray
* Ignore Case: true
* Default: 0
* Name: /input
* ParseType: If the Name contains in commandline arguments, the value is a next commandline argument.

## ID: OutputFile

An output file path.

* Type: String
* Ignore Case: true
* Default: 0
* ParseType: If a commandline argument is matched by the Pattern, the value is returned.
* Pattern : /Output:(.+)




# Static Values

## ID: ApplicationName

Application Name

* Type: String
* Value: ApplicationRegistries


