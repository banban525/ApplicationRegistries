
cd %~dp0

call "C:\Program Files (x86)\Microsoft Visual Studio 12.0\VC\bin\vcvars32.bat"

xsd /c /namespace:ApplicationRegistries.GeneratedXmlObject ..\..\..\schemas\1.0.0\ApplicationRegistryBehavior.xsd ..\..\..\schemas\1.0.0\ApplicationRegistryDefine.xsd
