using System;
using UnityEngine;
using Tilia.Interactions.Interactables.Interactables;
using Tilia.Interactions.Interactables.Interactors;
using UnityEngine.Events;

[RequireComponent(typeof(EventRegisterProvider))]
[RequireComponent(typeof(EventNotifierProvider))]
public class Lego : MonoBehaviour
{
    private AudioSource snapSound;

    UnityAction<InteractorFacade> OnGrabAction;
    UnityAction<InteractorFacade> OnUngrabAction;

    private InteractableFacade interactableFacade;
    private EventController eventController;
    private bool IsGrabbed;

    void Awake()
    {
        snapSound = GetComponent<AudioSource>();

        OnGrabAction += OnGrab;
        OnUngrabAction += OnUngrab;

        UpdateInteractableEvents();


        eventController = new EventController();
        GetComponent<EventRegisterProvider>().SetEventRegister(new EventRegister(eventController));
        GetComponent<EventNotifierProvider>().SetEventNotifier(new EventNotifier(eventController));

        // register snap event
        eventController.RegisterEvent("onSnap");
            
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

        eventController.RegisterForEvent("onSnap", OnSnap);
    }

    void Start()
    {
        this.IsGrabbed = false;

        // register event bridges
        eventController.RegisterForEvent("onPotentialConnectionFound", DisplayPreviewObjectOnPotentialConnectionFound);
        eventController.RegisterForEvent("onNoPotentialConnectionFound", HidePreviewObjectOnNoPotentialConnectionFound);
    }

    void Update()
    {
        if (IsGrabbed) eventController.OnEvent("whileGrabbed");
    }

    // event callers // INFO: these serve as the entry point for onGrab and onUngrab events and are called by the interactor
    public void OnGrab(InteractorFacade interactorFacade)
    {
        IsGrabbed = true;
        eventController.OnEvent("onGrab");
    }

    public void OnUngrab(InteractorFacade interactorFacade)
    {
        IsGrabbed = false;
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

    public Transform GetGroupContainer()
    {
        return gameObject.transform.parent;
    }

    public void SetGroupContainer(Transform target)
    {
        gameObject.transform.SetParent(target);
        UpdateInteractableEvents();
    }

    private void UpdateInteractableEvents()
    {
        interactableFacade = GetComponentInParent<InteractableFacade>();
        interactableFacade.Grabbed.AddListener(OnGrabAction);
        interactableFacade.Ungrabbed.AddListener(OnUngrabAction);
    }

    private void OnSnap()
    {
        snapSound.Play();
    }
}