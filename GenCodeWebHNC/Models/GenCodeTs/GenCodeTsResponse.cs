namespace GenCodeWebHNC.Models
{
    public class GenCodeTsResponse
    {
        public List<GenCodeTsFileResponse> ListFileForm { get; set; }

        public List<GenCodeTsFileResponse> ListFileModel { get; set; }

        public List<GenCodeTsFileResponse> ListFileService { get; set; }

        public List<GenCodeTsFileResponse> FileViewIndex { get; set; }
    }

    public class GenCodeTsFileResponse
    {
        public GenCodeTsFileResponse() { 
            Id = Guid.NewGuid().ToString();
        }
        public string Id{ get; set; }

        public string FileName { get; set; }

        public string Content { get; set; }
    }
}
