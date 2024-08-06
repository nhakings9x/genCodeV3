using GenCodeWebHNC.Common;
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
                    FileName = "Typescript",
                    Children = new List<FileItemModel>
                    {
                        new() {
                            FileName = "Popup",
                            Children = new List<FileItemModel>
                            {
                                new() {
                                    FileName = "Popup.ts" ,
                                    Content = FileMauConstants.POPUP_CONTENT
                                },
                                new() { 
                                    FileName = "PopupGrid.ts" ,
                                    Content = FileMauConstants.POPUP_GRID_CONTENT
                                },
                                new() {
                                    FileName = "PopupTabPenal.ts" ,
                                    Content = FileMauConstants.POPUP_TAB_PENAL
                                }
                            }
                        },
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
