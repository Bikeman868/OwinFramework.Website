echo After install script

echo Downloading NuGet packages using Paket

.paket\paket.exe restore > restore.log 2> restore.err
