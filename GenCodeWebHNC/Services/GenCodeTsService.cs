using GenCodeWebHNC.Common;
using GenCodeWebHNC.Models;

namespace GenCodeWebHNC.Services
{
    public class GenCodeTsService
    {
        public GenCodeTsService() { }

        public List<GenCodeTsFileResponse> GenIndexFileFolder(GenCodeTsRequest req)
        {
            List<GenCodeTsFileResponse> res = new();

            var indexContent = GenCodeTsConstan.BASE_INDEX_CONTENT;
            (indexContent, string fileName )= GenCodeByIndexModel(req.IndexModel, indexContent);
            indexContent = GenCodeByFormModel(req.FormModel, indexContent);
            indexContent = GenCodeByOptionModel(req.OptionModel, indexContent);
            var indexFile = new GenCodeTsFileResponse
            {
                FileName = fileName + ".ts",
                Content = indexContent
            };

            res.Add(indexFile);
            return res;
        }

        private (string, string) GenCodeByIndexModel(string indexModel, string indexContent)
        {
            var fileName = indexModel.GetFileName("");

            indexContent.MyReplace("@IndexModel", fileName);

            if (fileName.EndsWith("Model")) fileName = fileName.Substring(0, fileName.Length - "Model".Length);
            indexContent.MyReplace("@IndexFileName", fileName + "Index");
            return (indexContent, fileName + "Index");
        }

        private string GenCodeByFormModel(string formModel, string indexContent)
        {
            string formModelName = "";
            string formModelNameContructor = "";
            if (string.IsNullOrEmpty(formModel))
            {
                formModelName = "null";
                formModelNameContructor = "any";
                indexContent.MyReplace("@FormModel", formModelName);
                indexContent.MyReplace("@FormModelContructor", formModelNameContructor);
                return indexContent;
            }

            formModelName = formModel.GetFileName("");
            if (string.IsNullOrEmpty(formModelName))
            {
                formModelName = "null";
                formModelNameContructor = "any";
                indexContent.MyReplace("@FormModel", formModelName);
                indexContent.MyReplace("@FormModelContructor", formModelNameContructor);
                return indexContent;
            }

            indexContent.MyReplace("@IndexModel", formModelName);
            indexContent.MyReplace("@FormModelContructor", formModelName);

            return indexContent;
        }

        private string GenCodeByOptionModel(string optionModel, string indexContent)
        {
            string optionFileName = "";
            string optionModelNameContructor = "";

            if (string.IsNullOrEmpty(optionModel))
            {
                optionFileName = "null";
                optionModelNameContructor = "any";
                indexContent.MyReplace("@OptionModel", optionFileName);
                indexContent.MyReplace("@OptionModelContructor", optionModelNameContructor);
                return indexContent;
            }

            optionFileName = optionModel.GetFileName("");
            if (string.IsNullOrEmpty(optionFileName))
            {
                optionFileName = "null";
                optionModelNameContructor = "any";
                indexContent.MyReplace("@OptionModel", optionFileName);
                indexContent.MyReplace("@OptionModelContructor", optionModelNameContructor);
                return indexContent;
            }

            indexContent.MyReplace("@IndexModel", optionFileName);
            indexContent.MyReplace("@FormModelContructor", optionFileName);

            return indexContent;
        }
    }
}
