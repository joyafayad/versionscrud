using AutoMapper;
using VersionsCRUD.User;

namespace VersionsCRUD.Mapping
{
    // Define a mapping profile
    public class MappingUser : Profile
    {
        public MappingUser()
        {
            CreateMap<VersionsCRUD.Models.User, UserGet>();
            // Add more CreateMap lines if you have more complex mappings
        }
    }
}