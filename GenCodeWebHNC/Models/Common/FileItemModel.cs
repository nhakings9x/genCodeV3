namespace GenCodeWebHNC.Models
{
    public class FileItemModel
    {
        public string Id { get; set; }

        public string FileName { get; set; }

        public string Content { get; set; }

        public List<FileItemModel> Children { get; set; } = new List<FileItemModel>();
    }
}
