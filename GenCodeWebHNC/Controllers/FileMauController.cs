using GenCodeWebHNC.Models;
using Microsoft.AspNetCore.Mvc;

namespace GenCodeWebHNC.Controllers
{
    public class FileMauController : Controller
    {

        public FileMauController()
        {
        }

        public IActionResult Index()
        {
            var files = new List<FileItemModel>
            {
                new FileItemModel
                {
                    FileName = "Parent Folder",
                    Children = new List<FileItemModel>
                    {
                        new() {
                            FileName = "Forms",
                            Children = new List<FileItemModel>
                            {
                                new FileItemModel { FileName = "SupermarketInventoryByStallsReportIndex.ts" }
                            }
                        },
                        new() {
                            FileName = "Models",
                            Children = new List<FileItemModel>
                            {
                                new FileItemModel { FileName = "SupermarketInventoryByStallsReportModel.ts" },
                                new FileItemModel { FileName = "SupermarketInventoryByStallsReportSearchModel.ts" },
                                new FileItemModel { FileName = "GenCodeTsFileResponse.ts" },
                                new FileItemModel { FileName = "SupermarketInventoryByStallsReportService" }
                            }
                        },
                        new() {
                            FileName = "Views",
                            Children = new List<FileItemModel>
                            {
                                new FileItemModel { FileName = "Forms", Children = new List<FileItemModel> { new FileItemModel { FileName = "index.cshtml" } } }
                            }
                        },
                        new() {
                            FileName = "Language Key",
                            Children = new List<FileItemModel>
                            {
                                new FileItemModel { FileName = "LanguageKey.ts" },
                                new FileItemModel { FileName = "ListLanguageKey" }
                            }
                        }
                    }
                }
            };

            return View(files);
        }


        //[HttpPost]
        //public IActionResult GenCode(GenCodeTsRequest req)
        //{
        //    var res = new GenCodeTsResponse();
        //    (res.ListFileModel, bool isValid, string errorMess) = GenModelFileFolder(req);
        //    if (!isValid)
        //    {
        //        return BadRequest(errorMess);
        //    }
        //    res.ListFileForm = _serivce.GenIndexFileFolder(req);

        //    res.ListFileService = new List<GenCodeTsFileResponse> { _serivce.GenServiceFile(req.IndexModel) };

        //    res.FileViewIndex = new List<GenCodeTsFileResponse> { _serivce.GenIndexViewFile(req) };

        //    return Ok(res);
        //}

    }
}
