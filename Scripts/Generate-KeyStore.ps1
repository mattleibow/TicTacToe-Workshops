[CmdletBinding()]
Param(
    [Parameter(Mandatory=$true)]
    [string]$Alias,
    [string]$Path = "keystore.keystore"
)

keytool -genkeypair -v -keystore "$Path" -alias "$Alias" -keyalg RSA -keysize 2048 -validity 10000
