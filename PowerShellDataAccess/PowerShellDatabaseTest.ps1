<#
.Synopsis
   Takes an IEntity object and sends it to the WebApi as a create request.
.DESCRIPTION
   Takes in an IEntity object and passes it to the WebApi as a create request.  To pass multiple objects you must pass them across the pipeline.
.EXAMPLE
   Users the Create-DBuser cmdlet to create a new Entity.User object and passes this through the pipeline.
   Create-DBUser -UserName Test -UserEmail Test@email | Create-DBRecord
   {"DBUserId":1,"UserName":"Test","UserEmail":"Test@email","UserAddDTM":"2017-10-27T18:29:35.01Z"}

.EXAMPLE
   Uses the New-Object cmdlet to create a new Entity.Customer object and passes this through the pipeline
   New-Object Entities.Customer("Test", "Test@email", (Get-Date)) | Create-DBRecord
   {"DBCustomerId":1,"CustomerName":"Test","CustomerEmail":"Test@email","CustomerAddDTM":"2017-10-27T18:37:59.06Z"}

.EXAMPLE
    Passes in a new Entity.User object into the DBObject parameter.
    Create-DBRecord -DBObject (Create-DBUser -UserName Test -UserEmail Test@email)
    {"DBUserId":1,"UserName":"Test","UserEmail":"Test@email","UserAddDTM":"2017-10-27T18:40:56.487Z"}
#>
function Create-DBRecord
{
    Param
    (
        # Passes this object to the webApi
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
		Add-type -Path .\DataBaseAccess\Entities\bin\Release\Entities.dll

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
		Add-type -Path .\DataBaseAccess\Entities\bin\Release\Entities.dll

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
		Add-type -Path .\DataBaseAccess\Entities\bin\Release\Entities.dll

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
		Add-type -Path .\DataBaseAccess\Entities\bin\Release\Entities.dll

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

function Create-DBUser
{
    Param
    (
        # Param1 help description
        [Parameter(
            Position=0)]
        [int]
        $UserId = 0,
        [Parameter(
            Position=1)]
        [string]
        $UserName = $null,
        [Parameter(
            Position=2)]
        [string]
        $UserEmail = $null
    )

    Begin
    {
		Add-type -Path .\DataBaseAccess\Entities\bin\Release\Entities.dll
    }
    Process
    {
        Write-Output (New-Object Entities.User($UserId, $UserName, $UserEmail, (Get-Date)))
    }
    End
    {
    }
}

function Create-DBCustomer
{
    Param
    (
        # Param1 help description
        [Parameter(
            Position=0)]
        [int]
        $CustomerId = 0,
        [Parameter(
            Position=1)]
        [string]
        $CustomerName = $null,
        [Parameter(
            Position=2)]
        [string]
        $CustomerEmail = $null
    )

    Begin
    {
		Add-type -Path .\DataBaseAccess\Entities\bin\Release\Entities.dll
    }
    Process
    {
        Write-Output (New-Object Entities.Customer($CustomerId, $CustomerName, $CustomerEmail, (Get-Date)))
    }
    End
    {
    }
}

#Load the required DLL
Add-type -Path .\Entities.dll

#Functions that we loaded into scope
Get-Command -Noun DB*

#Example of syntax
Get-Help Create-DBRecord -ShowWindow

#Creating object and passing to create all in one step
Create-DBUser -UserName Test -UserEmail Test@email | Create-DBRecord
Create-DBRecord -DBObject (Create-DBUser -UserName Test -UserEmail Test@email)

#Using New-Object to create Entity objects
$createUser = New-Object Entities.User("Test", "Test@email", (Get-Date))
$createCustomer = New-Object Entities.Customer ("John", "Smith", (Get-Date))

#Using custom functions to create objects
$user = Create-DBUser 10 Bob Billy
$customer = Create-DBCustomer 10 Bob Billy
$userWithDefaults = Create-DBUser
$userWithSelectProperties = Create-DBUser -UserName Smith

#Object is a real object
$user | Get-Member

$createUser | Create-DBRecord
$createCustomer | Create-DBRecord

$readUser = New-Object Entities.User(1)
$readCustomer = New-Object Entities.Customer(1)

Create-DBUser -UserName Joe -UserEmail Joe@email.com | Create-DBRecord
Create-DBCustomer -CustomerName Jill | Create-DBRecord

$readUser | Read-DBRecord
$readCustomer | Read-DBRecord

$updateUser = New-Object Entities.User("Bill", "Bobby", (Get-Date))
$updateCustomer = New-Object Entities.Customer("Jill", "Smith", (Get-Date))

$readUser | Update-DBRecord -UpdatedDBObject $updateUser
$readCustomer | Update-DBRecord -UpdatedDBObject $updateCustomer

$readUser | Delete-DBRecord
$readCustomer | Delete-DBRecord

$ListOfDBObjects = (New-Object Entities.User("User1", "email", (Get-Date))), (New-Object Entities.Customer("Customer1", "email", (Get-Date)))

$ListOfDBObjects[0]
$ListOfDBObjects[1]

$ListOfDBObjects | Create-DBRecord

(Create-DBUser -UserName 1), (Create-DBUser -UserName 2), (Create-DBCustomer -CustomerName 1) | Create-DBRecord