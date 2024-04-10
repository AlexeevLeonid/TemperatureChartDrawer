using TempArAn.Domain.AbstractCore;

namespace TempArAn.Domain.Models.Source
{
    public record class SourcesSet
    {
        public List<SourceBase> userList { get; set; }
        public List<SourceBase> publicList { get; set; }
        public SourcesSet(List<SourceBase> userList, List<SourceBase> publicList)
        {
            this.userList = userList;
            this.publicList = publicList;
        }
    }
}
