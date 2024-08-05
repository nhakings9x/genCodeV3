namespace GenCodeWebHNC.Models
{
    public class GenCodeTsResponse
    {
        public List<GenCodeTsFileResponse> ListFileForm { get; set; }

        public List<GenCodeTsFileResponse> ListFileModel { get; set; }

        public List<GenCodeTsFileResponse> ListFileService { get; set; }

        public List<GenCodeTsFileResponse> FileViewIndex { get; set; }

        public List<GenCodeTsFileResponse> ListFileLanguageKey { get; set; }
    }

    public class GenCodeTsFileResponse
    {
        public GenCodeTsFileResponse() { 
            Id = Guid.NewGuid().ToString();
        }

        public GenCodeTsFileResponse(string fileName, string content)
        {
            Id = Guid.NewGuid().ToString();
            FileName = fileName;
            Content = content;
        }

        public string Id{ get; set; }

        public string FileName { get; set; }

        public string Content { get; set; }
    }
}
