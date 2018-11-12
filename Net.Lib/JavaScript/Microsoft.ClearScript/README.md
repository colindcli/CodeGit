## ClearScript [web解决方案有问题没解决]

> Install-Package Microsoft.ClearScript -Version 5.5.3


> 如果不使用在web.config移除它：

    <system.web>
        <compilation>
            <assemblies>
    
                <remove assembly="ClearScriptV8-64" />
                <remove assembly="ClearScriptV8-32" />
                ....
            </assemblies>
        </compilation>
    </system.web>