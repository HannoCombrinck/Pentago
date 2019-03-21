
using System.Collections.Generic;
using UnityEngine;

// Sort a List of GameObjects (with attached SpatialComponent) according to their world space positions in the xz plane.
public class SpatialSorter<SpatialComponentType>
    where SpatialComponentType : Component
{
    private List<SpatialComponentType> sortedSpatials;
    private SpatialComponentIndexComparer spatialComponentIndexComparer;

    public SpatialSorter(SpatialComponentType[] spatials, ref List<SpatialComponentType> sortedSpatials)
    {
        this.sortedSpatials = sortedSpatials;
        var xMin = float.MaxValue;
        var xMax = float.MinValue;

        foreach (var spatialComponent in spatials)
        {
            this.sortedSpatials.Add(spatialComponent);

            if (spatialComponent.gameObject.transform.position.x < xMin)
                xMin = spatialComponent.gameObject.transform.position.x;
            else if (spatialComponent.gameObject.transform.position.x > xMax)
                xMax = spatialComponent.gameObject.transform.position.x;
        }

        var xRange = (xMax - xMin) * 2.0f; // * 2.0f to prevent edge cases, albeit a bit excessive
        spatialComponentIndexComparer = new SpatialComponentIndexComparer(xRange);

        Sort();
    }

    public void Sort()
    {
        sortedSpatials.Sort(spatialComponentIndexComparer);
    }

    // Comparer to sorts spatial components from top left to bottom right based on world space positions in xz plane.
    private class SpatialComponentIndexComparer : IComparer<SpatialComponentType>
    {
        readonly float xRange;

        public SpatialComponentIndexComparer(float xRange)
        {
            this.xRange = xRange;
        }

        public int Compare(SpatialComponentType x, SpatialComponentType y)
        {
            var xIndex = x.gameObject.transform.position.x - x.gameObject.transform.position.z * xRange;
            var yIndex = y.gameObject.transform.position.x - y.gameObject.transform.position.z * xRange;
            return xIndex < yIndex ? -1 : 1;
        }
    }
   
}
