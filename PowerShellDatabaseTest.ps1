<#
.Synopsis
   Short description
.DESCRIPTION
   Long description
.EXAMPLE
   Example of how to use this cmdlet
.EXAMPLE
   Another example of how to use this cmdlet
#>
function Create-DBRecord
{
    Param
    (
        # Param1 help description
        [Parameter(Mandatory=$true,
                   Position=0)]
        [ValidateSet("Create", "Read", "Update", "Delete")]
		[string]
        $OperationType,

        # Param2 help description
        [Parameter()]
        [int]
        $UserId = 0,

        # Param3 help description
        [Parameter()]
        [string]
        $UserName = "",

         # Param4 help description
        [Parameter()]
        [string]
        $UserEmail = ""
    )

    Begin
    {
		Add-type -Path .\DBEntities.dll
        $User = New-Object DBEntities.User($UserId, $UserName, $UserEmail, (Get-Date))

        $url = "http://localhost:56577/DataBaseAccess.svc"

        $proxyWS = New-WebServiceProxy $url



    }
    Process
    {
        $temp = $User | ConvertTo-Json
        $results = $proxyWS.CreateRecord($temp)
        Write-Output $results
    }
    End
    {
    }
}

Create-DBRecord -OperationType Create -UserName "PowerSHell" -UserEmail "FromPowerShell"
