
public static class CommonTypes
{
    public enum PLAYER
    {
        PLAYER1,
        PLAYER2
    }

    public enum WIN_STATE
    {
        IN_PROGRESS,
        PLAYER1_WON,
        PLAYER2_WON
    }

    public enum MOVE_TYPE
    {
        PLACE_MARBLE,
        ROTATE_QUADRANT
    }

    public enum ROTATE_DIRECTION
    {
        CLOCKWISE,
        COUNTERCLOCKWISE
    };

    public enum SPACE_STATE
    {
        UNOCCUPIED,
        OCCUPIED_PLAYER1,
        OCCUPIED_PLAYER2,
    }
}
