using System.Text.RegularExpressions;
using System.Text;

namespace GenCodeWebHNC.Extensions
{
    public static class StringExtensions
    {
        public static string GetFileName(this string csharpClass, string fileType = ".ts")
        {
            string className = string.Empty;

            if (csharpClass == null) return className;
            var lines = csharpClass.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            foreach (var line in lines)
            {
                if (line.Trim().StartsWith("public class"))
                {
                    Match classMatch = Regex.Match(line.Trim(), @"public class (\w+)");
                    if (classMatch.Success)
                    {
                        className = classMatch.Groups[1].Value;
                    }
                }
            }
            return string.IsNullOrEmpty(className) ? "" : className + fileType;
        }

        public static string GetFileNameFromTs(this string csharpClass, string fileType = ".ts")
        {
            string className = string.Empty;

            if (csharpClass == null) return className;
            var lines = csharpClass.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            foreach (var line in lines)
            {
                if (line.Trim().StartsWith("export interface"))
                {
                    Match classMatch = Regex.Match(line.Trim(), @"export interface (\w+)");
                    if (classMatch.Success)
                    {
                        className = classMatch.Groups[1].Value;
                    }
                }
            }

            if (className.EndsWith("ViewModel")) className = className.Substring(0, className.Length - "ViewModel".Length);
            if (className.EndsWith("Model")) className = className.Substring(0, className.Length - "Model".Length);

            return string.IsNullOrEmpty(className) ? "" : className + fileType;
        }

        public static string ToTsModel(this string csharpClass)
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

