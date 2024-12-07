using System.Security.Principal;
using Microsoft.Extensions.Logging;
using CloudNotes.Domain.Entities;
using CloudNotes.Domain.Exceptions;
using CloudNotes.Domain.Extensions;
using CloudNotes.Domain.Models;
using CloudNotes.Domain.Interfaces.Events;
using CloudNotes.Domain.Interfaces.Repositories;
using CloudNotes.Application.DTO.ApiResult;
using CloudNotes.Application.DTO.Request;
using CloudNotes.Application.DTO.Response;
using CloudNotes.Application.Services.Interfaces;
using AutoMapper;


namespace CloudNotes.Application.Services.Implementations;

internal class NoteService : INoteService
{
    private readonly ILogger<NoteService> _logger;
    private readonly IEventPublisher _eventPublisher;
    private readonly IMapper _mapper;
    private readonly INoteRepository _repository;

    public NoteService(ILogger<NoteService> logger, IEventPublisher eventPublisher, IMapper mapper, INoteRepository repository)
    {
        _logger = logger;
        _eventPublisher = eventPublisher;
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<Result<IEnumerable<NoteSummaryResponse>>> GetNoteListAsync(string username)
    {
        var notes = await _repository.GetAllAsync();
        var response = _mapper.Map<IEnumerable<NoteSummaryResponse>>(notes);
        return Result<IEnumerable<NoteSummaryResponse>>.Success(response);
    }

    public async Task<Result<NoteResponse>> GetNoteAsync(string username, Guid noteId)
    {
        var note = await _repository.GetByIdAsync(noteId).OrElseThrowsAsync(new NoteNotFoundException(noteId));
        var response = _mapper.Map<NoteResponse>(note);
        return Result<NoteResponse>.Success(response);
    }    

    public async Task<Result<NoteResponse>> SaveNoteAsync(NoteCreateRequest request)
    {        
        var note = _mapper.Map<Note>(request);
        var newNote = await _repository.InsertAsync(note);

        _logger.LogInformation($"[SaveNote] Publishing an event to NoteID: '{newNote.Id.Value}'");

        await _eventPublisher.PublishEventAsync(new Event
        {
            EventType = Domain.Enums.EventType.NoteCreated,
            NoteId = newNote.Id.Value,
            TimeStamp = DateTime.UtcNow,
            Username = WindowsIdentity.GetCurrent().Name
        });

        var response = _mapper.Map<NoteResponse>(newNote);
        return Result<NoteResponse>.Success(response);
    }

    public async Task<Result<NoteResponse>> UpdateNoteAsync(NoteUpdateRequest request)
    {
        var currentNote = await _repository.GetByIdAsync(request.Id.Value).OrElseThrowsAsync(new NoteNotFoundException(request.Id.Value));
        currentNote.Title = request.Title;
        currentNote.Content = request.Content;
        currentNote.UpdatedAt = DateTime.UtcNow;

        var updatedNote = await _repository.UpdateAsync(currentNote);

        _logger.LogInformation($"[UpdateNote] Publishing an event to NoteID: '{updatedNote.Id.Value}'");

        await _eventPublisher.PublishEventAsync(new Event
        {
            EventType = Domain.Enums.EventType.NoteEdited,
            NoteId = updatedNote.Id.Value,
            TimeStamp = DateTime.UtcNow,
            Username = WindowsIdentity.GetCurrent().Name
        });

        var response = _mapper.Map<NoteResponse>(updatedNote);
        return Result<NoteResponse>.Success(response);
    }

    public async Task DeleteNoteAsync(string username, Guid noteId)
    {
        var note = await _repository.GetByIdAsync(noteId).OrElseThrowsAsync(new NoteNotFoundException(noteId));
        
        await _repository.DeleteAsync(note);

        _logger.LogInformation($"[DeleteeNote] Publishing an event to NoteID: '{noteId}'");

        await _eventPublisher.PublishEventAsync(new Event
        {
            EventType = Domain.Enums.EventType.NoteDeleted,
            NoteId = noteId,
            TimeStamp = DateTime.UtcNow,
            Username = WindowsIdentity.GetCurrent().Name
        });
    }
}
