cd /d %~dp0
if not exist CharaStudio_Data\globalgamemanagers_vr goto Error
if not exist CharaStudio_Data\globalgamemanagers_novr copy CharaStudio_Data\globalgamemanagers CharaStudio_Data\globalgamemanagers_novr
copy /y CharaStudio_Data\globalgamemanagers_vr CharaStudio_Data\globalgamemanagers
start CharaStudio.exe
goto End
:Error
@echo .
@echo *** Error ***
@echo It needs the VR-hacked globalgamemanagers
@echo see PluggableVR_KK_HowToUse.html
pause
:End
