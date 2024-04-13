using AutoMapper;
using VersionsCRUD.Version;

namespace VersionsCRUD.Mapping
{
    // Define a mapping profile
    public class MappingVersion : Profile
    {
        public MappingVersion()
        {
            CreateMap<VersionsCRUD.Models.Version, VersionGet>();
            // Add more CreateMap lines if you have more complex mappings
        }
    }
}