using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Kanban;

public class Projekt
{
    public required string Name { get; init; }
    public required List<string> ToDo { get; init; }
    public required List<string> Doing { get; init; }
    public required List<string> Done { get; init; }

    public Projekt(string json)
    {
        JsonSerializer.Deserialize<Projekt>(json);
    }

    public Projekt() {}

    public string ToJson()
    {
        return JsonSerializer.Serialize(this);
    }

    public void AddItem(string item)
    {
        ToDo.Add(item);
    }
    
    #region moveItem

    public void DoToDoing(string item)
    {
        ToDo.Remove(item);
        Doing.Add(item);
    }

    public void DoToDone(string item)
    {
        ToDo.Remove(item);
        Done.Add(item);
    }

    public void DoingToDone(string item)
    {
        Doing.Remove(item);
        Done.Add(item);
    }

    public void DoingToDo(string item)
    {
        Doing.Remove(item);
        ToDo.Add(item);
    }

    public void DoneToDoing(string item)
    {
        Done.Remove(item);
        Doing.Add(item);
    }

    public void DoneToDo(string item)
    {
        Done.Remove(item);
        ToDo.Add(item);
    }
    
    #endregion
}