**nginx默认80端口被System占用，造成nginx启动报错**

2018/03/16 17:57:18 [emerg] 1728#22888: bind() to 0.0.0.0:80 failed (10013: An attempt was made to access a socket in a way forbidden by its access permissions)

    C:\Users\Administrator>netstat -aon | findstr :80

> 看到80端口果真被占用。发现占用的pid是4，名字是System。怎么禁用呢？
> 
> 1、打开注册表：regedit
> 
> 2、找到：HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\services\HTTP
> 
> 3、找到一个名称为Start类型为REG_DWORD，将其改为0
> 
> 4、重启系统，System进程不会占用80端口
> 
> 重启之后，start nginx.exe 。在浏览器中，输入127.0.01，即可看到亲爱的“Welcome to nginx!” 了。


- [CloseTheDoor(端口查看工具)](https://m.2cto.com/soft/201410/52975.html)

**如果以上还不能解决，尝试一下方法：**

https://jingyan.baidu.com/article/870c6fc37678c4b03fe4bef6.html
SQL Server ReportingServices (SQLEXPRESS) 服务占用80端口