using AutoMapper;
using DocumentMiddleware.Core.Models;

namespace DocumentMiddleware.Api.Profiles;

public class AntiqueProfile : Profile
{
    public AntiqueProfile()
    {
        CreateMap<Antique, AntiqueDto>();
        CreateMap<AntiqueDto, Antique>();
    }
}