using CloudNotes.Application.DTO.Response;
using CloudNotes.Application.DTO.Request;
using CloudNotes.Domain.Entities;
using AutoMapper;

namespace CloudNotes.Application.Mappers;

internal class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Note, NoteResponse>().ReverseMap();
        CreateMap<Note, NoteSummaryResponse>().ReverseMap();
        CreateMap<NoteCreateRequest, Note>().ReverseMap();
        CreateMap<NoteUpdateRequest, Note>().ReverseMap();
    }
}
