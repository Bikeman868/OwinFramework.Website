echo Before install script

echo Deleting prior version of website files
del /s /q /f c:\inetpub\wwwroot

echo Returning success in the case where no files were deleted - initial install
exit /b 0
