using UnityEngine;

public class Lego : MonoBehaviour
{

    private ConnectionManager connectionManager;
    private SnapManager snapManager;

    private bool isGrabbed = false;

    void Start()
    {
        connectionManager = new ConnectionManager(this.gameObject);
        snapManager = new SnapManager(this.gameObject, connectionManager.getSupportedSides());
    }

    // void Update {

    // }

    public void OnLegoGrab()
    {
        isGrabbed = true;
    }

    public void onLegoUngrab()
    {
        isGrabbed = false;
    }
}