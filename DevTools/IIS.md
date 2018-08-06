## IIS

- 禁用缓存：输出缓存 - 添加- “*” 启用用户模式缓存（使用文件更改通知）、内核模式缓存（使用文件更改通知）
<system.webServer>
    <caching>
        <profiles>
            <add extension="*" policy="CacheUntilChange" kernelCachePolicy="CacheUntilChange" />
      </profiles>
    </caching>
</system.webServer>


- 反向代理: [proxy](https://github.com/colindcli/CodeGit/issues/19)

- 代理：[AnyProxy](https://github.com/alibaba/anyproxy) / [doc](http://anyproxy.io/cn/) apache2
