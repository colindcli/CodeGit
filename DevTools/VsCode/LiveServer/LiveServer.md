## LiveServer

- 代理 (host也使用域名)
    
    // To switch between localhost or 127.0.0.1 or anything else. Default is 127.0.0.1
    "liveServer.settings.host": "www.demo.com",
    // To Setup Proxy
    "liveServer.settings.proxy": {
        "enable": true,
        "baseUri": "/api/",
        "proxyUri": "http://www.demo.com/api/"
    },

    // 注：如果在c盘，请设置根目录权限。