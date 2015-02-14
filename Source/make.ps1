#requires -version 2
<#
.SYNOPSIS
  Build ApplicationRegistries and create nupkg

.DESCRIPTION
  Build ApplicationRegistries and create nupkg

.PARAMETER Version
  Version No for nupkg

.INPUTS

.OUTPUTS
  Output nupkg in script directory.

.NOTES
  Version: 1.0
  Author: banban525
  Creation Date: 2015-02-13
  Purpose/Change: Initial script development

.EXAMPLE
  powershell -f make.ps1 -Version 1.0.0

#>
 
#---------------------------------------------------------[Initialisations]--------------------------------------------------------
  
param(
[parameter(Mandatory=$true)]
[string]$version
)

Set-StrictMode -Version Latest
$ErrorActionPreference = "Stop"
$WarningPreference = "Continue"
$VerbosePreference = "Continue"
$DebugPreference = "Continue"

$scriptDir = Split-Path ( & { $myInvocation.ScriptName } ) -parent
Push-Location $scriptDir
try
{

    $msbuild = Join-Path $env:SystemRoot "Microsoft.NET\Framework\v4.0.30319\MSBuild.exe"

    & $msbuild (Join-Path $scriptDir "ApplicationRegistries.sln")  /p:Configuration=Release "/p:Platform=Any CPU"  /t:Rebuild

    $nuget = Join-Path $scriptDir ".nuget\nuget.exe"
    & "$nuget" "pack" "-Version" "$version"
}
finally
{
    Pop-Location
}