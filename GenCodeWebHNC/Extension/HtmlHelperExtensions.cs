using GenCodeWebHNC.Models;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;

namespace GenCodeWebHNC.Extension
{
    public static class HtmlHelperExtensions
    {
        public static IHtmlContent RenderTree(this IHtmlHelper htmlHelper, List<FileItemModel> files)
        {
            var html = new StringBuilder();
            html.Append("<ul class=\"tree\">");
            foreach (var file in files)
            {
                html.Append(RenderTreeNode(file));
            }
            html.Append("</ul>");
            return new HtmlString(html.ToString());
        }

        private static string RenderTreeNode(FileItemModel file)
        {
            var html = new StringBuilder();
            if (file.Children != null && file.Children.Count > 0)
            {
                html.Append($"<li><span class=\"folder\" data-id=\"{file.Id}\" data-content=\"{file.Content}\">{file.FileName}</span>");
                html.Append("<ul>");
                foreach (var child in file.Children)
                {
                    html.Append(RenderTreeNode(child));
                }
                html.Append("</ul>");
                html.Append("</li>");
            }
            else
            {
                html.Append($"<li><span class=\"file\" data-id=\"{file.Id}\" data-content=\"{file.Content}\">{file.FileName}</span></li>");
            }
            return html.ToString();
        }
    }
}
