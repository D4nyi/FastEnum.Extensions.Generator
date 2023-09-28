Clear-Host;
Write-Host "`n`n`n`n`n";


$script:Progress = 0;
function script:Progress ([string] $Activity, [string] $Status) {
    # 8
    $script:Progress += 12.5;
    Write-Progress -Activity $Activity -Status $Status -PercentComplete $Progress;
}


function script:InvokeCommand ([string] $Command) {
    Progress -Activity "Running command..." -Status $Command;
    
    Write-Host $Command;
    Write-Host "";
    Invoke-Expression $Command | Out-Null;
}


function private:RemoveDir([string] $DirPath) {
    Progress -Activity "Previous test results are removed!" -Status $DirPath;
    
    if (Test-Path -Path $DirPath) {
        Write-Host "Deleting folder: $DirPath";
        Remove-Item $DirPath -Recurse -Force;
    } else {
        Write-Host "Folder does not exist: $DirPath";
    }
    Write-Output "";
}


function private:PrintReports([string] $TestResultsPath) {
    Progress -Activity "Collected coverage report" -Status "Done";

    $CoverageReports = (Get-ChildItem -Path $TestResultsPath -Directory | Foreach-Object { Join-Path -Path $_.FullName -ChildPath "coverage.cobertura.xml" });

    Write-Host "Found test result: $CoverageReports`n";

    return $CoverageReports;
}


function private:ExecuteTests([string] $TestProject) {
    $TestCommand = "dotnet test {0} --collect:`"XPlat Code Coverage`" --settings coverlet.runsettings.xml";

    InvokeCommand("dotnet clean");

    InvokeCommand(($TestCommand -f $TestProject));
}


function private:FixReport([string] $Path) {
    Progress -Activity "Fixing report paths" -Status "Replace URLs with filesystem paths.";

    $FileContent = Get-Content -Path $Path;

    $FileContent = $FileContent -replace "https://raw\.githubusercontent\.com/D4nyi/FastEnum\.Extensions\.Generator/[a-z0-9]{40}", $PSScriptRoot;

    Set-Content -Path $Path -Value $FileContent;
}


function private:GenerateReport([string] $CoverageReportPath) {
    $GenerateReportCommand = "reportgenerator -reports:.\test\coverageresults\**\coverage.cobertura.xml -targetdir:$CoverageReportPath -reporttypes:Html_Dark";

    InvokeCommand($GenerateReportCommand);

    InvokeCommand("start $CoverageReportPath\index.html");
}


$CoverageReportPath = ".\test\coveragereport";
$TestResultsPath    = ".\test\coverageresults";
$TestProject        = ".\test\FastEnum.Extensions.Generator.Tests.Integration\FastEnum.Extensions.Generator.Tests.Integration.csproj";


RemoveDir($CoverageReportPath);
RemoveDir($TestResultsPath);


ExecuteTests($TestProject);

Write-Host "TestResultsPath: $TestResultsPath"


$CoverageReports = PrintReports($TestResultsPath);

FixReport($CoverageReports);

GenerateReport($CoverageReportPath);


Start-Sleep -Milliseconds 1000;
