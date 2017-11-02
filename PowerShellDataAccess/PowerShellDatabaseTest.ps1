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

function New-DBUser
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

function New-DBCustomer
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
Add-type -Path .\DataBaseAccess\Entities\bin\Release\Entities.dll

#Functions that we loaded into scope
Get-Command -Noun DB*

#Example of syntax
Get-Help Create-DBRecord -ShowWindow
Get-Help New-DBUser -ShowWindow

#Using custom functions to create objects
$user = New-DBUser 0 User User@email.com
$customer = New-DBCustomer 0 Customer Customer@email.com
$userWithDefaults = New-DBUser
$CustomerWithProperties = New-DBCustomer -CustomerName Customer1

#Using objects as parameter for Create-DBRecord
Create-DBRecord -DBObject $user

#Passing objects to Create-DBRecord
$user | Create-DBRecord

#Passing multiple objects through the pipeline to Create-DBRecord
$customer, $userWithDefaults | Create-DBRecord

$arrayOfObjects = $user, $customer
$arrayOfObjects | Create-DBRecord

#Creating object and passing to Create-DBRecord
New-DBUser -UserName Test -UserEmail Test@email.com | Create-DBRecord
Create-DBRecord -DBObject (New-DBUser -UserName Test -UserEmail Test@email)

#Find records using primary key
New-DBUser 1 | Read-DBRecord

#Find records using properties
New-DBUser -UserName Test | Read-DBRecord
New-DBUser -UserName Test -UserEmail Test@email.com | Read-DBRecord

#Find records using multiple properties
(New-DBUser -UserName Test),(New-DBCustomer -CustomerName Customer) | Read-DBRecord

#Update records using Primary key
New-DBUser 3 | Update-DBRecord -UpdatedDBObject (New-DBUser -UserName UpdateTest -UserEmail UpdateEmail)

#Update records using properties
New-DBUser -UserName Test | Update-DBRecord -UpdatedDBObject (New-DBUser -UserName Test1)

#Delete record using primary key
New-DBUser 7 | Delete-DBRecord

#Delete records(s) using properties
New-DBUser -UserName Test | Delete-DBRecord