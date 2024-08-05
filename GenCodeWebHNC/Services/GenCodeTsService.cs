using GenCodeWebHNC.Common;
using GenCodeWebHNC.Models;

namespace GenCodeWebHNC.Services
{
    public class GenCodeTsService
    {
        public GenCodeTsService() { }

        #region Forms
        public List<GenCodeTsFileResponse> GenIndexFileFolder(GenCodeTsRequest req)
        {
            List<GenCodeTsFileResponse> res = new();

            var indexContent = GenCodeTsConstans.BASE_INDEX_CONTENT;
            (indexContent, string fileName) = GenCodeByIndexModel(req.IndexModel, indexContent);
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

            indexContent = indexContent.Replace("@ColumnGridContent", indexModel.GenerateColumnTsFromCSharpModel());

            indexContent = indexContent.Replace("@IndexModel", fileName);

            if (fileName.EndsWith("ViewModel")) fileName = fileName.Substring(0, fileName.Length - "ViewModel".Length);
            if (fileName.EndsWith("Model")) fileName = fileName.Substring(0, fileName.Length - "Model".Length);
            indexContent = indexContent.Replace("@IndexFileExcelName", (fileName).GetDisplayName());
            indexContent = indexContent.Replace("@IndexFileName", fileName + "Index");
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
                indexContent = indexContent.Replace("@FormModelContructor", formModelNameContructor);
                indexContent = indexContent.Replace("@FormModel", formModelName);
                indexContent = indexContent.Replace("@LoadParams", "null");
                indexContent = indexContent.Replace("@LoadparamsFunc", "");
                indexContent = indexContent.Replace("@FormSearchContent", "");
                return indexContent;
            }

            formModelName = formModel.GetFileName("");
            if (string.IsNullOrEmpty(formModelName))
            {
                formModelName = "null";
                formModelNameContructor = "any";
                indexContent = indexContent.Replace("@FormModelContructor", formModelNameContructor);
                indexContent = indexContent.Replace("@FormModel", formModelName);
                indexContent = indexContent.Replace("@LoadParams", "null");
                indexContent = indexContent.Replace("@LoadparamsFunc", "");
                indexContent = indexContent.Replace("@FormSearchContent", "");

                return indexContent;
            }
            indexContent = indexContent.Replace("@LoadParams", "this.getLoadParams()");
            indexContent = indexContent.Replace("@FormModelContructor", formModelName);
            indexContent = indexContent.Replace("@FormModel", formModelName);

            indexContent = indexContent.Replace("@FormSearchContent", GenCodeTsConstans.BASE_FORM_DATA_CONTENT);
            indexContent = indexContent.Replace("@FormSearchItems", formModel.GenerateFormBuilderTsFromCSharpModel());

            indexContent = indexContent.Replace("@LoadparamsFunc", GenCodeTsConstans.LOADPARAMS_FUNC);
            indexContent = indexContent.Replace("@LoadParamReturn", formModel.GeneratePropertyFunctions());

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
                indexContent = indexContent.Replace("@OptionModelContructor", optionModelNameContructor);
                indexContent = indexContent.Replace("@OptionModel", optionFileName);
                return indexContent;
            }

            optionFileName = optionModel.GetFileName("");
            if (string.IsNullOrEmpty(optionFileName))
            {
                optionFileName = "null";
                optionModelNameContructor = "any";
                indexContent = indexContent.Replace("@OptionModelContructor", optionModelNameContructor);
                indexContent = indexContent.Replace("@OptionModel", optionFileName);
                return indexContent;
            }

            indexContent = indexContent.Replace("@OptionModelContructor", optionFileName);
            indexContent = indexContent.Replace("@OptionModel", optionFileName);

            return indexContent;
        }
        #endregion

        #region Service
        public GenCodeTsFileResponse GenServiceFile(string indexModel)
        {

            var fileName = indexModel.GetFileName("");
            if (fileName.EndsWith("ViewModel")) fileName = fileName.Substring(0, fileName.Length - "ViewModel".Length);
            if (fileName.EndsWith("Model")) fileName = fileName.Substring(0, fileName.Length - "Model".Length);

            var serviceContent = GenCodeTsConstans.BASE_SERVICE_CONTENT;
            serviceContent = serviceContent.Replace("@IndexFileName", fileName);
            var  res = new GenCodeTsFileResponse{ 
                FileName = fileName + "Service",
                Content = serviceContent,
            };

            return res;
        }
        #endregion

        #region Index View
        public GenCodeTsFileResponse GenIndexViewFile(GenCodeTsRequest req)
        {
            var fileName = req.IndexModel.GetFileName("");
            if (fileName.EndsWith("ViewModel")) fileName = fileName.Substring(0, fileName.Length - "ViewModel".Length);
            if (fileName.EndsWith("Model")) fileName = fileName.Substring(0, fileName.Length - "Model".Length);

            var indexViewContent = GenCodeTsConstans.BASE_VIEW_CONTENT;
            indexViewContent = indexViewContent.Replace("@IndexFileName", fileName);

            if (!string.IsNullOrEmpty(req.FormModel))
            {
                var content = req.FormModel.GenerateFormDataCode();
                indexViewContent = indexViewContent.Replace("@FormModel", content.Substring(0, content.Length - 2));
            }
            else
            {
                indexViewContent = indexViewContent.Replace("@FormModel", "null");
            }

            if (!string.IsNullOrEmpty(req.OptionModel))
            {
                var content = req.OptionModel.GenerateFormDataCode();
                indexViewContent = indexViewContent.Replace("@OptionModel", content.Substring(0, content.Length - 2));
            }
            else
            {
                indexViewContent = indexViewContent.Replace("@OptionModel", "null");
            }

            var res = new GenCodeTsFileResponse
            {
                FileName = "index.cshtml",
                Content = indexViewContent,
            };
            return res;
        }
        #endregion
    }
}
