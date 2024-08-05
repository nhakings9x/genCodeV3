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
            return View();
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
