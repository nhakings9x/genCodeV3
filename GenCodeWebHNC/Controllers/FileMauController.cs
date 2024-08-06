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
                                    Content = FileMauConstants.Popup.POPUP_CONTENT
                                },
                                new() {
                                    FileName = "PopupGrid.ts" ,
                                    Content = FileMauConstants.Popup.POPUP_GRID_CONTENT
                                },
                                new() {
                                    FileName = "PopupTabPenal.ts" ,
                                    Content = FileMauConstants.Popup.POPUP_TAB_PENAL
                                }
                            }
                        },
                    }
                },
                new()
                {
                    FileName = "C#",
                    Children = new List<FileItemModel>
                    {
                        new() {
                                    FileName = "Controller.cs" ,
                                    Content = FileMauConstants.CSharp.CONTROLLER_CONTENT
                                },
                        new() {
                                    FileName = "Service.cs" ,
                                    Content = FileMauConstants.CSharp.SERVICE_CONTENT
                                },
                    }
                }
            };

            return View(files);
        }
    }
}
