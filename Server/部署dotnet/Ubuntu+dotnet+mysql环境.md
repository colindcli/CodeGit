# 系统版本：ubuntu 16.04 64位

## Ubuntu服务器安装图形化界面

```
sudo apt-get update
apt-get install ubuntu-desktop #安装桌面软件
reboot #重启
```


## 开启防火墙

```
#开启防火墙，如果要关闭：sudo ufw disable
sudo ufw enable

#开启端口，如果要删除：ufw delete allow 3306
ufw allow 80
ufw allow 22
ufw allow 3306

sudo ufw status
```


## 安装mysql 5.7

```
sudo apt-get update
sudo apt-get install mysql-server
# 安装过程中，选择y; 输入mysql登录密码。
```

- 打开文件/etc/mysql/mysql.conf.d/mysqld.cnf，在`[mysqld]`下添加：

```
# 修改允许远程连接
bind-address = 0.0.0.0
```

- 授权远程连接

```
mysql -u root -p

grant all on *.* to root@'%' identified by '123456';

flush privileges;

exit;
```

- 重启

```
sudo /etc/init.d/mysql restart
```

- 远程部署mysql数据库


- (如需要修改) 打开文件/etc/mysql/my.cnf，在`[mysqld]`下添加：

```
# 添加：0区分大小写; 1不区分大小写
lower_case_table_names=0
```


## 安装Ftp

```
sudo apt-get install openssh-server
```

主机：ip 用户名：root  密码：Ubuntu系统密码 端口：22

上传文件到目录，如：/home/www/


## 安装nginx

```
sudo apt-get update
sudo apt-get install nginx
```

```
sudo service nginx start
```

- 打开文件/etc/nginx/sites-enabled/default

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
wget https://packages.microsoft.com/config/ubuntu/16.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
```

```
sudo apt-get update; \
  sudo apt-get install -y apt-transport-https && \
  sudo apt-get update && \
  sudo apt-get install -y dotnet-sdk-3.1
```

## 安装supervisor

```
sudo apt-get install supervisor
```

```
cd /etc/supervisor/conf.d/
sudo touch projectCore.conf
```

- 打开文件/etc/supervisor/conf.d/projectCore.conf添加

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