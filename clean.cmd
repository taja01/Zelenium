powershell -Command "Get-ChildItem -Depth 5 -Filter bin | foreach ($_) { remove-item $_.fullname -Force -Recurse }"
powershell -Command "Get-ChildItem -Depth 5 -Filter obj | foreach ($_) { remove-item $_.fullname -Force -Recurse }"