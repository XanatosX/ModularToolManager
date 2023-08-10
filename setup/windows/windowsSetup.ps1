param(
    [String][Parameter(Mandatory, HelpMessage="Enter the version of the installation")]$version,
    [String][Parameter(HelpMessage="The directory to put the resulting setup file in")]$outdir = "output",
    [String][Parameter(HelpMessage="The folder containing the build")]$buildfolder = ""
) 

# Switch to setup location
$dir = split-path $MyInvocation.MyCommand.Path -Parent
Push-Location $dir

# Write information and create setup
Write-Output "Output directory was set to $outdir"
Write-Output "Install version in $version"

if ([string]::IsNullOrEmpty($buildfolder))
{
    Write-Output "Use default build folder"
    iscc /DMyAppVersion=$version /O$outdir .\ModularToolManagersSetup.iss 
} else 
{
    Write-Output "Use $buildfolder as build folder"
    iscc /DMyAppVersion=$version /DBuildFolder=$buildfolder /O$outdir .\ModularToolManagersSetup.iss 
}

# Switch back to original location
Pop-Location