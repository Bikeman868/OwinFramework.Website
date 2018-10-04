echo After install script

echo Downloading NuGet packages using Paket

cd c:\inetpub\wwwroot
.paket\paket.exe restore > restore.log 2> restore.err

echo Copying NuGet packages to the bin folder

DeployPackages net40 packages web\bin
