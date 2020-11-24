# 系统版本 windows 2012数据中心版 64位中文版

## 下载

```
https://dev.mysql.com/downloads/mysql/5.7.html

https://cdn.mysql.com/archives/mysql-5.7/mysql-5.7.31-winx64.zip
```

## 缺少msvcp120.dll

无法启动程序,因为计算机中丢失 MSVCP120dl尝试重新安装该程序以解决此间题
下载并安装Visual C++ Redistributable Packages for Visual Studio 2013, 有三个版本, 根据自己的系统版本下载, 下载完成后, 安装即可;
下载地址: https://www.microsoft.com/zh-CN/download/details.aspx?id=40784


## 创建my.ini

```
[client]
port=3306
[mysql]
# 设置mysql客户端默认字符集
default-character-set=utf8
  
[mysqld]
# 设置3306端口
port = 3306
# 设置mysql的安装目录
basedir=C:\mysql-5.7.31-winx64
# 设置mysql数据库的数据的存放目录
datadir=C:\mysql-5.7.31-winx64\data
# 允许最大连接数
max_connections=200
# 服务端使用的字符集默认为8比特编码的latin1字符集
character-set-server=utf8
# 创建新表时将使用的默认存储引擎
default-storage-engine=INNODB
# 0：大小写敏感；1：大小写不敏感
lower_case_table_names=2
```

## 配置环境变量

新建”一个名为 `MYSQL_HOME` 的变量。变量值：`C:\mysql-5.7.31-winx64`

编辑现有的环境变量 `Path`，在最后增加 `%MYSQL_HOME%\bin` ，注意用英文分号（;）隔开。


## 注册windows系统服务

```
cd /d C:\mysql-5.7.31-winx64\bin

mysqld install MySQL –defaults-file="C:\mysql-5.7.31-winx64\my.ini"
```

## 修改注册表值

```
cmd > regedit
HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\MySQL
ImagePath
C:\mysql-5.7.31-winx64\bin\mysqld.exe
```

## 启动服务

```
net start mysql
```

## 自动创建data

```
mysqld –initialize
```

## 修改密码

有一个初始密码在data目录下有个以 计算机名 `.err` 的文件, 使用这个密码进入mysql; 如果这里报错, 请看下面的问题集合

- 登录

```
mysql -u root -p
```

- 修改

```
alter user 'root'@'localhost' identified by '123456';
exit;
```


- 设置远程连接

```
show databases;
use mysql
show tables;
select * from user \G

update mysql.user set host='%' where user='root'; 

flush privileges;

exit;
