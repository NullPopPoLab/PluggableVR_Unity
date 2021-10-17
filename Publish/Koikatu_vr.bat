cd %~dp0
if not exist Koikatu_Data\globalgamemanagers_vr goto Error
if not exist Koikatu_Data\globalgamemanagers_novr copy Koikatu_Data\globalgamemanagers Koikatu_Data\globalgamemanagers_novr
copy /y Koikatu_Data\globalgamemanagers_vr Koikatu_Data\globalgamemanagers
start Koikatu.exe
goto End
:Error
@echo .
@echo *** Error ***
@echo It needs the VR-hacked globalgamemanagers
@echo see PluggableVR_HowToUse.html
pause
:End
