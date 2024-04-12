using Models.PagingRequest;

namespace Models.Bug
{
    public class BugGetByIdReq:Paging
    {
        public Guid Id { get; set; }
        
    }


}
