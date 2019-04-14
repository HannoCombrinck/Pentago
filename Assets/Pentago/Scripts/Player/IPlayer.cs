using static IGame;

public interface IPlayer
{
    string GetName();
    PLAYER GetPlayerID();
    void PlaceMarble(int spaceIndex);
    void RotateQuadrant(int quadrantIndex, ROTATE_DIRECTION direction);
    void StartTurn();
    void EndTurn();
}
