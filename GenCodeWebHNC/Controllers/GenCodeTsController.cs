using GenCodeWebHNC.Common;
using GenCodeWebHNC.Models;
using GenCodeWebHNC.Services;
using Microsoft.AspNetCore.Mvc;

namespace GenCodeWebHNC.Controllers
{
    public class GenCodeTsController : Controller
    {
        private readonly GenCodeTsService _serivce;

        public GenCodeTsController()
        {
            _serivce = new GenCodeTsService();
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GenCode(GenCodeTsRequest req)
        {
            var res = new GenCodeTsResponse();
            (res.ListFileModel, bool isValid, string errorMess) = GenModelFileFolder(req);
            if (!isValid)
            {
                return BadRequest(errorMess);
            }
            res.ListFileForm = _serivce.GenIndexFileFolder(req);

            res.ListFileService = new List<GenCodeTsFileResponse> { _serivce.GenServiceFile(req.IndexModel) };

            res.FileViewIndex = new List<GenCodeTsFileResponse> { _serivce.GenIndexViewFile(req) };

            res.ListFileLanguageKey = new List<GenCodeTsFileResponse>
            {
                new ("LanguageKey.ts", req.IndexModel.ToTsModel().GenerateLanguageKeyCode()),
                new ("ListLanguageKey", req.IndexModel.ToTsModel().GenerateValues()),
            };

            return Ok(res);
        }

        private (List<GenCodeTsFileResponse>, bool, string) GenModelFileFolder(GenCodeTsRequest req)
        {
            List<GenCodeTsFileResponse> res = new();
            if (!string.IsNullOrEmpty(req.IndexModel))
            {
                var indexRes = new GenCodeTsFileResponse
                {
                    FileName = req.IndexModel.GetFileName(),
                    Content = req.IndexModel.ToTsModel()
                };

                if(string.IsNullOrEmpty(indexRes.FileName) || string.IsNullOrEmpty(indexRes.Content))
                {
                    return (new List<GenCodeTsFileResponse>(), false, "Kiểm tra lại Model Index");
                }

                res.Add(indexRes);
            }

            if (!string.IsNullOrEmpty(req.FormModel))
            {
                var formRes = new GenCodeTsFileResponse
                {
                    FileName = req.FormModel.GetFileName(),
                    Content = req.FormModel.ToTsModel()
                };

                if (string.IsNullOrEmpty(formRes.FileName) || string.IsNullOrEmpty(formRes.Content))
                {
                    return (new List<GenCodeTsFileResponse>(), false, "Kiểm tra lại Model Form");
                }

                res.Add(formRes);
            }

            if (!string.IsNullOrEmpty(req.OptionModel))
            {
                var OptionRes = new GenCodeTsFileResponse
                {
                    FileName = req.OptionModel.GetFileName(),
                    Content = req.OptionModel.ToTsModel()
                };

                if (string.IsNullOrEmpty(OptionRes.FileName) || string.IsNullOrEmpty(OptionRes.Content))
                {
                    return (new List<GenCodeTsFileResponse>(), false, "Kiểm tra lại Model Option");
                }

                res.Add(OptionRes);
            }

            return (res, true , "");
        }
    }
}
