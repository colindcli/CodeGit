DIM objShell
set objShell=wscript.createObject("wscript.shell")  
iReturn=objShell.Run("cmd.exe /c E:\Run.exe 1", 0, TRUE) //0不显示窗口
