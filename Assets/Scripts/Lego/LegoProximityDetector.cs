using UnityEngine;
using System.Collections.Generic;

public class LegoProximityDetector
{
    private GameObject legoObject;
    private Renderer legoObjectRenderer;
    private List<Side> supportedSides;
    private float maxSnappableDistance;
    private List<Ray> rays;

    // array of layer names that are valid snap target layers
    private string[] validLegoLayers = { "Lego" };
    // layer mask used to only allow raycast to hit oobjects on the "Lego" layer
    private LayerMask legoLayerMask;

    public LegoProximityDetector(GameObject legoObject, List<Side> supportedSides, float maxSnappableDistance)
    {
        this.legoObject = legoObject;
        this.legoObjectRenderer = legoObject.GetComponent<Renderer>();
        this.supportedSides = supportedSides;
        this.maxSnappableDistance = maxSnappableDistance;
    }

    private void updateRayList()
    {
        Vector3 legoCenter = legoObjectRenderer.bounds.center;
        foreach (Side side in this.supportedSides)
            rays.Insert(0, new Ray(legoCenter, legoObject.transform.rotation * (side.getDirectionVector3() * maxSnappableDistance)));
    }

    public GameObject getClosestGameObject()
    {
        GameObject closest = null;
        float closetstDistance = float.MaxValue;
        RaycastHit hit;

        foreach (Ray ray in rays)
        {
            Debug.DrawRay(ray.origin, ray.direction, Color.red);
            bool found = Physics.Raycast(ray, out hit, maxSnappableDistance, legoLayerMask);

            if (found && hit.distance < closetstDistance)
            {
                closest = hit.collider.gameObject;
                closetstDistance = hit.distance;
            }
        }

        return closest;
    }

}