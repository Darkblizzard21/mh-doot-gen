@setlocal enableextensions
@pushd %~dp0
@echo off
set LISTFILE=mhrisePC.list
if "%~1"=="" (
    echo Drag re_chunk_000.pak from the Monster Hunter Rise folder onto extract-rise-pak.bat to extract it.
) else (
	if not exist ".\tmp\RETool.exe" (
		echo RETool is missing. Downloading it from GitHub...
		powershell wget https://raw.githubusercontent.com/mhvuze/MonsterHunterRiseModding/main/files/REtool.exe -OutFile ".\tmp\RETool.exe"
	) else (
	echo RETool.exe found.
	)
	
    if not exist ".\tmp\%LISTFILE%" (
		echo %LISTFILE% is missing. Downloading it from GitHub...
		powershell wget https://raw.githubusercontent.com/mhvuze/MonsterHunterRiseModding/main/files/%LISTFILE% -OutFile ".\tmp\%LISTFILE%"
	) else (
	echo %LISTFILE% found.
	)
	echo Running RETool.exe...

	if not exist "./re_files" (
            mkdir "./re_files"
            if errorlevel 1 (
            	echo Failed to create folder: "./re_files"
            ) else (
            	echo Folder created: "./re_files"
            )
	)
	cd re_files
	..\tmp\REtool.exe -h "..\tmp\%LISTFILE%" -x -skipUnknowns %1 -noExtractDir
	cd ..
)
@popd
@pause