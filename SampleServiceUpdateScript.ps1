$serviceName = "MyServiceName"
$sourcePath = "D:\Installs\ExampleService\1.1\*"
$destPath = "D:\Applications\ExampleService"
$svc = Get-Service $serviceName
if($svc.status -eq "Running"){
    Stop-Service $svc
    $svc.WaitForStatus('Stopped')
	Get-Service $serviceName
}
Copy-Item $sourcePath -Destination $destPath -force -recurse
Start-Service $serviceName
Get-Service $serviceName