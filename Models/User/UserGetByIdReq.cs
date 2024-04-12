using Models.PagingRequest;

namespace Models.User
{
    public class UserGetByIdReq : Paging
    {
        public Guid Id { get; set; }
        
    }
}
