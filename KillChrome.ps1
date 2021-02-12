$counter = (Get-Process | Where-Object {$_.Name -eq 'chromedriver'}).count

if ( $counter -eq 0 )
{
    Write-Output "Chrome driver does not stuck"
}
else
{
    Get-Process chromedriver | Stop-Process
}