using Tilia.Interactions.Interactables.Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InteractableFacade))]
[RequireComponent(typeof(Rigidbody))]
public class Lego : MonoBehaviour
{
    private InteractableFacade interactableFacade;
    private Rigidbody rbody;
    Collider coll;
    private LayerMask legoLayerMask;
    private GameObject legoToSnapTo;
    public float maxSnappableDistance = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        interactableFacade = GetComponent<InteractableFacade>();
        rbody = GetComponent<Rigidbody>();
        coll = GetComponentInChildren<BoxCollider>();
        legoLayerMask = LayerMask.GetMask("Lego");
    }

    // Update is called once per frame
    void Update()
    {
        if (interactableFacade.IsGrabbed)
        {
            Ray ray = new Ray(transform.position + new Vector3(0.1f, -0.001f, 0.1f), Vector3.down * 5);
            RaycastHit hit;
            Debug.DrawRay(ray.origin, ray.direction, Color.red);

            if (Physics.Raycast(ray, out hit, maxSnappableDistance, legoLayerMask))
            {
                legoToSnapTo = hit.collider.gameObject;
            }
        }
    }

    public void OnLegoGrab()
    {
        Debug.Log("Lego Grabbed");
        rbody.useGravity = true;
        coll.isTrigger = false;

        legoToSnapTo = null;
    }

    public void onLegoUngrab()
    {

        Debug.Log("Lego Ungrabbed");
        if (legoToSnapTo != null)
        {
            rbody.useGravity = false;
            coll.isTrigger = true;
            transform.position = legoToSnapTo.transform.position + new Vector3(0, 0.24f, 0);
            transform.rotation = legoToSnapTo.transform.rotation;
        }
    }
}
