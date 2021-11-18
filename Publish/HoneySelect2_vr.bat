cd /d %~dp0
if not exist HoneySelect2_Data\globalgamemanagers_vr goto Error
if not exist HoneySelect2_Data\globalgamemanagers_novr copy HoneySelect2_Data\globalgamemanagers HoneySelect2_Data\globalgamemanagers_novr
copy /y HoneySelect2_Data\globalgamemanagers_vr HoneySelect2_Data\globalgamemanagers
start HoneySelect2.exe
goto End
:Error
@echo .
@echo *** Error ***
@echo It needs the VR-hacked globalgamemanagers
@echo see PluggableVR_HS2_HowToUse.html
pause
:End
