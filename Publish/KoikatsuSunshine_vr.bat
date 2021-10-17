cd %~dp0
if not exist KoikatsuSunshine_Data\globalgamemanagers_vr goto Error
if not exist KoikatsuSunshine_Data\globalgamemanagers_novr copy KoikatsuSunshine_Data\globalgamemanagers KoikatsuSunshine_Data\globalgamemanagers_novr
copy /y KoikatsuSunshine_Data\globalgamemanagers_vr KoikatsuSunshine_Data\globalgamemanagers
start KoikatsuSunshine.exe
goto End
:Error
@echo .
@echo *** Error ***
@echo It needs the VR-hacked globalgamemanagers
@echo see PluggableVR_KKS_HowToUse.html
pause
:End
