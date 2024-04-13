using AutoMapper;
using VersionsCRUD.Environment;

namespace VersionsCRUD.Mapping
{
    // Define a mapping profile
    public class MappingEnvironment : Profile
    {
        public MappingEnvironment()
        {
            CreateMap<VersionsCRUD.Models.Environment, EnvironmentGet>();
            // Add more CreateMap lines if you have more complex mappings
        }
    }
}