using Models.PagingRequest;

namespace Models.Versions
{
    public class VersionGetByIdReq : Paging
    {
        public Guid Id { get; set; }

    }
}