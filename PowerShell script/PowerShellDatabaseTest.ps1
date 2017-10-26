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
        [Parameter(
            Position=0, 
            Mandatory=$true, 
            ValueFromPipeline=$true,
            ValueFromPipelineByPropertyName=$true)]
        [Entities.IEntity]
        $DBObject
    )

    Begin
    {
		Add-type -Path .\Entities.dll

        $url = "http://localhost:53172/Service1.svc"

        $proxyWS = New-WebServiceProxy $url
    }
    Process
    {
        $results = $proxyWS.DBRecord(($DBObject | ConvertTo-Json), "Create", $null)
        Write-Output $results
    }
    End
    {
    }
}

function Read-DBRecord
{
    Param
    (
        # Param1 help description
        [Parameter(
            Position=0, 
            Mandatory=$true, 
            ValueFromPipeline=$true,
            ValueFromPipelineByPropertyName=$true)]
        [Entities.IEntity]
        $DBObject
    )

    Begin
    {
		Add-type -Path .\Entities.dll

        $url = "http://localhost:53172/Service1.svc"

        $proxyWS = New-WebServiceProxy $url
    }
    Process
    {
        $results = $proxyWS.DBRecord(($DBObject | ConvertTo-Json), "Read", $null)
        Write-Output $results
    }
    End
    {
    }
}

function Update-DBRecord
{
    Param
    (
        # Param1 help description
        [Parameter(
            Position=0, 
            Mandatory=$true, 
            ValueFromPipeline=$true,
            ValueFromPipelineByPropertyName=$true)]
        [Entities.IEntity]
        $DBObject,

        [Parameter(
            Position=1, 
            Mandatory=$true)]
        [Entities.IEntity]
        $UpdatedDBObject
    )

    Begin
    {
		Add-type -Path .\Entities.dll

        $url = "http://localhost:53172/Service1.svc"

        $proxyWS = New-WebServiceProxy $url
    }
    Process
    {
        $results = $proxyWS.DBRecord(($DBObject | ConvertTo-Json), "Update", ($UpdatedDBObject | ConvertTo-Json))
        Write-Output $results
    }
    End
    {
    }
}

function Delete-DBRecord
{
    Param
    (
        # Param1 help description
        [Parameter(
            Position=0, 
            Mandatory=$true, 
            ValueFromPipeline=$true,
            ValueFromPipelineByPropertyName=$true)]
        [Entities.IEntity]
        $DBObject
    )

    Begin
    {
		Add-type -Path .\Entities.dll

        $url = "http://localhost:53172/Service1.svc"

        $proxyWS = New-WebServiceProxy $url
    }
    Process
    {
        $results = $proxyWS.DBRecord(($DBObject | ConvertTo-Json), "Delete", $null)
        Write-Output $results
    }
    End
    {
    }
}

Get-Command -Noun DBRecord

Get-Help Create-DBRecord -Full

$createUser = New-Object Entities.User("Billy", "Bob", (Get-Date))
$createCustomer = New-Object Entities.Customer ("John", "Smith", (Get-Date))

$createUser | Create-DBRecord
$createCustomer | Create-DBRecord

$readUser = New-Object Entities.User(10)
$readCustomer = New-Object Entities.Customer(4)

$readUser | Read-DBRecord
$readCustomer | Read-DBRecord

$updateUser = New-Object Entities.User("Bill", "Bobby", (Get-Date))
$updateCustomer = New-Object Entities.Customer("Jill", "Smith", (Get-Date))

$readUser | Update-DBRecord -UpdatedDBObject $updateUser
$readCustomer | Update-DBRecord -UpdatedDBObject $updateCustomer

$readUser | Delete-DBRecord
$readCustomer | Delete-DBRecord