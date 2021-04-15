using System;
using UnityEngine;

[RequireComponent(typeof(EventRegisterProvider))]
[RequireComponent(typeof(EventNotifierProvider))]
public class Lego : MonoBehaviour
{
    private EventController eventController;
    private bool isGrabbed;

    void Awake()
    {
        this.eventController = new EventController();
        this.GetComponent<EventRegisterProvider>().SetEventRegister(new EventRegister(this.eventController));
        this.GetComponent<EventNotifierProvider>().SetEventNotifier(new EventNotifier(this.eventController));

        // register various grab events
        eventController.RegisterEvent("onGrab");
        eventController.RegisterEvent("onUngrab");
        eventController.RegisterEvent("whileGrabbed");

        // register connection events
        eventController.RegisterEvent("onPotentialConnectionFound");
        eventController.RegisterEvent("onNoPotentialConnectionFound");

        // register preview object events
        eventController.RegisterEvent("DisplayPreviewObject");
        eventController.RegisterEvent("HidePreviewObject");
    }

    void Start()
    {
        this.isGrabbed = false;

        // register event bridges
        eventController.RegisterForEvent("onPotentialConnectionFound", this.DisplayPreviewObjectOnPotentialConnectionFound);
        eventController.RegisterForEvent("onNoPotentialConnectionFound", this.HidePreviewObjectOnNoPotentialConnectionFound);
    }

    void Update()
    {
        if (this.isGrabbed) this.eventController.OnEvent("whileGrabbed");
    }

    // event callers // INFO: these serve as the entry point for onGrab and onUngrab events and are called by the interactor
    public void OnGrab()
    {
        this.isGrabbed = true;
        eventController.OnEvent("onGrab");
    }

    public void OnUngrab()
    {
        this.isGrabbed = false;
        eventController.OnEvent("onUngrab");
    }

    // define event bridges
    public void DisplayPreviewObjectOnPotentialConnectionFound()
    {

        PreviewManager previewManager = GetPreviewManager();
        ConnectionManager connectionManager = GetConnectionManager();
        previewManager.UpdateSender(connectionManager.getCurrentSender());
        previewManager.UpdateTarget(connectionManager.getCurrentTarget());
        this.eventController.OnEvent("DisplayPreviewObject");
    }

    public void HidePreviewObjectOnNoPotentialConnectionFound()
    {
        this.eventController.OnEvent("HidePreviewObject");
    }

    public ConnectionManager GetConnectionManager()
    {
        return GetComponent<ConnectionManager>();
    }

    public PreviewManager GetPreviewManager()
    {
        return GetComponent<PreviewManager>();
    }
}