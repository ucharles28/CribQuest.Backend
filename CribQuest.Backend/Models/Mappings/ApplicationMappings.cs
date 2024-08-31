using CribQuest.Backend.DTOs.Requests;

namespace CribQuest.Backend.Models.Mappings;

public class ApplicationMappings : Profile
{
    public ApplicationMappings()
    {
        CreateMap<SignUpPayload, User>();
    }
}