        private static string ConvertCSharpTypeToTypeScript(string csharpType)
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
                case "StringAsLookupItem":
                    return "CodeNameModel";
                default:
                    return csharpType; // For custom or complex types, you might need additional handling
            }
        }

        #region ColumnBuilder
        public static string GenerateColumnTsFromCSharpModel(this string cSharpModel)
        {
            string tsModel = cSharpModel.ToTsModel();
            var columnContent = tsModel.GenerateColumnsCode(true, true);
            return columnContent;
        }

        public static string GenerateColumnsCode(this string tsClassDefinition, bool genModelCSharp = false, bool isGenGridMode = false)
        {
            var regex = new Regex(@"(\w+): (\w+);");
            var matches = regex.Matches(tsClassDefinition);

            var columns = new List<string>();

            foreach (Match match in matches)
            {
                string propertyName = match.Groups[1].Value;
                string propertyType = match.Groups[2].Value;
                string column = $"columns.addColumn('{propertyName}', \"{GetDisplayName(propertyName)}\").width(150)";

                if (isGenGridMode)
                {
                    column = $"columns.addColumn('{propertyName}', {"LanguageKey." + GetFileNameFromTs(tsClassDefinition, "") + ".Table." + propertyName}).width(150)";
                }

                if (propertyType == "number")
                {
                    column += ".format({ type: 'fixedPoint', precision: 2 })";
                }
                else if (propertyType == "Date")
                {
                    column += ".formatDateTime(\"yyyy-MM-dd HH:mm:ss\")";
                }

                columns.Add(column + ";");
            }

            if (genModelCSharp)
            {
                return ".columns(columns => {\n                    \t\t" + string.Join("\n                    \t\t", columns) + "\n                \t})";
            }

            return ".columns(columns => {\n    " + string.Join("\n    ", columns) + "\n})";
        }

        public static string GetDisplayName(this string propertyName)
        {
            // Convert property name to display name (e.g., camel case to words)
            return System.Text.RegularExpressions.Regex.Replace(propertyName, "([A-Z])", " $1").Trim();
        } 
        #endregion

        #region FormBuilder
        public static string GenerateFormBuilderTsFromCSharpModel(this string cSharpModel)
        {
            string tsModel = cSharpModel.ToTsModel();
            var columnContent = tsModel.GenerateCodeFormBuilder();
            return columnContent;
        }

        public static string GenerateCodeFormBuilder(this string tsFormModel)
        {
            // Define regex patterns for extracting properties and their types
            string propertyPattern = @"\s*(\w+):\s*(string|number|boolean|Date);";
            var matches = Regex.Matches(tsFormModel, propertyPattern);

            // Initialize StringBuilder to hold the generated code
            var codeBuilder = new StringBuilder();
            codeBuilder.AppendLine(".items(items => {");

            // Iterate over matches and generate code for each property
            foreach (Match match in matches)
            {
                string propertyName = match.Groups[1].Value;
                string propertyType = match.Groups[2].Value;
                string editorMethod = propertyType switch
                {
                    "string" => "createTextBox",
                    "number" => "createNumberBox",
                    "boolean" => "createCheckBox",
                    "Date" => "createDateBox",
                    _ => "createTextBox"
                };

                string displayName = propertyName switch
                {
                    "FromDate" => "LanguageKey.Common.FromDate",
                    "ToDate" => "LanguageKey.Common.ToDate",
                    _ => $"\"{propertyName.GetDisplayName()}\""
                };

                codeBuilder.AppendLine($"                    \t\titems.addSimpleFor('{propertyName}', {displayName})");
                codeBuilder.AppendLine($"                        \t\t.editor(e => e.{editorMethod}('{propertyName.ToLower()}', this.formData.{propertyName}));");
            }

            codeBuilder.AppendLine("                    \t})");

            return codeBuilder.ToString();
        }

        public static string GeneratePropertyFunctions(this string cSharpModel)
        {
            var tsModel = cSharpModel.ToTsModel();
            // Define regex patterns for extracting properties and their types
            string propertyPattern = @"\s*(\w+):\s*(string|number|boolean|Date);";
            var matches = Regex.Matches(tsModel, propertyPattern);

            // Initialize StringBuilder to hold the generated code
            var codeBuilder = new StringBuilder();

            // Iterate over matches and generate code for each property
            foreach (Match match in matches)
            {
                string propertyName = match.Groups[1].Value;
                string propertyType = match.Groups[2].Value;

                string functionBody = propertyType switch
                {
                    "Date" => $"this.$formBuilder.getDateBoxString('{propertyName}')",
                    _ => $"this.$formBuilder.getData().{propertyName}"
                };

                codeBuilder.AppendLine($"                        {propertyName}: () => {{ return {functionBody} }},");
            }

            // Remove the last comma
            if (codeBuilder.Length > 0)
                codeBuilder.Length -= 3; // Removing the last comma and newline characters

            return codeBuilder.ToString();
        }
        #endregion

        #region View Index extension
        public static string GenerateFormDataCode(this string cSharpModel)
        {
            var tsModel = cSharpModel.ToTsModel();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("\n\t\t{");

            // Trích xuất các thuộc tính
            var matches = Regex.Matches(tsModel, @"\s+(\w+):\s+[\w\[\]]+;");

            foreach (Match match in matches)
            {
                string propertyName = match.Groups[1].Value;
                sb.AppendLine($"\t\t\t{propertyName}: ...,");
            }

            // Xóa dấu phẩy cuối cùng
            if (matches.Count > 0)
            {
                sb.Length -= 3;  // Xóa dấu phẩy và dòng mới cuối cùng
                sb.AppendLine();
            }

            sb.AppendLine("\t\t}");

            return sb.ToString();
        }
        #endregion

        #region KeyLanguage
        public static string GenerateLanguageKeyCode(this string model)
        {
            // Sử dụng regex để lấy tên interface
            var interfaceNameMatch = Regex.Match(model, @"interface\s+(\w+)\s*{");
            if (!interfaceNameMatch.Success)
            {
                throw new Exception("Interface name not found in the model.");
            }

            string interfaceName = interfaceNameMatch.Groups[1].Value;
            if (interfaceName.EndsWith("ViewModel")) interfaceName = interfaceName.Substring(0, interfaceName.Length - "ViewModel".Length);
            if (interfaceName.EndsWith("Model")) interfaceName = interfaceName.Substring(0, interfaceName.Length - "Model".Length);

            var sb = new StringBuilder();
            sb.AppendLine("namespace My {");
            sb.AppendLine("    export namespace LanguageKey {");
            sb.AppendLine($"        export const {interfaceName} = {{");
            sb.AppendLine($"             Table: {{");
            // Sử dụng regex để lấy các key trong interface
            var keyMatches = Regex.Matches(model, @"\s*(\w+)\s*:\s*\w+;");
            foreach (Match match in keyMatches)
            {
                string key = match.Groups[1].Value;
                sb.AppendLine($"                 {key}: \"@ProjectName.{interfaceName}.Table.{key}\",");
            }

            sb.AppendLine("            },");
            sb.AppendLine("        };");
            sb.AppendLine("    }");
            sb.AppendLine("}");

            return sb.ToString();
        }

        public static string GenerateValues(this string model)
        {
            var sb = new StringBuilder();

            // Sử dụng regex để lấy tên interface
            var interfaceNameMatch = Regex.Match(model, @"interface\s+(\w+)\s*{");
            if (!interfaceNameMatch.Success)
            {
                throw new Exception("Interface name not found in the model.");
            }

            string interfaceName = interfaceNameMatch.Groups[1].Value;
            if (interfaceName.EndsWith("ViewModel")) interfaceName = interfaceName.Substring(0, interfaceName.Length - "ViewModel".Length);
            if (interfaceName.EndsWith("Model")) interfaceName = interfaceName.Substring(0, interfaceName.Length - "Model".Length);
            // Sử dụng regex để lấy các key trong interface
            var keyMatches = Regex.Matches(model, @"\s*(\w+)\s*:\s*\w+;");
            foreach (Match match in keyMatches)
            {
                string key = match.Groups[1].Value;
                sb.AppendLine($"@ProjectName.{interfaceName}.Table.{key}");
            }

            return sb.ToString();
        }
        #endregion
    }
}
