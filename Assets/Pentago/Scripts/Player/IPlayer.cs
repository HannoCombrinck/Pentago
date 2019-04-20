using static IGame;

public interface IPlayer
{
    string GetName();
    PLAYER GetPlayerID();
    void ExecutePlaceMarble(int spaceIndex);
    void ExecuteRotateQuadrant(int quadrantIndex, ROTATE_DIRECTION direction);
}
