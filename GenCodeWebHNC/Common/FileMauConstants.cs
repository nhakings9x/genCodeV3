namespace GenCodeWebHNC.Common
{
    public class FileMauConstants
    {
        public class Popup
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
            this.createPopup();
        }

        public bindingEvents() {
        }

        public createPopup() {
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

        public showPopup(): void {
            this.$popup.build().instance().show();
        }

        public hidePopup(): void {
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
            this.createPopup();
        }

        public bindingEvents() {
        }

        public createPopup() {
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

        public showPopup(): void {
            this.$popup.build().instance().show();
        }

        public hidePopup(): void {
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

        public class CSharp
        {
            public const string SERVICE_CONTENT =
@"public class @ModelNameService : BaseService
    {
        // @ModelName: Tên model
        // @RecordName: Tên bản ghi
        // Replace xong nhớ xóa 3 dòng comment này
        private readonly @ModelNameRepository _repo;

        public @ModelNameService(IWorkingContext context) : base(context)
        {
            _repo = new @ModelNameRepository(null, context);
        }

        public async Task<Result<List<@ModelNameModel>>> GetAllAsync()
        {
            try
            {
                var entities = await _repo.FindAllAsync();
                var models = entities.MapToList<@ModelNameModel>();
                return Result.Ok(models);
            }
            catch (Exception ex)
            {
                var errorMessage = ""Lỗi lấy danh sách @RecordName"";
                Logger.Error(LogSection, ex, errorMessage);
                return Result.Exception<List<@ModelNameModel>>(errorMessage, ex);
            }
        }

        public async Task<Result> InsertAsync(string jsonValues)
        {
            string msg;
            bool b;
            //var userInsert = Context.GetUserNameWithoutDomain();
            try
            {
                var model = JsonHelper.Parse<@ModelNameModel>(jsonValues);
                var entity = model.MapTo<@ModelNameEntity>();

                //entity.Id = IdHelper.NewId();
                //entity.CreatedUser = userInsert;
                //entity.CreatedTime = DateTimeHelper.UtcNow;

                b = await _repo.InsertAsync(entity);
                if (!b)
                {
                    msg = $""Lỗi thêm @RecordName"";
                    Logger.Debug(LogSection, msg);
                    return Result.Error(msg);
                }

                return Result.Ok();
            }
            catch (Exception ex)
            {
                msg = ""Thêm @RecordName thất bại"";
                Logger.Error(LogSection, ex, msg);
                return Result.Exception(msg, ex);
            }
        }

        public async Task<Result> UpdateAsync(string id, string patchJsonValues)
        {
            string msg;
            bool b;
            //var userUpdate = Context.GetUserNameWithoutDomain();
            try
            {
                var entity = await _repo.GetAsync(id);
                if (entity == null)
                {
                    msg = $""Không tìm thấy @RecordName có id [{id}]"";
                    Logger.Debug(LogSection, msg);
                    return Result.Error(Result.DATA_NOT_EXISTED.Code, msg);
                }

                var patchModel = entity.MapTo<@ModelNameModel>();

                //entity.UpdateTime = DateTimeHelper.UtcNow;
                //entity.UpdatedUser = userUpdate;

                b = await _repo.UpdateAsync(entity);
                if (!b)
                {
                    msg = $""Lỗi cập nhật @RecordName có thông tin [{id}]"";
                    Logger.Debug(LogSection, msg);
                    return Result.Error(msg);
                }

                return Result.Ok();
            }
            catch (Exception ex)
            {
                msg = $""Lỗi cập nhật @RecordName có id [{id}]"";
                Logger.Error(LogSection, ex, msg);
                return Result.Exception(msg, ex);
            }
        }

        public async Task<Result> DeleteAsync(string id)
        {
            var b = id.IsEmpty() || id.Length <= 3;
            if (b)
            {
                var mes = ""Dữ liệu @RecordName không hợp lệ để xóa"";
                Logger.Debug(LogSection, mes);
                return Result.Error(Result.DATA_INVALID.Code, mes);
            }
            try
            {
                var entity = await _repo.GetAsync(id);
                if (entity == null)
                {
                    var msg = $""Không tìm thấy @RecordName có id [{id}] để xóa"";
                    Logger.Debug(LogSection, msg);
                    return Result.Error(Result.DATA_NOT_EXISTED.Code, msg);
                }

                b = await _repo.DeleteAsync(entity);

                if (!b)
                {
                    var msg = $""Lỗi xóa @RecordName có id [{id}]"";
                    Logger.Debug(LogSection, msg);
                    return Result.Error(msg);
                }
                return Result.Ok();
            }
            catch (Exception ex)
            {
                var msg = $""Lỗi xóa @RecordName có id [{id}]"";
                Logger.Error(LogSection, ex, msg);
                return Result.Exception(msg, ex);
            }
        }
    }";

            public const string CONTROLLER_CONTENT =
                @"[Route(""[controller]/[action]"")]
    public class @ModelNameController : BaseWebController
    {
        // @ModelName: Tên model
        // @RecordName: Tên bản ghi
        // Replace xong nhớ xóa 3 dòng comment này
        private readonly @ModelNameService _service;

		public @ModelNameController(IWebContext<@ModelNameController> context) : base(context)
        {
            _service = new @ModelNameService(context);
		}

        [WebPagePermission]
        public IActionResult Index()
        {
            return View();
        }

        [WebReadPermission]
        [HttpPost]
        public async Task<IActionResult> GetData()
        {
            var result = await _service.GetAllAsync();
            if (!result.IsSuccess())
            {
                return BadRequest(""Xảy ra lỗi, vui lòng thử lại!"");
            }

            var modelView = result.Data.MapToList<@ModelNameViewModel>();
            var result = modelView.ConvertToLoadResult();
            return Json(result);
        }

        [WebInsertPermission]
        [HttpPost]
        public async Task<IActionResult> Insert(string values)
        {
            if (!values.HasMinLength(Constants.JSON_MIN_LENGTH))
            {
                return BadRequest(""Dữ liệu @RecordName không hợp lệ để thêm mới"");
            }

            var r = await _service.InsertAsync(values);
            if (r.IsOk())
            {
                return Ok(r);
            }

            return BadRequest(r.Message);
        }

        [WebUpdatePermission]
        [HttpPost]
        public async Task<IActionResult> Update(string key, string values)
        {
            if (!key.HasMinLength(Constants.NAME_MIN_LENGTH) || !values.HasMinLength(Constants.JSON_MIN_LENGTH))
            {
                return BadRequest(""Dữ liệu @RecordName không hợp lệ để cập nhật"");
            }

            var r = await _service.UpdateAsync(key, values);
            if (r.IsOk())
            {
                return Ok(r);
            }

            return BadRequest(r.Message);
        }

        [WebDeletePermission]
        [HttpPost]
        public async Task<IActionResult> Delete(string key)
        {
            if (!key.HasMinLength(Constants.ID_MIN_LENGTH))
            {
                return BadRequest(""Dữ liệu @RecordName không hợp lệ để xóa"");
            }

            var r = await _service.DeleteAsync(key);
            if (r.IsOk())
            {
                return Ok(r);
            }

            return BadRequest(r.Message);
        }
    }";
        }
    }
}
