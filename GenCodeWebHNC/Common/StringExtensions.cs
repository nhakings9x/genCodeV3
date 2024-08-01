using System.Text.RegularExpressions;
using System.Text;

namespace GenCodeWebHNC.Common
{
    public static class StringExtensions
    {
        public static string GetFileName(this string csharpClass)
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
            return string.IsNullOrEmpty(className) ? "" : className + ".ts";
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
                default:
                    return csharpType; // For custom or complex types, you might need additional handling
            }
        }
    }
}
