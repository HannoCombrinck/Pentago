using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Search for spatial component descendents and sort them according to their world
// space positions in the xz plane. Maintain and expose this sorted list.
public class SpatialSorter<SpatialComponentType> : MonoBehaviour
    where SpatialComponentType : Component
{
    public List<SpatialComponentType> sortedSpatials;
    private SpatialComponentType[] spatials;

    protected virtual void Awake()
    {
        spatials = GetComponentsInChildren<SpatialComponentType>();
        SortSpatials();
    }

    public void SortSpatials()
    {
        var xMin = spatials.Min(s => s.gameObject.transform.position.x);
        var xMax = spatials.Max(s => s.gameObject.transform.position.x);
        var xRange = (xMax - xMin) * 2.0f; // * 2.0f to prevent edge cases, albeit a bit excessive.

        // Sorts spatial components from top left to bottom right based on world space positions in xz plane.
        sortedSpatials = spatials.OrderBy(s => s.gameObject.transform.position.x - (s.gameObject.transform.position.z * xRange)).ToList();
    }
}
