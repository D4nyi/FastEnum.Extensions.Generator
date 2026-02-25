Clear-Host;

$UtcNow = [DateTime]::UtcNow
$BuildTime = $UtcNow.ToString('yyyy-MM-ddTHH:mm:ssZ')
$ReleaseYear = $UtcNow.Year

function script:UpdateProjectVersion([string] $CsprojPath, [string] $CsprojName) {
    $ConstantsPath = Split-Path $CsprojPath
    $ConstantsPath = Join-Path $ConstantsPath "Constants.cs"

    $constants = (Get-Content -Path $ConstantsPath -Encoding UTF8)
    
    $csproj = (Get-Content -Path $CsprojPath -Encoding UTF8)

    $ms = [regex]::Matches($constants, '(?m)Version = "([^"]+)";')

    $currentVersion = $ms.Groups[1]
    
    $nextVersion = Read-Host "Next version of $CsprojName ($currentVersion)"

    if ([string]::IsNullOrWhiteSpace($nextVersion)) {
        Write-Host "Not modified" -ForegroundColor Yellow
        return
    }
    
    $constants = $constants -replace '(?m)Version = "([^"]+)";', "Version = `"$nextVersion`";"

    $csproj = $csproj -replace "<Version>.*</Version>", "<Version>$nextVersion</Version>"
    $csproj = $csproj -replace "<InformationalVersion>.*</InformationalVersion>", "<InformationalVersion>$nextVersion Built: $BuildTime</InformationalVersion>"
    $csproj = $csproj -replace "<Copyright>.*</Copyright>", "<Copyright>Copyright © $ReleaseYear. Dániel Szöllősi</Copyright>"

    Set-Content -Path $ConstantsPath -Value $constants -Encoding UTF8
    Set-Content -Path $CsprojPath -Value $csproj -Encoding UTF8
}


$SrcPath = Join-Path $PSScriptRoot "src"

Get-ChildItem -Path $SrcPath -File -Filter "*.csproj" -Recurse -Depth 1 | ForEach-Object {
    UpdateProjectVersion -CsprojPath $_.FullName -CsprojName $_.Name
}
