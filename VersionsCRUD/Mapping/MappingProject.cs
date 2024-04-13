using AutoMapper;
using VersionsCRUD.Project;

namespace VersionsCRUD.Mapping
{
    // Define a mapping profile
    public class MappingProject : Profile
    {
        public MappingProject()
        {
            CreateMap<VersionsCRUD.Models.Project, ProjectGet>();
            // Add more CreateMap lines if you have more complex mappings
        }
    }
}