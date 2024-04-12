using Models.PagingRequest;

namespace Models.Feature
{
    public class FeatureGetByIdReq : Paging
    {
        public Guid Id { get; set; }
       
    }
}
