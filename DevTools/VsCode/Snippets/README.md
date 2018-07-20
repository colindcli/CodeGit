## User Snippets

- Easy Snippet Maker
- 自定义snippets路劲：C:\Users\Administrator\AppData\Roaming\Code\User\snippets 或 F1 > User Snippets > 新建全局代码片段文件
- [doc](https://code.visualstudio.com/docs/editor/userdefinedsnippets)



片段语法
    该body片段可以使用特殊的结构来控制插入光标和文字。以下是支持的功能及其语法：

制表位
    使用tabstops，您可以使编辑器光标在代码段内移动。使用$1，$2指定光标位置。数字是访问tabstops的顺序，而$0表示最终光标位置。同一个tabstop的多次出现被链接并同步更新。

占位符
    占位符是带有值的tabstops，例如${1:foo}。将插入并选择占位符文本，以便可以轻松更改。占位符可以嵌套，比如${1:another ${2:placeholder}}。

选择
    占位符可以作为值进行选择。例如，语法是逗号分隔的值枚举，用管道字符括起来${1|one,two,three|}。插入代码段并选择占位符后，选项将提示用户选择其中一个值。

变量
    使用$name或者${name:default}您可以插入变量的值。未设置变量时，将插入其默认值或空字符串。当变量未知（即，未定义其名称）时，将插入变量的名称并将其转换为占位符。

可以使用以下变量：

    TM_SELECTED_TEXT 当前选定的文本或空字符串
    TM_CURRENT_LINE 当前行的内容
    TM_CURRENT_WORD 光标下的单词内容或空字符串
    TM_LINE_INDEX 基于零索引的行号
    TM_LINE_NUMBER 基于单索引的行号
    TM_FILENAME 当前文档的文件名
    TM_FILENAME_BASE 没有扩展名的当前文档的文件名
    TM_DIRECTORY 当前文档的目录
    TM_FILEPATH 当前文档的完整文件路径
    CLIPBOARD 剪贴板的内容
    用于插入当前日期和时间：

    CURRENT_YEAR 本年度
    CURRENT_YEAR_SHORT 本年度的最后两位数
    CURRENT_MONTH 月份为两位数（例如'02'）
    CURRENT_MONTH_NAME 月份的全名（例如'July'）
    CURRENT_MONTH_NAME_SHORT 月份的简称（例如'Jul'）
    CURRENT_DATE 这个月的哪一天
    CURRENT_DAY_NAME 一天的名字（例如'星期一'）
    CURRENT_DAY_NAME_SHORT 当天的简称（例如'Mon'）
    CURRENT_HOUR 24小时时钟格式的当前小时
    CURRENT_MINUTE 目前的一分钟
    CURRENT_SECOND 目前的第二个