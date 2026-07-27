using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Kanban;

public class Projekt(string name)
{
    public string Name { get; } = name;

    public List<string> ToDo { get; init; } = [];
    public List<string> Doing { get; init; } = [];
    public List<string> Done { get; init; } = [];

    public string ToJson()
    {
        return JsonSerializer.Serialize(this, new JsonSerializerOptions
        {
            WriteIndented = true,
            IndentCharacter = ' ',
            IndentSize = 2
        });
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
    
    #endregion
}