param(
    [Parameter(Mandatory=$true)]
    [string]$apiId,

    [Parameter(Mandatory=$true)]
    [string]$displayName,

    [Parameter(Mandatory=$true)]
    [string]$baseUrl,

    [Parameter(Mandatory=$true)]
    [string]$docsPath,

    [Parameter(Mandatory=$true)]
    [ValidateSet("Explicit","AutoIncrement","UpdateLatest")]
    [string]$versionUpdate,

    [Parameter(Mandatory=$false)]
    [string]$versionNumber,
    
    [Parameter(Mandatory=$false)]
    [string]$buildOrReleaseTag
)

$getResponse = madot api-get --id $apiId | ConvertFrom-Json

if($getResponse.PSObject.Properties.Name -contains "notFound") {
    
    $insertResponse = madot api-insert --id $apiId --display-name $displayName --base-url $baseUrl
    
    if ($insertResponse.PSObject.Properties.Name -contains "error") {
        exit 1
    }
}

$mergeResponse = madot docs-merge --api-id $apiId --docs-path $docsPath


switch ($versionUpdate) {
    "Explicit" {
        if ($versionNumber){
            madot apiversion-publish --api-id $apiId --version-number $versionNumber --tag $buildOrReleaseTag
        }        
    }
    
    "AutoIncerment" {
        madot apiversion-publish --api-id $apiId --auto-increment --tag $buildOrReleaseTag
    }
    
    "Updatelatest" {
        madot apiversion-publish --api-id $apiId --update-latest --tag $buildOrReleaseTag
    }
}



