namespace ToksApi.Models.UtilityClasses
{
    public class IssueResultWithEnumerable
    {
        public uint? Id { get; set; }
        public string? Target { get; set; }
        public string Message { get; set; }
        public bool Result { get; set; }
        public IEnumerable<IssueModel>? issueModels { get; set; }    

    }
}
