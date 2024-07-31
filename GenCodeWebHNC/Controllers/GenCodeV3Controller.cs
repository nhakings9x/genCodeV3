using GenCodeWebHNC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace GenCodeWebHNC.Controllers
{
    public class GenCodeV3Controller : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://api.mymemory.translated.net";

        public GenCodeV3Controller()
        {
            _httpClient = new HttpClient();
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<string> GenCode(GenCodeV3RequestModel req)
        {
            var msg = "Vui lòng kiểm tra lại nội dung hoặc kiểu gen code!";
            string content = "";

            if(req.Type == GenCodeType.CSharpToTS)
            {
                try
                {
                    content = ConvertToTypeScriptInterface(req.CodeContent);
                    content = content?.Trim() != "" ? content : msg;
                    return content;
                }
                catch (Exception)
                {
                    throw;
                }
            }

            if (req.Type == GenCodeType.CSharpToListString)
            {
                try
                {
                    content = ExtractConstStrings(req.CodeContent);
                    content = content?.Trim() != "" ? content : msg;
                    return content;
                }
                catch (Exception)
                {

                    throw;
                }
            }

            if (req.Type == GenCodeType.Translate)
            {
                try
                {
                    content = await TranslateTextAsync(req.CodeContent);
                    content = content?.Trim() != "" ? content : msg;
                    return content;
                }
                catch (Exception)
                {

                    throw;
                }
            }


            if (req.Type == GenCodeType.BeautifyText)
            {
                try
                {
                    content = BeautifyText(req.CodeContent);
                    content = content?.Trim() != "" ? content : msg;
                    return content;
                }
                catch (Exception)
                {

                    throw;
                }
            }

            if(req.Type == GenCodeType.ModelLanguageCSharpToTs)
            {
                content = ConvertCSharpToTypeScript(req.CodeContent);
                content = content?.Trim() != "" ? content : msg;
                return content;
            }

            if (req.Type == GenCodeType.ModelTsToClumnGrid)
            {
                content = GenerateColumnsCode(req.CodeContent);
                content = content?.Trim() != "" ? content : msg;
                return content;
            }

            return "Đã có lỗi xảy ra";
        }

        #region GenCodeType.ModelTsToClumnGrid
        private string GenerateColumnsCode(string tsClassDefinition)
        {
            var regex = new Regex(@"(\w+): (\w+);");
            var matches = regex.Matches(tsClassDefinition);

            var columns = new List<string>();

            foreach (Match match in matches)
            {
                string propertyName = match.Groups[1].Value;
                string propertyType = match.Groups[2].Value;
                string column = $"columns.addColumn('{propertyName}', \"{GetDisplayName(propertyName)}\").width(150)";

                if (propertyType == "number")
                {
                    column += ".format({ type: 'fixedPoint', precision: 2 })";
                }
                else if (propertyType == "Date")
                {
                    column += ".formatDateTime(\"yyyy-MM-dd\")";
                }

                columns.Add(column + ";");
            }

            return ".columns(columns => {\n    " + string.Join("\n    ", columns) + "\n})";
        }

        private string GetDisplayName(string propertyName)
        {
            // Convert property name to display name (e.g., camel case to words)
            return System.Text.RegularExpressions.Regex.Replace(propertyName, "([A-Z])", " $1").Trim();
        }
        #endregion

        #region GenCodeType.ModelLanguageCSharpToTs
        public static string ConvertCSharpToTypeScript(string csharpCode)
        {
            // Loại bỏ các khoảng trắng và dòng trống không cần thiết
            csharpCode = csharpCode.Trim();

            // Tìm tên namespace
            Match namespaceMatch = Regex.Match(csharpCode, @"namespace\s+(\S+)");
            if (!namespaceMatch.Success || namespaceMatch.Groups.Count < 2)
            {
                throw new ArgumentException("Invalid namespace declaration");
            }
            string namespaceName = namespaceMatch.Groups[1].Value;

            StringBuilder tsCodeBuilder = new StringBuilder();
            tsCodeBuilder.AppendLine($"namespace My {{");
            tsCodeBuilder.AppendLine($"    export namespace LanguageKey {{");

            // Tìm các lớp trong LanguageKey và tạo export const tương ứng
            MatchCollection classMatches = Regex.Matches(csharpCode, @"public class\s+(\S+)\s*{[^}]*}");
            int classCount = classMatches.Count;
            int processedClasses = 0;
            foreach (Match classMatch in classMatches)
            {
                string className = classMatch.Groups[1].Value;
                tsCodeBuilder.AppendLine($"        export const {className} = {{");

                // Tìm các const string trong lớp và thêm chúng vào export const tương ứng
                MatchCollection constantMatches = Regex.Matches(classMatch.Value, @"public const string\s+(\S+)\s*=\s*""([^""]+)"";");
                foreach (Match constantMatch in constantMatches)
                {
                    string constantName = constantMatch.Groups[1].Value;
                    string constantValue = constantMatch.Groups[2].Value;
                    tsCodeBuilder.AppendLine($"            {constantName}: '{constantValue}',");
                }

                tsCodeBuilder.AppendLine($"        }};");

                // Thêm một dòng trống giữa các export const, trừ khối cuối cùng
                processedClasses++;
                if (processedClasses < classCount)
                {
                    tsCodeBuilder.AppendLine();
                }
            }

            tsCodeBuilder.AppendLine($"    }}");
            tsCodeBuilder.AppendLine($"}}");

            return tsCodeBuilder.ToString();
        }

        #endregion

        #region GenCodeType.Translate

        public async Task<string> TranslateTextAsync(string text, string sourceLang = "vi", string targetLang = "en")
        {
            text = BeautifyText(text);
            var url = $"{_baseUrl}/get?q={Uri.EscapeDataString(text)}&langpair={sourceLang}|{targetLang}";

            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                // Xử lý phản hồi JSON và trả về văn bản dịch
                var jsonResponse = JObject.Parse(responseContent);
                var translatedText = jsonResponse["responseData"]["translatedText"].ToString();
                translatedText = Regex.Replace(translatedText, @"(\n)+", "\n");
                return translatedText;
            }
            else
            {
                // Xử lý trường hợp lỗi
                return $"Error: {response.StatusCode}";
            }
        }

        private static string BeautifyText(string input)
        {
            // Remove all occurrences of "\t"
            string cleanedInput = input.Trim().Replace("\t", "");

            // Replace consecutive "\n" with a single "\n"
            string result = Regex.Replace(cleanedInput, @"(\n)+", "\n");

            return result;
        }

        #endregion

        #region GenCodeType.CSharpToListString
        public string ExtractConstStrings(string code)
        {
            // Sử dụng regex để tìm tất cả các chuỗi trong các hằng số
            string pattern = @"public const string \w+ = ""(.*?)"";";
            MatchCollection matches = Regex.Matches(code, pattern);

            // Kết hợp các chuỗi tìm được thành một chuỗi duy nhất, mỗi chuỗi trên một dòng
            string result = string.Join("\n",
                System.Linq.Enumerable.Select(matches.Cast<Match>(), m => m.Groups[1].Value));

            return result;
        } 
        #endregion

        #region GenCodeType.CSharpToTS

        [HttpPost]
        public string ConvertToTypeScriptInterface(string csharpClass)
        {
            var lines = csharpClass.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            var sb = new StringBuilder();

            string className = null;
            bool isInClass = false;

            foreach (var line in lines)
            {
                if (line.Trim().StartsWith("public class"))
                {
                    isInClass = true;
                    var classMatch = Regex.Match(line.Trim(), @"public class (\w+)");
                    if (classMatch.Success)
                    {
                        className = classMatch.Groups[1].Value;
                        sb.AppendLine($"namespace My {{");
                        sb.AppendLine($"    export interface {className} {{");
                    }
                }
                else if (isInClass && line.Trim().StartsWith("public"))
                {
                    var propertyMatch = Regex.Match(line.Trim(), @"public (\w+<\w+>|\w+)(\?)?\s+(\w+)\s*(\{.*)?");

                    if (propertyMatch.Success)
                    {
                        var csharpType = propertyMatch.Groups[1].Value;
                        var hasQuestionMark = propertyMatch.Groups[2].Success;
                        var name = propertyMatch.Groups[3].Value;
                        var tsType = ConvertCSharpTypeToTypeScript(csharpType);

                        // Xử lý trường hợp kiểu dữ liệu kết thúc bằng dấu ?
                        if (hasQuestionMark)
                        {
                            tsType += " | null";
                        }

                        sb.AppendLine($"        {name}: {tsType};");
                    }
                }
                else if (isInClass && line.Trim() == "}")
                {
                    isInClass = false;
                    sb.AppendLine("    }");
                    sb.AppendLine("}");
                }
            }

            return sb.ToString();
        }

        private string ConvertCSharpTypeToTypeScript(string csharpType)
        {
            if (csharpType.StartsWith("List<"))
            {
                var genericType = csharpType.Substring(5, csharpType.Length - 6); // Remove "List<" and ">"
                return $"{ConvertCSharpTypeToTypeScript(genericType)}[]";
            }

            switch (csharpType)
            {
                case "string":
                    return "string";
                case "int":
                case "long":
                case "float":
                case "double":
                case "decimal":
                    return "number";
                case "bool":
                    return "boolean";
                case "DateTime":
                    return "Date";
                default:
                    return csharpType; // For custom or complex types, you might need additional handling
            }
        } 
        #endregion

    }
}
