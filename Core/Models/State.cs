namespace Sokofarm.Core.Models;

public class State
{
    public Grid Grid { get; set; }
    public Position Farmer { get; set; }
    public int CurrentLevel { get; set; }
    public bool IsCurrentLevelSolved { get; set; }
    public int SeedsCount { get; set; }
    public int StoragesCount { get; set; }
    public int SeedsOnStorageCount { get; set; }
}
