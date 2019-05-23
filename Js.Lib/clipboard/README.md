## 复制内容

> 项目地址：https://github.com/zenorocha/clipboard.js


> 安装脚本：https://github.com/zenorocha/clipboard.js/tree/master/dist



> 写法一：

```html
<button class="btn" data-clipboard-text="Copy1">Copy1</button>
```
```js
<script>
    var clipboard = new ClipboardJS('.btn');
    clipboard.on('success', function (e) {
        var text = e.text;
        console.log(text);
    });
    clipboard.on('error', function (e) {
        console.log(e);
    });
</script>
```


> 写法二：

```html
<button class="btn2">Copy2</button>
```
```js
<script>
    var cb = new ClipboardJS('.btn2', {
        text: function () {
            return 'Copy2';
        }
    });
    cb.on('success', function (e) {
        var text = e.text;
        console.log(text);
    });
    cb.on('error', function (e) {
        console.log(e);
    });
</script>
```