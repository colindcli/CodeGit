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