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

            indexContent = indexContent.Replace("@ColumnGridContent", indexModel.GenerateColumnTsFromCSharpModel());

            indexContent = indexContent.Replace("@IndexModel", fileName);

            if (fileName.EndsWith("ViewModel")) fileName = fileName.Substring(0, fileName.Length - "ViewModel".Length);
            if (fileName.EndsWith("Model")) fileName = fileName.Substring(0, fileName.Length - "Model".Length);
            indexContent = indexContent.Replace("@IndexFileExcelName", (fileName + "Index").GetDisplayName());
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

            indexContent = indexContent.Replace("@FormSearchContent", GenCodeTsConstan.BASE_FORM_DATA_CONTENT);
            indexContent = indexContent.Replace("@FormSearchItems", formModel.GenerateFormBuilderTsFromCSharpModel());

            indexContent = indexContent.Replace("@LoadparamsFunc", GenCodeTsConstan.LOADPARAMS_FUNC);
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
    }
}
