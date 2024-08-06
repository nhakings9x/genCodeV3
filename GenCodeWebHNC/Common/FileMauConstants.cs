namespace GenCodeWebHNC.Common
{
    public class FileMauConstants
    {
        public const string POPUP_CONTENT = @"namespace My {
    export class Popup...Component {
        public readonly ID_POPUP: string = ""id_popup"";
        public $popup: PopupBuilder;

        constructor() {
            var div = document.createElement(""div"");
            div.id = this.ID_POPUP;
            document.body.appendChild(div);

            this.$popup = new PopupBuilder(this.ID_POPUP);
            this.onInit();
            this.bindingEvents();
        }

        public onInit() {
            this.createPopupDetail();
        }

        public bindingEvents() {
        }

        public createPopupDetail() {
            this.$popup.createPopup(option => {
                option.title(""..."")
                    .width(""800"")
                    .height(""600"")
                    .visible(false)
                    .showCloseButton(true)
                    .hideOnOutsideClick(false)
                    .contentTemplate(
                        (contentElement) => {
                            //var option = this.createGrid()
                            //contentElement.dxDataGrid(option.build());
                        }
                )
            });
        }

        public showPopupConfirm(): void {
            this.$popup.build().instance().show();
        }

        public hidePopupConfirm(): void {
            this.$popup.build().instance().hide();
        }
    }
}";

        public const string POPUP_GRID_CONTENT = @"namespace My {
    export class Popup...Component {
        public readonly ID_POPUP: string = ""id_popup"";
        public readonly ID_POPUP_GRID: string = ""id_popup_grid"";
        public $popup: PopupBuilder;

        constructor() {
            var div = document.createElement(""div"");
            div.id = this.ID_POPUP;
            document.body.appendChild(div);

            this.$popup = new PopupBuilder(this.ID_POPUP);
            this.onInit();
            this.bindingEvents();
        }

        public onInit() {
            this.createPopupDetail();
        }

        public bindingEvents() {
        }

        public createPopupDetail() {
            this.$popup.createPopup(option => {
                option.title(""..."")
                    .width(""800"")
                    .height(""600"")
                    .visible(false)
                    .showCloseButton(true)
                    .hideOnOutsideClick(false)
                    .contentTemplate(
                        (contentElement) => {
                            var option = this.createGrid()
                            contentElement.dxDataGrid(option.build());
                        }
                )
            });
        }

        public showPopupConfirm(): void {
            this.$popup.build().instance().show();
        }

        public hidePopupConfirm(): void {
            this.$popup.build().instance().hide();
        }

        private createGrid(): DataGridOptionBuilder<...> {
            var gridOptions = new DataGridOptionBuilder<...>();
            gridOptions.id(this.ID_POPUP_GRID).showBorders(true).height(""100%"")
                .columns(columns => {
                    
                })
                .dataSource(option => {
                    option.addMvc('...', '....', null, 'POST');
                });
            return gridOptions;
        }
    }
}";

        public const string POPUP_TAB_PENAL = @"namespace My {
    export class PopupSupermarketInventoryByStallsReportDetailComponent {
        public readonly POPUP_ID: string = ""popup_detail"";
        public readonly ID_GRID_NHAP: string = ""popup_grid_nhap"";
        public readonly ID_GRID_XUAT: string = ""popup_grid_xuat"";
        public $popup: PopupBuilder;
        public $gridNhap: DataGridBuilder<SupermarketInventoryByStallsReportDetailModel>;
        public $gridXuat: DataGridBuilder<SupermarketInventoryByStallsReportDetailModel>;

        constructor() {
            var div = document.createElement(""div"");
            div.id = this.POPUP_ID;
            document.body.appendChild(div);

            this.$popup = new PopupBuilder(this.POPUP_ID);
            this.onInit();
            this.bindingEvents();
        }

        public getLoadParams: (type: string) => { [key: string]: any };

        public onInit() {
            this.createPopupDetail();
        }

        public bindingEvents() {

        }

        public createPopupDetail() {
            this.$popup.createPopup(option => {
                option.title(""Chi tiết tồn kho siêu thị"")
                    .width(""80%"")
                    .height(""80%"")
                    .visible(false)
                    .showCloseButton(true)
                    .hideOnOutsideClick(false)
                    .contentTemplate(
                        (contentElement) => {
                            var option: DevExpress.ui.dxTabPanel.Properties = {
                                dataSource: [
                                    {
                                        title: ""Nhập kho"",
                                        editorOptions: this.createGridNhap()
                                    },
                                    {
                                        ""title"": ""Xuất kho"",
                                        ""editorOptions"": this.createGridXuat()
                                    }
                                ], 
                                elementAttr: { id: ""tabpanel-popup""},
                                height: '100%',
                                animationEnabled: true,
                                swipeEnabled: true,
                                tabsPosition: 'top', 
                                stylingMode: 'primary', 
                                iconPosition: 'start'
                            }
                            contentElement.dxTabPanel(option);
                        }
                )
                    .onShowing(e => this.onShowing())
            });
        }

        private onShowing(): void {
            var tabpanel = $(""#tabpanel-popup"").dxTabPanel(""instance"");
            tabpanel.option(""itemTemplate"", (itemData, itemIndex, itemElement) => {
                itemElement.dxDataGrid(itemData.editorOptions);
                itemElement.css(""padding"", ""16px"")
            })
            tabpanel.option(""selectedIndex"", 0)
        }

        //private onShown(): void {
        //    $(`#${this.ID_GRID_NHAP}`).dxDataGrid(""instance"").refresh();
        //}

        public showPopupConfirm(): void {
            this.$popup.build().instance().show();
        }

        public hidePopupConfirm(): void {
            this.$popup.build().instance().hide();
        }

        private createGridNhap(): DevExpress.ui.dxDataGrid<SupermarketInventoryByStallsReportDetailModel, any> {
            var gridNhap = new DataGridOptionBuilder<SupermarketInventoryByStallsReportDetailModel>();
            gridNhap.height('70vh').id(this.ID_GRID_NHAP).showBorders(true).height(""100%"").scrolling({ mode: ""standard"" })
                .filterRow({
                    applyFilter: ""auto"",
                    visible: true
                })
                .columns(columns => {
                    columns.addColumn('MaThamChieu', ""Mã nhập kho"").width(180);
                    columns.addColumn('NgayNhapKho', ""Ngày nhập kho"").width(150).formatDateTime(""yyyy-MM-dd"");
                    columns.addColumn('MaHang', ""Mã hàng"").width(150);
                    columns.addColumn('TenSanPham', ""Tên hàng"").minWidth(150);
                    columns.addColumn('TenDonViTinh', ""Đơn vị tính"").width(150);
                    columns.addColumn('SoLuong', ""Số lượng"").width(150);
                    columns.addColumn('LoaiNhapKho', ""Loại nhập kho"").width(150);
                })
                .summary(x => {
                    x.totalItems(items => {
                        items.addFor(""SoLuong"").summaryType(""sum"").displayFormat(""{0}"").valueFormat({ type: 'fixedPoint', precision: 3 })
                    })
                })
                .dataSource(option => {
                    option.addMvc('SupermarketInventoryByStallsReport', 'GetDataDetailWithListMaHang', this.getLoadParams(""NHAP""), 'POST');
                });
            return gridNhap.build() as DevExpress.ui.dxDataGrid<SupermarketInventoryByStallsReportDetailModel, any>;
        }

        private createGridXuat(): DevExpress.ui.dxDataGrid<SupermarketInventoryByStallsReportDetailModel, any> {
            var gridNhap = new DataGridOptionBuilder<SupermarketInventoryByStallsReportDetailModel>();
            gridNhap.height('70vh').id(this.ID_GRID_XUAT).showBorders(true).height(""100%"").scrolling({ mode: ""standard"" })
                .filterRow({
                    applyFilter: ""auto"",
                    visible: true
                })
                .columns(columns => {
                    columns.addColumn('MaThamChieu', ""Mã xuất kho"").width(180);
                    columns.addColumn('NgayXuatKho', ""Ngày xuất kho"").width(150).formatDateTime(""yyyy-MM-dd"");
                    columns.addColumn('MaHang', ""Mã hàng"").width(150);
                    columns.addColumn('TenSanPham', ""Tên hàng"").minWidth(150);
                    columns.addColumn('TenDonViTinh', ""Đơn vị tính"").width(150);
                    columns.addColumn('SoLuong', ""Số lượng"").width(150);
                    columns.addColumn('LoaiXuatKho', ""Loại xuất kho kho"").width(150);
                })
                .summary(x => {
                    x.totalItems(items => {
                        items.addFor(""SoLuong"").summaryType(""sum"").displayFormat(""{0}"").valueFormat({ type: 'fixedPoint', precision: 3 })
                    })
                })
                .dataSource(option => {
                    option.addMvc('SupermarketInventoryByStallsReport', 'GetDataDetailWithListMaHang', this.getLoadParams(""XUAT""), 'POST');
                });
            return gridNhap.build() as DevExpress.ui.dxDataGrid<SupermarketInventoryByStallsReportDetailModel, any>;
        }
    }
}";
    }
}
