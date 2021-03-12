using System.Collections.Generic;
using UnityEngine;

public class SnapManager
{
    // reference to the Lego Object that owns this SnapManager
    private GameObject legoObject;
    // array of snappable sides
    private List<Side> snappableSides;

    public SnapManager(GameObject legoObject, List<Side> snappableSides)
    {
        this.legoObject = legoObject;
        this.snappableSides = snappableSides;
    }
}