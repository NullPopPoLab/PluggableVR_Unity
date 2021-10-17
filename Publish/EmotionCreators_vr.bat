cd %~dp0
if not exist EmotionCreators_Data\globalgamemanagers_vr goto Error
if not exist EmotionCreators_Data\globalgamemanagers_novr copy EmotionCreators_Data\globalgamemanagers EmotionCreators_Data\globalgamemanagers_novr
copy /y EmotionCreators_Data\globalgamemanagers_vr EmotionCreators_Data\globalgamemanagers
start EmotionCreators.exe
goto End
:Error
@echo .
@echo *** Error ***
@echo It needs the VR-hacked globalgamemanagers
@echo see PluggableVR_EC_HowToUse.html
pause
:End
