using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The PentagoController encapsulates both "game mode" and "game state" (in UE4 jargon) 
// Actions like "New Game" and checking win conditions all happen here
// The PentagoController can query the board state vector from the BoardController and also apply a board state vector to the BoardController
// The PentagoController determines which player plays first, which player wins, the game's duration and the game's rules
// The PlayerController can directly interact with the BoardController, however, the PentagoController is in control of the game at a higher lever
// The PentagoController lets the PlayerController interact with the BoardController and monitors the state changes

interface GameActionOld
{
    void Execute(PentagoOld.PlayerID[,] gameState);
    string GetDescription();
}

class PlaceMarbleAction : GameActionOld
{
    private int row;
    private int col;
    private PentagoOld.PlayerID player;

    public PlaceMarbleAction(int _row, int _col, PentagoOld.PlayerID _player)
    {
        row = _row;
        col = _col;
        player = _player;
    }

    public void Execute(PentagoOld.PlayerID[,] gameState)
    {
        gameState[row, col] = player;
    }

    public string GetDescription()
    {
        string descriptionString = "Player 1 ";
        if (player == PentagoOld.PlayerID.PLAYER2)
            descriptionString = "Player 2 ";

        descriptionString += "placed a marble on row " + row + " column " + col;

        return descriptionString;
    }
}

class RotateSubBoardAction : GameActionOld
{
    private int row;
    private int col;
    private PentagoOld.RotateDirection direction;
    private PentagoOld.PlayerID player;

    public RotateSubBoardAction(int _row, int _col, PentagoOld.RotateDirection _direction, PentagoOld.PlayerID _player)
    {
        row = _row;
        col = _col;
        direction = _direction;
        player = _player;
    }

    public void Execute(PentagoOld.PlayerID[,] gameState)
    {
        // RotateSubMatrix(gameState, 3, row, col);
    }

    public string GetDescription()
    {
        string descriptionString = "Player 1 ";
        if (player == PentagoOld.PlayerID.PLAYER2)
            descriptionString = "Player 2 ";

        string directionString = "left";
        if (direction == PentagoOld.RotateDirection.RIGHT)
            directionString = "right";

        descriptionString += "rotated sub board " + row + ", " + col + " to the " + directionString;

        return descriptionString;
    }
}

public class PentagoOld
{
    public enum PlayerID : byte
    {
        NONE = 0,
        PLAYER1 = 1,
        PLAYER2 = 2
    }

    public enum RotateDirection
    {
        LEFT,
        RIGHT
    }

    private static int BOARD_SIZE = 6;
    private PlayerID[,] gameState = new PlayerID[BOARD_SIZE, BOARD_SIZE];
    private PlayerID currentPlayer = PlayerID.PLAYER1;

    private struct HistoryEntry
    {
        public GameActionOld action;
        public PlayerID[,] state;
    }
    private List<HistoryEntry> history = new List<HistoryEntry>();


    public void NewGame()
    {
        currentPlayer = PlayerID.PLAYER1;
        for (int row = 0; row < BOARD_SIZE; ++row)
            for (int col = 0; col < BOARD_SIZE; ++col)
                gameState[row, col] = PlayerID.NONE;
    }

    public bool PlaceMarble(int row, int col)
    {
        if (gameState[row, col] != PlayerID.NONE)
            return false;

        ExecuteAction(new PlaceMarbleAction(row, col, currentPlayer));
        return true;
    }

    public void RotateSubBoard(int row, int col, RotateDirection direction)
    {
        ExecuteAction(new RotateSubBoardAction(row, col, direction, currentPlayer));
    }

    private PlayerID CheckWinner()
    {
        // TODO: Check if 5 marbles of same player are aligned 
        return PlayerID.NONE;
    }

    private void ExecuteAction(GameActionOld action)
    {
        history.Add(new HistoryEntry
        {
            action = action,
            state = (PlayerID[,])gameState.Clone()
        });
        action.Execute(gameState);
    }
}

public class PentagoController : MonoBehaviour {

	void Start ()
    {
		
	}
	
	void Update ()
    {
		
	}
}
