## MvcPaging 分页

- [MvcPaging](https://www.nuget.org/packages/MvcPaging/) / [doc](https://github.com/martijnboland/MvcPaging)


1、View > Shared > DisplayTemplates 添加 Pager.cshtml

2、Web.config添加

<system.web>
    <pages>
      <namespaces>
        <add namespace="MvcPaging" />
      </namespaces>
    </pages>
</system.web>

3、cshtml添加

@Html.Pager(Model.PageSize, Model.PageIndex, Model.Total).Options(o => o
        .DisplayTemplate("pager")
        .DisplayFirstAndLastPage())