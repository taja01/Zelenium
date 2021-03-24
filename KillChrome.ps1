$counter = (Get-Process | Where-Object {$_.Name -eq 'chromedriver'}).count


if ( $counter -eq 0 )
{
    Write-Output "All chrome drivers closed correctly"
}
else
{
    Write-Host Number of chromedriver stuck: $counter
    Get-Process chromedriver | Stop-Process
}