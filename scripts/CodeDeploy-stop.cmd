echo Application stop script

echo Stopping IIS

iisreset /stop

echo Deleting prior version of website files ignoring errors

del /s /q /f c:\inetpub\wwwroot 2>nul


