using AutoMapper;
using VersionsCRUD.Feature;

namespace VersionsCRUD.Mapping
{
    // Define a mapping profile
    public class MappingFeature : Profile
    {
        public MappingFeature()
        {
            CreateMap<VersionsCRUD.Models.Feature, FeatureGet>();
            // Add more CreateMap lines if you have more complex mappings
        }
    }
}