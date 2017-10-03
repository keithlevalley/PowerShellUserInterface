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
        [ValidateSet("USB001_USER_MASTER.mdf","Medium","High")]
        [string]
        $DatabaseName,

        # Param2 help description
        [Parameter(Mandatory=$true,
                   Position=1)]
        [ValidateSet("UST001_USER_MSTR","Medium","High")]
        [string]
        $TableName,

        # Param3 help description
        [Parameter(Mandatory=$true,
                   Position=2)]
        [ValidateSet("Create","Read","Update", "Delete")]
        [string]
        $OperationType,

        # Param4 help description
        [Parameter(Mandatory=$true,
                   Position=3)]
        [string]
        $Username,

        # Param5 help description
        [Parameter(Mandatory=$true,
                   Position=4)]
        [string]
        $UserFirstName,

        # Param6 help description
        [Parameter(Mandatory=$true,
                   Position=5)]
        [string]
        $UserLastName,

        # Param7 help description
        [Parameter(Mandatory=$true,
                   Position=6)]
        [string]
        $UserEmail,

        # Param8 help description
        [Parameter(Mandatory=$false,
                   Position=7)]
        [string]
        $UserPhoneNumber
    )

    Begin
    {
    }
    Process
    {
        Start-Process -FilePath .\PowerShellDataAccess\PowerShellDataAccess\bin\Release\PowerShellDataAccess.exe -ArgumentList $DatabaseName, $TableName, $OperationType, "USER_NM", $Username, "USER_FNM", $UserFirstName, "USER_LNM", $UserLastName, "USER_EMAIL", $UserEmail, "USER_PHONE_NUM", $UserPhoneNumber -Wait
    }
    End
    {
    }
}

Create-DBRecord -DatabaseName USB001_USER_MASTER.mdf -TableName UST001_USER_MSTR -OperationType Create -Username JDoe -UserFirstName John -UserLastName Doe -UserEmail jdoe@example.com -UserPhoneNumber 111-11-1111