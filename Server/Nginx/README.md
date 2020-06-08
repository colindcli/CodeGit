## Nginx反向代理

- [下载](http://nginx.org/en/download.html)
- [文档](http://www.nginx.cn/doc/)



**语法规则： location [=|~|~*|^~] /uri/ { … }**
    
    = 开头表示精确匹配
    
    ^~ 开头表示uri以某个常规字符串开头，理解为匹配 url路径即可。nginx不对url做编码，因此请求为/static/20%/aa，可以被规则^~ /static/ /aa匹配到（注意是空格）。
    
    ~ 开头表示区分大小写的正则匹配
    
    ~* 开头表示不区分大小写的正则匹配
    
    !~和!~*分别为区分大小写不匹配及不区分大小写不匹配 的正则
    
    / 通用匹配，任何请求都会匹配到。
    
    
    location ~ /api/ {
        proxy_pass   http://dev.oa.com:8061;
    }



## 安装服务

> [Github](https://github.com/daptiv/NginxService)，包含文件：nginxservice.exe和Topshelf.dll

注意：用admin运行cmd安装

1、将NginxService.exe复制到与nginx.exe相同的目录中
2、运行NginxService.exe install
3、运行NginxService.exe start

要卸载，只需运行即可 nginxservice.exe uninstall


## 删除服务

> sc delete 服务名称
> 如：sc delete nginx


## Nginx快捷键

- 启动：start nginx 或 nginx.exe

- 停止: nginx.exe -s stop 或 nginx.exe -s quit  （stop是快速停止nginx，可能并不保存相关信息；quit是完整有序的停止nginx，并保存相关信息。）

- 重新载入: nginx.exe -s reload

- 查看版本: nginx -v


## bug

- 代理后加载很慢，还报错如下。 解决办法：将 **localhost** 改用IP **127.0.0.1**
``` txt
2020/01/01 22:27:00 [error] 13396#9788: *73 upstream timed out (10060: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond) while connecting to upstream, client: 127.0.0.1, server: www.demo.com, request: "GET /app.js HTTP/1.1", upstream: "http://[::1]:9527/app.js", host: "localhost:8050", referrer: "http://localhost:8050/"
```

- nginx 进程杀不死

``` bat
cd /d E:\nginx-1.13.9
taskkill /f /t /im nginx.exe
start nginx
```