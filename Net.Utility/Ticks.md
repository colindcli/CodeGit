## 时间戳

- C#时间戳(毫秒)：(DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000
- JS时间戳(毫秒)：new Date().getTime()
- SqlServer(秒): SELECT DATEDIFF(s, '1970-01-01 00:00:00', GETUTCDATE())


- 更多：[matools](http://www.matools.com/timestamp) / [jb51](http://tools.jb51.net/code/unixtime)