namespace GenCodeWebHNC.Common
{
    public class GenCodeTsConstans
    {
        public const string BASE_INDEX_CONTENT = 
@"namespace My {
        export class @IndexFileName extends BaseDevExGridForm<@IndexModel, @FormModel, @OptionModel>{
            constructor(container: JQuery, formData: @FormModelContructor, option: @OptionModelContructor) {
                super(container, formData, option);
            }

            protected onInit(): void {
                this.excelFileName = '@IndexFileExcelName';
                super.onInit();
                @FormSearchContent
                this.$gridBuilder.createDataGrid(opts => opts.addSearchPanel(LanguageKey.Common.Search).height('70vh')
                    @ColumnGridContent

                    .dataSource(option => {
                        option.addMvc('...', '...', @LoadParams, 'POST');
                    })
                );
            }

            protected bindingEvents(): void {
                super.bindingEvents();
                this.btnSearch.on(""click"", () => {
                    var grid = this.$gridBuilder.build();
                    grid.refresh();
                });
            }
            @LoadparamsFunc
        }
    }";

        public const string BASE_FORM_DATA_CONTENT = "\n                    this.$formBuilder.createForm(opts => opts.formData(this.formData)\r\n                        @FormSearchItems\r\n                    );\n";

        public const string LOADPARAMS_FUNC = 
@"
                private getLoadParams() {
                    return {
@LoadParamReturn
                    }
                }";

        public const string BASE_SERVICE_CONTENT =
@"namespace My {
    export class @IndexFileNameService extends BaseService {
        public static readonly ReturnUrl: string = ""ReturnUrl"";

        constructor(baseUrl?: string) {
            super(Utils.isEmpty(baseUrl) ? ""/@IndexFileName"" : baseUrl);
        }

        //public async getJsonDemo() {
        //    var url = ""/MethodName"";
        //    var res = await this.getJson(url);
        //    return res.result;
        //}

        //public async postJsonDemo(req) {
        //    var url = ""/MethodName"";
        //    var res = await this.postJson(url, req);
        //    return res.result;
        //}
    }
}";

        public const string BASE_VIEW_CONTENT =
@"@model ...;
@{
    var title = ""..."";
    var breadCrumbUrl = Url.Action<@IndexFileNameController>(x => x.Index());
    ViewData[""title""] = title;
    Layout = WebSharedConst.LAYOUT_ADMIN_DEVEXDATAGRID;
    Options.IsSearchPage = true;
    Options.Title = title;
    Options.BreadCrumb = new WebBreadCrumbModel
    {
        Items = {
            new WebBreadCrumbItem(""/"", ""Home""),
            new WebBreadCrumbItem(breadCrumbUrl, title, true)
        }
    };
}

<script>
    $(document).ready(function () {
        var form = new My.@IndexFileNameIndex($('#page-content'), @FormModel, @OptionModel);
        form.init();
    });
</script>";
    }
}
