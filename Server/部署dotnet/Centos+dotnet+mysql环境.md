# 系统版本：centos 7.5 64位

## Ubuntu服务器安装图形化界面

## 打开防火墙

```
#开机启动
systemctl enable firewalld.service

#启动防火墙
systemctl start firewalld

#打开防火墙端口，关闭firewall-cmd --zone=public --remove-port=3306/tcp --permanent
firewall-cmd --zone=public --add-port=22/tcp --permanent
firewall-cmd --zone=public --add-port=80/tcp --permanent
firewall-cmd --zone=public --add-port=3306/tcp --permanent

#重新加载
firewall-cmd --reload

#检查防火墙状态
systemctl status firewalld
```

## 安装mysql 5.7

```
#配置YUM源
wget http://dev.mysql.com/get/mysql57-community-release-el7-8.noarch.rpm
yum localinstall mysql57-community-release-el7-8.noarch.rpm
yum repolist enabled | grep "mysql.*-community.*"
```

```
#安装MySQL
yum install mysql-community-server

#开启
systemctl start mysqld
systemctl status mysqld

#开机启动
systemctl enable mysqld
systemctl daemon-reload
```

- 修改root本地登录密码

```
#查看初始化密码
grep 'temporary password' /var/log/mysqld.log

mysql -u root -p

-- 解决报错：Your password does not satisfy the current policy requirements
set global validate_password_policy=0;
set global validate_password_length=1;

-- 修改密码
ALTER USER 'root'@'localhost' IDENTIFIED BY '123456';

-- 设置访问权限
grant all privileges on *.* to 'root' @'%' identified by '123456'; 

flush privileges;
exit;
```

- 重启

```
systemctl restart mysqld;
```

## 安装Ftp


```
# 查看是否安装了相关软件
rpm -qa|grep -E "openssh"

# 安装openssh-server
sudo yum install openssh*

# 注册使用服务
sudo systemctl enable sshd
sudo systemctl start sshd
```


## 安装nginx

- 安装
```
# 安装nginx
yum install nginx -y

# 启动nginx（需要先确保80端口未被其他程序占用）
systemctl start nginx

# 设为开机启动
systemctl enable nginx
```

- 配置: `/etc/nginx/nginx.conf`

```
server {
    listen        80;
    server_name  _;
    location / {
        proxy_pass         http://localhost:5000;
        proxy_http_version 1.1;
        proxy_set_header   Upgrade $http_upgrade;
        proxy_set_header   Connection keep-alive;
        proxy_set_header   Host $host;
        proxy_cache_bypass $http_upgrade;
        proxy_set_header   X-Forwarded-For $proxy_add_x_forwarded_for;
        proxy_set_header   X-Forwarded-Proto $scheme;
    }
}
```

- 重启

```
sudo nginx -t

sudo nginx -s reload
```

## 安装dotnet

```
sudo rpm -Uvh https://packages.microsoft.com/config/centos/7/packages-microsoft-prod.rpm
```

```
sudo yum install dotnet-sdk-3.1
```

## 安装supervisor

```
sudo yum install supervisor

#开机自启
systemctl enable supervisord

#启动supervisord
systemctl start supervisord

#查看状态
systemctl status supervisord
```

```
cd /etc/supervisord.d/
sudo touch projectCore.ini
```

- 打开文件`/etc/supervisord.d/projectCore.ini`添加

```
[program:projectCore]
command=dotnet /home/www/ProjectCore.Web.dll                  ;被监控的进程路径
numprocs=1                      ; 启动一个进程
directory=/home/www/            ;执行前切换路径
autostart=true                  ; 随着supervisord的启动而启动
autorestart=true                ; 自动重启
startretries=10                 ; 启动失败时的最多重试次数
exitcodes=0                     ; 正常退出代码
stopsignal=KILL                 ; 用来杀死进程的信号
stopwaitsecs=10                 ; 发送SIGKILL前的等待时间
redirect_stderr=true            ; 重定向stderr到stdout
stdout_logfile=logfile          ; 指定日志文件
```

```
supervisorctl reload
supervisorctl start projectCore
```