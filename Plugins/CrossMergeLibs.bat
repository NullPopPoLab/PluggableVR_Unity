@echo HierarchyDumper\src\ and ..\Assets\Scripts\HierarchyDumper\
@echo NullPopPoSpecial\src\ and ..\Assets\Scripts\NullPopPoSpecial\
@echo PluggableVR\src\PluggableVR\ and ..\Assets\Scripts\PluggableVR\
@echo ...
@echo will updated to newer files, continue to.
@echo or close the window.
@pause

xcopy /d/s/y HierarchyDumper\src\*.cs ..\Assets\Scripts\HierarchyDumper\
xcopy /d/s/y ..\Assets\Scripts\HierarchyDumper\*.cs HierarchyDumper\src\
xcopy /d/s/y NullPopPoSpecial\src\*.cs ..\Assets\Scripts\NullPopPoSpecial\
xcopy /d/s/y ..\Assets\Scripts\NullPopPoSpecial\*.cs NullPopPoSpecial\src\
xcopy /d/s/y PluggableVR\src\PluggableVR\*.cs ..\Assets\Scripts\PluggableVR\
xcopy /d/s/y ..\Assets\Scripts\PluggableVR\*.cs PluggableVR\src\PluggableVR\
@pause
