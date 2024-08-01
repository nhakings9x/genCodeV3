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
            this.$formBuilder.createForm(opts => opts.formData(this.formData)
                .items(items => {
                    items.addSimpleFor('MaKhoHang', ""Mã kho hàng"")
                        .editor(e => e.createTextBox('maKhoHang', this.formData.MaKhoHang));
                    items.addSimpleFor('FromDate', LanguageKey.Common.FromDate)
                        .editor(e => e.createDateBox('fromDate', this.formData.FromDate));
                    items.addSimpleFor('ToDate', LanguageKey.Common.ToDate)
                        .editor(e => e.createDateBox('toDate', this.formData.ToDate));
                })
            );

            this.$gridBuilder.createDataGrid(opts => opts.addSearchPanel(LanguageKey.Common.Search).height('70vh')
                .columns(columns => {
                    columns.addColumn('MaHang', ""Mã hàng"").width(150);
                    columns.addColumn('TenHang', ""Tên sản phẩm"").width(250);
                    columns.addColumn('DonViTinh', ""Đơn vị tính"").width(100);
                    columns.addColumn('NhomSanPham', ""Nhóm sản phẩm"").width(150);
                    columns.addColumn('Stalls', ""Quầy hàng"").width(250);
                })

                .dataSource(option => {
                    option.addMvc('...', '...', this.getLoadParams(), 'POST');
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

        private getLoadParams() {
            return {
                FromDate: () => { return this.$formBuilder.getDateBoxString('FromDate') },
                ToDate: () => { return this.$formBuilder.getDateBoxString('ToDate') },
                MaKhoHang: () => { return this.$formBuilder.getData().MaKhoHang }
            }
        }
    }
}";
    }
}
