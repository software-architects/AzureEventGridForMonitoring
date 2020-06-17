$location = "westeurope"
$stage = "dev"
$resourceGroupName = "$stage-rg-egmon"

.\Deploy-AzTemplate.ps1 `
    -ArtifactStagingDirectory '.\' `
    -ResourceGroupName $resourceGroupName `
    -ResourceGroupLocation $location `
    -TemplateFile '.\deploy.json' `
    -TemplateParametersFile ".\parameters-$stage.json"