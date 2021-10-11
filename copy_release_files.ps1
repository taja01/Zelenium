Remove-Item "release" -Recurse -ErrorAction Ignore

New-Item -Path . -Name "release" -ItemType "directory"


Copy-Item -Filter *.dll -Path "src\Zelenium.Core\obj\Release\netcoreapp3.1\*" -Destination release
Copy-Item -Filter *.dll -Path "src\Zelenium.Shared\obj\Release\netcoreapp3.1\*" -Destination release
Copy-Item -Filter *.dll -Path "src\Zelenium.WebDriverManager\obj\Release\netcoreapp3.1\*" -Destination release