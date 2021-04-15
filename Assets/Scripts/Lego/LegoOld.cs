using System.Collections.Generic;
using Tilia.Interactions.Interactables.Interactables;
using UnityEngine;

[RequireComponent(typeof(InteractableFacade))]
[RequireComponent(typeof(Rigidbody))]
public class LegoOld : MonoBehaviour
{
    /***********************************/
    /* GameObject Component References */
    /***********************************/
    // reference to the InteractableFacade component of the object
    private InteractableFacade interactableFacade;
    // reference to the RigidBody component of the object
    private Rigidbody legoRigidbody;
    // reference to the Renderer component of the Lego object within the mesh container
    private Renderer legoRenderer;
    // reference to the Collider component of the Lego object within the mesh container
    private Collider legoCollider;

    /**********************************/
    /* Preview/Snap Feature Variables */
    /**********************************/
    // reference to the object(prefab) to be used when generating a preview object
    public GameObject previewObjectPrefab;
    // reference to the object created to show the user what the resulting lego placement will look like
    private GameObject previewObject;
    // reference to the GameObject that the lego will snap to
    private GameObject snapTargetObject;
    // reference to the renderer of the GameObject that the lego will snap to
    private Renderer snapTargetRenderer;
    // Direction of the Snap
    private Side snapTargetSide;
    // the maximum distance that a ray will travel in order to detect an object to snap to (default: 1f)
    public float maxSnappableDistance = 1f;

    /*******************************/
    /* Raycast LayerMask Variables */
    /*******************************/
    // array of layer names that are valid snap target layers
    public string[] validLegoLayers = { "Lego" };
    // layer mask used to only allow raycast to hit oobjects on the "Lego" layer
    private LayerMask legoLayerMask;

    /**************************************/
    /* Stud/Receptacle Location Variables */
    /**************************************/
    // the sides of the object where studs are located
    public List<Side> studs;
    // the sides of the object where stud receptacles are located
    public List<Side> studReceptacles;

    /*********************/
    /* ConnectionManager */
    /*********************/
    private ConnectionManager connectionManager;

    /***************************/
    /* Unity Methods Overrides */
    /***************************/
    // Start is called before the first frame update
    void Start()
    {
        connectionManager = new ConnectionManager();
        interactableFacade = GetComponent<InteractableFacade>();
        legoRigidbody = GetComponent<Rigidbody>();
        legoRenderer = GetComponentInChildren<Renderer>();
        legoCollider = GetComponentInChildren<BoxCollider>();
        legoLayerMask = LayerMask.GetMask(validLegoLayers);
    }

    // Update is called once per frame
    void Update()
    {
        if (interactableFacade.IsGrabbed)
        {
            // center of the lego object. Used as origin of Raycast
            Vector3 legoCenter = legoRenderer.bounds.center;

            // directions of the ray cast from the lego.
            Vector3 up = transform.rotation * (Vector3.up * maxSnappableDistance);
            Vector3 down = transform.rotation * (Vector3.down * maxSnappableDistance);

            // rays used to detect other Lego GameObjects
            Ray ray_up = new Ray(legoCenter, up);
            Ray ray_down = new Ray(legoCenter, down);

            Debug.DrawRay(ray_up.origin, ray_up.direction, Color.red);
            Debug.DrawRay(ray_down.origin, ray_down.direction, Color.green);

            RaycastHit hit_up;
            RaycastHit hit_down;

            bool raycast_result_up = Physics.Raycast(ray_up, out hit_up, maxSnappableDistance, legoLayerMask);
            bool raycast_result_down = Physics.Raycast(ray_down, out hit_down, maxSnappableDistance, legoLayerMask);

            if (raycast_result_up && raycast_result_down)
            {
                setSnapTarget((hit_up.distance < hit_down.distance ? hit_up.collider.gameObject : hit_down.collider.gameObject));
                displayPreviewObject();
            }
            else if (raycast_result_up)
            {
                setSnapTarget(hit_up.collider.gameObject);
                displayPreviewObject();
            }
            else if (raycast_result_down)
            {
                setSnapTarget(hit_down.collider.gameObject);
                displayPreviewObject();
            }
            else
            {
                if (snapTargetObject != null) setSnapTarget(null);
                if (previewObject != null) hidePreviewObject();
            }
        }
    }

    /*******************************/
    /* Grab/Ungrab Handler Methods */
    /*******************************/
    // grab handler. must be added to Grab Events in InteractableFacade
    public void OnLegoGrab()
    {
        this.createPreviewObject();
    }

    // ungrab handler. must be added to Grab Events in InteractableFacade
    public void onLegoUngrab()
    {
        // remove preview object
        this.destroyPreviewObject();

        // TODO: Move functionality to connectionManager
        // if we are snapping to another lego
        if (snapTargetObject != null)
        {
            legoRigidbody.useGravity = false;
            legoCollider.isTrigger = true;
            transform.position = getSnapToPosition();
            transform.rotation = getSnapToRotation();
            setSnapTarget(null);
        }
        // if we are not snapping to another lego
        else
        {
            legoRigidbody.useGravity = true;
            legoCollider.isTrigger = false;
        }
    }

    /******************************************/
    /* Snap Feature Position/Rotation Methods */
    /******************************************/
    /// FIXME: Take into account size and rotation of lego
    Vector3 getSnapToPosition()
    {
        Collider coll = GetComponentInChildren<BoxCollider>();
        Vector3 offset = snapTargetObject.transform.up * (float)(coll.bounds.size.y * LegoMeasurements.RATIO.BRICK_HEIGHT_NO_STUD_TO_TOTAL_HEIGHT);
        // Vector3 offset = snapTargetObject.transform.up * 0.28f;
        return snapTargetObject.transform.position + offset;
    }

    Quaternion getSnapToRotation()
    {
        return snapTargetObject.transform.rotation;
    }

    /**************************/
    /* Preview Object Methods */
    /**************************/
    // instantiates the preview object
    void createPreviewObject()
    {
        previewObject = Instantiate(this.previewObjectPrefab);
        previewObject.SetActive(false);
    }

    // sets the preview object to active
    void displayPreviewObject()
    {
        previewObject.SetActive(true);
        previewObject.transform.position = getSnapToPosition();
        previewObject.transform.rotation = getSnapToRotation();
    }

    // sets the preview object to not active
    void hidePreviewObject()
    {
        previewObject.SetActive(false);
    }

    // destroys the preview object
    void destroyPreviewObject()
    {
        Destroy(previewObject);
    }

    /******************/
    /* Helper Methods */
    /******************/
    void setSnapTarget(GameObject gameObject)
    {
        snapTargetObject = gameObject;
        if (snapTargetObject != null)
            snapTargetRenderer = snapTargetObject.GetComponent<Renderer>();
    }
}
