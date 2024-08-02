namespace GenCodeWebHNC.Common
{
    public class GenCodeTsConstan
    {
        public const string BASE_INDEX_CONTENT = @"namespace My {
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

        public const string BASE_FORM_DATA_CONTENT = "this.$formBuilder.createForm(opts => opts.formData(this.formData)\r\n                        @FormSearchItems\r\n                    );\n";

        public const string LOADPARAMS_FUNC = @"private getLoadParams() {
                    return {
@LoadParamReturn
                    }
                }";
    }
}
