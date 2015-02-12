#########################################
External Configuration Specification
#########################################


Abstract
==========

This document is described for the specifications of the external setting.


Registries
==========

$_output.Entries.RegistryEntries:{entry|
ID: $entry.Define.ID$
------------------------------

$entry.Define.Description$

* Type: $entry.Define.Type$
* Key: $entry.Key$
* Name: $entry.Name$
* Default: $entry.DefaultValue$

}$

Environment Variables
======================

$_output.Entries.EnvironmentVariableEntries:{entry|
ID: $entry.Define.ID$
------------------------------

$entry.Define.Description$

* Type: $entry.Define.Type$
* Name: $entry.VariableName$
* Default: $entry.DefaultValue$

}$

Commandline Arguments
=====================


$_output.Entries.CommandLineArgumentEntries:{entry|
ID: $entry.Define.ID$
------------------------------

$entry.Define.Description$

* Type: $entry.Define.Type$
* Name: $entry.ArgumentName$
* Ignore Case: $entry.IgnoreCase$
* Default: $entry.DefaultValue$

}$

Static Values
==============


$_output.Entries.StaticValueEntries:{entry|
ID: $entry.Define.ID$
------------------------------

$entry.Define.Description$

* Type: $entry.Define.Type$
* Value: $entry.Value$

}$