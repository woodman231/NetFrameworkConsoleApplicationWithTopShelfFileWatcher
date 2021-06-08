$sourcePath = "D:\Installs\ExampleService\1.0\*"
$destinationPath = "D:\Applications\ExampleService\"
$serviceCmd = "D:\Applications\ExampleService\ExampleService.exe"
$params = 'install -username:some-domain\SomeServiceAccount -password:SomeServieAccountPassword'
Copy-Item $sourcePath -Destination $destinationPath -force -recurse
$prms = $params.Split(" ");
& $serviceCmd $prms