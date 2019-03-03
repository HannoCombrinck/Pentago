using UnityEngine;

// TODO: Appropriate refactoring - this class won't be necessary anymore.
[CreateAssetMenu]
public class Settings : ScriptableObject
{
    public GameObject player1MarblePrefab; // This is probably a bad idea - shouldn't store prefab references in this manner
    public GameObject player2MarblePrefab; // This is probably a bad idea - shouldn't store prefab references in this manner
    public float marbleHeightOffset;
}
