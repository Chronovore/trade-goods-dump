#Constants
[string]$SUB_MODULE_FILE_NAME = 'SubModule.xml';

###>===---Per Project Settings---===<###
[string]$ModuleName = 'TradeGoodsDump';
[string]$Source = $PSScriptRoot;
[string]$CompileSource = "$Source\src\bin\x64\Debug";
[string]$ModuleDestination = 'C:\Program Files (x86)\Steam\steamapps\common\Mount & Blade II Bannerlord\Modules';
###>===---Per Project Settings---===<###

#Computed Settings
[string]$DeployDestination = "$ModuleDestination\$ModuleName";
[string]$DeployLibraryDestination = "$DeployDestination\bin\Win64_Shipping_Client";
[string]$LibraryName = "$ModuleName.dll";

###>===---Per Project Deploy Files---===<###
$FilesToDeploy = @{
    #SubModule
    $SUB_MODULE_FILE_NAME = @($Source, $DeployDestination);
    #DLL
    $LibraryName = @($CompileSource, $DeployLibraryDestination);
};
###>===---Per Project Deploy Files---===<###

function Deploy-File {
    Param($file, $src, $dest)

    Copy-Item -Path "$src\$file" -Destination "$dest\$file" -Force;
    Write-Host "Deployed $file from $src to $dest";
}

Write-Host "Starting Deployment..."
foreach($fileInfo in $FilesToDeploy.GetEnumerator())
{
    Deploy-File -file $fileInfo.Key -src $fileInfo.Value[0] -dest $fileInfo.Value[1];
}
Write-Host "Deployment Complete."