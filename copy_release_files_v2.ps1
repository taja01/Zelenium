Remove-Item "release" -Recurse -ErrorAction Ignore

New-Item -Path . -Name "release" -ItemType "directory"

$dest = "C:\Users\Tamas_Jarvas\Documents\development\microservices-testing\Wizz.TestAutomation\Wizz.TestAutomation.Shared\Lib"
$folder = "\obj\Release\netcoreapp3.1\*"
$coreApp = "src\Zelenium.Core"
$sharedApp = "src\Zelenium.Shared"
$webdrivermanagerApp = "src\Zelenium.WebDriverManager"

Copy-Item -Filter *.dll -Path "$coreApp$folder" -Destination $dest
Copy-Item -Filter *.dll -Path "$sharedApp$folder" -Destination $dest
Copy-Item -Filter *.dll -Path "$webdrivermanagerApp$folder" -Destination $dest