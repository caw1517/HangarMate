using System.Security.Claims;
using Api.Context;
using Api.Models;
using Api.Models.Dtos;
using Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class LogItemController : ControllerBase
{

    private readonly LogItemService _logItemService;
    public LogItemController(LogItemService logItemService)
    {
        _logItemService = logItemService;
    }
    
    [HttpGet]
    [Authorize]
    public async Task<ActionResult<List<LogItem>>> GetAll()
    {
        return await _logItemService.GetAllLogItems();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<LogItem>> GetLogItemById(Guid id)
    {
        var logItem = await _logItemService.GetLogItemById(id);
        if (logItem == null)
            return NotFound();
        
        return logItem;
    }

    [HttpPost]
    public async Task<ActionResult<LogItem>> CreateNewLogItem(CreateLogItemDto dto)
    {
        try
        {
            var newLogItem = await _logItemService.CreateLogItem(dto);
            
            return CreatedAtAction(nameof(GetLogItemById), new { id = newLogItem.Id }, newLogItem);

        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        
    }
}