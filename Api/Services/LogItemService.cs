using Api.Context;
using Api.Models;
using Api.Models.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public class LogItemService
{
    private readonly DatabaseContext _context;

    public LogItemService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<List<LogItem>> GetAllLogItems()
    {
        return await _context.LogItems.ToListAsync();
    }

    public async Task<LogItem?> GetLogItemById(Guid id)
    {
        return await _context.LogItems.FindAsync(id);
    }

    public async Task<LogItem> CreateLogItem(CreateLogItemDto dto)
    {
        var newLogItem = new LogItem
        {
            Id = Guid.NewGuid(),
            Discrepancy = dto.Discrepancy,
            SignedOff = false,
            CreatedOn = DateTime.UtcNow,
            SignedOffOn = null
        };
        
        var logItem = _context.LogItems.Add(newLogItem).Entity;
        await _context.SaveChangesAsync();
        
        return logItem;
    }

/*public static LogItem? Get(Guid id) => LogItems.FirstOrDefault(i => i.Id == id);*/

    /*public static void AddLogItem(LogItem newItem)
    {
        newItem.Id = Guid.NewGuid();
        LogItems.Add(newItem);
    }

    public static void Delete(Guid id)
    {
        var logItem = Get(id);
        if (logItem is null)
            return;
        
        LogItems.Remove(logItem);
    }

    public static void Update(LogItem logItem)
    {
        var index = LogItems.FindIndex(i => i.Id == logItem.Id);
        if (index == -1)
        {
            LogItems[index] = logItem;
        }
    }*/
}