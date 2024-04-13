using AutoMapper;
using VersionsCRUD.Bug;

namespace VersionsCRUD.Mapping
{
    // Define a mapping profile
    public class MappingBug : Profile
    {
        public MappingBug()
        {
            CreateMap<VersionsCRUD.Models.Bug, BugGet>();
            // Add more CreateMap lines if you have more complex mappings
        }
    }
}