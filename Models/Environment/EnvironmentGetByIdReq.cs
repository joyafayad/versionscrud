using Models.PagingRequest;

namespace Models.Environment
{
    public class EnvironmentGetByIdReq : Paging
    {
        public Guid Id { get; set; }
    }
}
