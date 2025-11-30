using Api.Models;
namespace Api.Services;

public class LogItemService
{
    private static List<LogItem> LogItems { get; }

    static LogItemService()
    {
        LogItems = new List<LogItem>
        {
            new LogItem { Id = Guid.NewGuid(), Discrepancy = "RH Fuel Pump Inop", CreatedOn = DateTime.Now, SignedOff = false, SignedOffOn = null},
            new LogItem { Id = Guid.NewGuid(), Discrepancy = "HF Static", CreatedOn = DateTime.Now, SignedOff = false, SignedOffOn = null}
        };
    }

    public static List<LogItem> GetAll() => LogItems;

    public static LogItem? Get(Guid id) => LogItems.FirstOrDefault(i => i.Id == id);

    public static void AddLogItem(LogItem newItem)
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
    }
}