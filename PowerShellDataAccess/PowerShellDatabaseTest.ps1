<#
.Synopsis
   Takes an IEntity object and sends it to the WebApi as a create request.
.DESCRIPTION
   Takes in an IEntity object and passes it to the WebApi as a create request.  To pass multiple objects you must pass them across the pipeline.
.EXAMPLE
   Users the New-DBuser cmdlet to create a new Entity.User object and passes this through the pipeline.
   New-DBUser -UserName Test -UserEmail Test@email | Create-DBRecord
   {"DBUserId":1,"UserName":"Test","UserEmail":"Test@email","UserAddDTM":"2017-10-27T18:29:35.01Z"}
.EXAMPLE
   Uses the New-Object cmdlet to create a new Entity.Customer object and passes this through the pipeline
   New-Object Entities.Customer("Test", "Test@email", (Get-Date)) | Create-DBRecord
   {"DBCustomerId":1,"CustomerName":"Test","CustomerEmail":"Test@email","CustomerAddDTM":"2017-10-27T18:37:59.06Z"}
.EXAMPLE
    Passes in a new Entity.User object into the DBObject parameter.
    Create-DBRecord -DBObject (Create-DBUser -UserName Test -UserEmail Test@email)
    {"DBUserId":1,"UserName":"Test","UserEmail":"Test@email","UserAddDTM":"2017-10-27T18:40:56.487Z"}
.EXAMPLE
    Users the New-DBuser cmdlet to create multiple new Entity.User objects and passes them through the pipeline.
    (New-DBUser -UserName User1),(New-DBCustomer -CustomerName Customer1) | Create-DBRecord
    {"DBUserId":8,"UserName":"User1","UserEmail":"","UserAddDTM":"2017-11-02T18:41:47.698Z"}
    {"DBCustomerId":3,"CustomerName":"Customer1","CustomerEmail":"","CustomerAddDTM":"2017-11-02T18:41:47.698Z"}
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

<#
.Synopsis
   Takes an IEntity object and sends it to the WebApi as a read request.
.DESCRIPTION
   Takes in an IEntity object and passes it to the WebApi as a read request.  To pass multiple objects you must pass them across the pipeline.
.EXAMPLE
   Users the New-DBuser cmdlet to pass in an object to query the database by primary key.
   New-DBUser 1 | Read-DBRecord
   {"DBUserId":1,"UserName":"Test","UserEmail":"Test@email","UserAddDTM":"2017-10-27T18:29:35.01Z"}
.EXAMPLE
   Users the New-DBuser cmdlet to pass in an object to query the database using properties.
   New-DBUser -UserName Test | Read-DBRecord
   {"DBUserId":9,"UserName":"Test","UserEmail":"Test@email.com","UserAddDTM":"2017-11-02T18:44:53.033"}
.EXAMPLE
    Users the New-DBuser cmdlet to pass in multiple objects to query the database using properties.
    (New-DBUser -UserName Test),(New-DBCustomer -CustomerName Customer) | Read-DBRecord
    {"DBUserId":9,"UserName":"Test","UserEmail":"Test@email.com","UserAddDTM":"2017-11-02T18:44:53.033"}
    {"DBCustomerId":1,"CustomerName":"Customer","CustomerEmail":"Customer@email.com","CustomerAddDTM":"2017-11-02T13:07:24.967"}
    {"DBCustomerId":2,"CustomerName":"Customer","CustomerEmail":"Customer@email.com","CustomerAddDTM":"2017-11-02T13:07:24.967"}
#>
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

<#
.Synopsis
   Takes an IEntity object and sends it to the WebApi as an update request.
.DESCRIPTION
   Takes in an IEntity object and passes it to the WebApi as an update request.  To pass multiple objects you must pass them across the pipeline, you can only pass a single update object
.EXAMPLE
   Uses the New-DBuser cmdlet to pass in an object to query the database by primary key and update the record.
   New-DBUser 3 | Update-DBRecord -UpdatedDBObject (New-DBUser -UserName UpdateTest -UserEmail UpdateEmail)
   {"DBUserId":3,"UserName":"UpdateTest","UserEmail":"UpdateEmail","UserAddDTM":"2017-11-02T13:07:24.983"}
.EXAMPLE
   Uses the New-DBuser cmdlet to pass in an object to query the database by property and update the record(s).
   New-DBUser -UserName Test | Update-DBRecord -UpdatedDBObject (New-DBUser -UserName Test1)
   {"DBUserId":9,"UserName":"Test1","UserEmail":"Test@email.com","UserAddDTM":"2017-11-02T18:44:53.033"}
#>
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

<#
.Synopsis
   Takes an IEntity object and sends it to the WebApi as a delete request.
.DESCRIPTION
   Takes in an IEntity object and passes it to the WebApi as a delete request.  To pass multiple objects you must pass them across the pipeline.
.EXAMPLE
   Uses the New-DBuser cmdlet to pass in an object to query the database by primary key and delete the record.
   New-DBUser 8 | Delete-DBRecord
   {"DBUserId":8,"UserName":"User1","UserEmail":"","UserAddDTM":"2017-11-02T18:41:47.697"}
.EXAMPLE
   Uses the New-DBuser cmdlet to pass in an object to query the database by property and update the object(s).
   New-DBUser -UserName Test1 | Delete-DBRecord
   {"DBUserId":9,"UserName":"Test1","UserEmail":"Test@email.com","UserAddDTM":"2017-11-02T18:44:53.033"}
#>
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

<#
.Synopsis
   Creates a new DBUser object.
.DESCRIPTION
   Creates a new DBUser object and assigns defaults values to any non-specified parameters
.EXAMPLE
   Uses the New-DBuser cmdlet to create a new object using positional parameters.
   New-DBUser 0 User User@email.com
   DBUserId UserName UserEmail      UserAddDTM          
 -------- -------- ---------      ----------          
        0 User     User@email.com 11/2/2017 3:27:50 PM
.EXAMPLE
   Uses the New-DBuser cmdlet to create a new object using named parameters.
   New-DBUser -UserName Test -UserEmail Test@email.com
   DBUserId UserName UserEmail      UserAddDTM          
 -------- -------- ---------      ----------          
        0 Test     Test@email.com 11/2/2017 3:29:42 PM
#>
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

<#
.Synopsis
   Creates a new DBCustomer object.
.DESCRIPTION
   Creates a new DBCustomer object and assigns defaults values to any non-specified parameters
.EXAMPLE
   Uses the New-DBCustomer cmdlet to create a new object using positional parameters.
   New-DBCustomer 0 Customer Customer@email.com
   DBCustomerId CustomerName CustomerEmail      CustomerAddDTM      
------------ ------------ -------------      --------------      
           0 Customer     Customer@email.com 11/2/2017 3:32:01 PM
.EXAMPLE
   Uses the New-DBCustomer cmdlet to create a new object using named parameters.
   New-DBCustomer -CustomerName Test -CustomerEmail Test@email.com
   DBCustomerId CustomerName CustomerEmail  CustomerAddDTM      
------------ ------------ -------------  --------------      
           0 Test         Test@email.com 11/2/2017 3:33:46 PM
#>
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
Get-Help Read-DBRecord -ShowWindow
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

#Creating object(s) and passing to Create-DBRecord
New-DBUser -UserName Test -UserEmail Test@email.com | Create-DBRecord
(New-DBUser -UserName User1),(New-DBCustomer -CustomerName Customer1) | Create-DBRecord
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
New-DBUser 8 | Delete-DBRecord

#Delete records(s) using properties
New-DBUser -UserName Test1 | Delete-DBRecord