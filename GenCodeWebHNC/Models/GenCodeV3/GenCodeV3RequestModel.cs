namespace GenCodeWebHNC.Models
{
    public class GenCodeV3RequestModel
    {
        public string CodeContent { get; set; }

        public GenCodeType Type { get; set; }
    }

    public enum GenCodeType
    {
        /// <summary>
        /// Model C# sang TS
        /// </summary>
        CSharpToTS = 0,

        /// <summary>
        /// Model Keylanguage sang chuỗi string dán vào excel
        /// </summary>
        CSharpToListString = 1,

        /// <summary>
        /// GG dịch
        /// </summary>
        Translate = 2,

        /// <summary>
        /// Beauty text
        /// </summary>
        BeautifyText = 3,

        /// <summary>
        /// Model language C# sang TS
        /// </summary>
        ModelLanguageCSharpToTs = 4,

        /// <summary>
        /// ModelTsToClumnGrid
        /// </summary>
        ModelTsToClumnGrid = 5,
    }
}
