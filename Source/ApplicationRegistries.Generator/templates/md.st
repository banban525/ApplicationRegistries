
# Abstract

This document is described for the specifications of the external setting.


# Registries

$_output.Entries.RegistryEntries:{entry|
## ID: $entry.Define.ID$

$entry.Define.Description$

* Type: $entry.Define.Type$
* Key: $entry.Key$
* Name: $entry.Name$
* Default: $entry.DefaultValue$

}$

# Environment Variables

$_output.Entries.EnvironmentVariableEntries:{entry|
## ID: $entry.Define.ID$

$entry.Define.Description$

* Type: $entry.Define.Type$
* Name: $entry.VariableName$
* Default: $entry.DefaultValue$

}$

# Commandline Arguments

$_output.Entries.CommandLineArgumentEntries:{entry|
## ID: $entry.Define.ID$

$entry.Define.Description$

* Type: $entry.Define.Type$
* Ignore Case: $entry.IgnoreCase$
* Default: $entry.DefaultValue$
$if(entry.IsTypeHasArgument)$
* Name: $entry.ArgumentName$
* ParseType: If the Name contains in commandline arguments, the value is true.
$endif$
$if(entry.IsTypeUseNextValue)$
* Name: $entry.ArgumentName$
* ParseType: If the Name contains in commandline arguments, the value is a next commandline argument.
$endif$
$if(entry.IsTypeParsePattern)$
* ParseType: If a commandline argument is matched by the Pattern, the value is returned.
* Pattern : $entry.Pattern$
$endif$


}$

# Static Values

$_output.Entries.StaticValueEntries:{entry|
## ID: $entry.Define.ID$

$entry.Define.Description$

* Type: $entry.Define.Type$
* Value: $entry.Value$

}$
