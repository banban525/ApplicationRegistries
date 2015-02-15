$pshost = get-host
$pswindow = $pshost.ui.rawui
  
$newsize = $pswindow.buffersize
$newsize.height = 25
$newsize.width = 78
$pswindow.buffersize = $newsize

$newsize = $pswindow.windowsize
$newsize.height = 25
$newsize.width = 78
$pswindow.windowsize = $newsize

Clear-Host
.\packages\ApplicationRegistries.1.0.0\tools\ApplicationRegistries.Generator.exe -Mode Code -Output Registries.cs -Input .\define.xml