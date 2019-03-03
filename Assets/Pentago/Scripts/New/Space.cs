using UnityEngine;

public class Space : MonoBehaviour
{
    public CommonTypes.SPACE_STATE state = CommonTypes.SPACE_STATE.UNOCCUPIED;
    public int spaceIndex;

    public Game game { get; set; }

    private GameObject currentMarble;

    void Start()
    {
        Debug.Assert(game.state != null);
        Debug.Assert(game.settings != null);
    }

    void Update()
    {
        if (state != game.state.spaceState[spaceIndex])
        {
            state = game.state.spaceState[spaceIndex];
            //Debug.Log("Space " + gameObject.name + " has changed stated");

            switch (state)
            {
                case CommonTypes.SPACE_STATE.UNOCCUPIED:
                    if (currentMarble != null)
                        Destroy(currentMarble);

                    currentMarble = null;
                    break;
                case CommonTypes.SPACE_STATE.OCCUPIED_PLAYER1:
                    if (currentMarble != null)
                        Destroy(currentMarble);

                    currentMarble = Instantiate(game.settings.player1MarblePrefab, transform.position + Vector3.up * game.settings.marbleHeightOffset, Quaternion.identity);
                    currentMarble.transform.SetParent(transform);
                    break;
                case CommonTypes.SPACE_STATE.OCCUPIED_PLAYER2:
                    if (currentMarble != null)
                        Destroy(currentMarble);

                    currentMarble = Instantiate(game.settings.player2MarblePrefab, transform.position + Vector3.up * game.settings.marbleHeightOffset, Quaternion.identity);
                    currentMarble.transform.SetParent(transform);
                    break;
            }

            // TODO: Fire event (space state changed to reflect new game state)
        }
    }
}
