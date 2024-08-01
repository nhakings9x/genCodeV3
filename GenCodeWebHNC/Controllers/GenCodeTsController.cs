using GenCodeWebHNC.Common;
using GenCodeWebHNC.Models;
using Microsoft.AspNetCore.Mvc;

namespace GenCodeWebHNC.Controllers
{
    public class GenCodeTsController : Controller
    {
        public GenCodeTsController()
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public GenCodeTsResponse GenCode(GenCodeTsRequest req)
        {
            var res = new GenCodeTsResponse();
            res.ListFileModel = GenModelFileFolder(req);
            return res;
        }

        private List<GenCodeTsFileResponse> GenModelFileFolder(GenCodeTsRequest req)
        {
            List<GenCodeTsFileResponse> res = new();
            if (!string.IsNullOrEmpty(req.IndexModel))
            {
                var indexRes = new GenCodeTsFileResponse
                {
                    FileName = req.IndexModel.GetFileName(),
                    Content = req.IndexModel.ToTsModel()
                };

                res.Add(indexRes);
            }

            if (!string.IsNullOrEmpty(req.FormModel))
            {
                var formRes = new GenCodeTsFileResponse
                {
                    FileName = req.FormModel.GetFileName(),
                    Content = req.FormModel.ToTsModel()
                };

                res.Add(formRes);
            }

            if (!string.IsNullOrEmpty(req.OptionModel))
            {
                var OptionRes = new GenCodeTsFileResponse
                {
                    FileName = req.OptionModel.GetFileName(),
                    Content = req.OptionModel.ToTsModel()
                };

                res.Add(OptionRes);
            }

            return res;
        }
    }
}
