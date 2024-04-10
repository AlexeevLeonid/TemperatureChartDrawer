namespace TempArAn.Domain.Responses
{
    public record class SourcesSetResponse
    {
        public List<SourceResponse> userList { get; set; }
        public List<SourceResponse> publicList { get; set; }
        public SourcesSetResponse(List<SourceResponse> userList, List<SourceResponse> publicList)
        {
            this.userList = userList;
            this.publicList = publicList;
        }
    }
}
