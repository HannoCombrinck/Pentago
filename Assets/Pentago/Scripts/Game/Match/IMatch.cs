using System.Collections.Generic;

// A Match manages the lifetime of an instance of a Pentago along with the Players playing the game.
public interface IMatch
{
    IGame Game { get; set; }
    void Begin();
    void End();

    //List<IPlayer> GetPlayers();
}
