cd /d %~dp0
if not exist StudioNEOV2_Data\globalgamemanagers_vr goto Error
if not exist StudioNEOV2_Data\globalgamemanagers_novr copy StudioNEOV2_Data\globalgamemanagers StudioNEOV2_Data\globalgamemanagers_novr
copy /y StudioNEOV2_Data\globalgamemanagers_vr StudioNEOV2_Data\globalgamemanagers
start StudioNEOV2.exe
goto End
:Error
@echo .
@echo *** Error ***
@echo It needs the VR-hacked globalgamemanagers
@echo see PluggableVR_HS2_HowToUse.html
pause
:End
