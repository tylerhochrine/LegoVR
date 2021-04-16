using UnityEngine;

[RequireComponent(typeof(EventRegisterProvider))]
[RequireComponent(typeof(EventNotifierProvider))]
public class PreviewManager : MonoBehaviour
{
    private EventRegister eventRegister;
    private EventNotifier eventNotifier;

    [SerializeField]
    private GameObject previewObjectTemplate;
    private GameObject previewObject;
    private LegoConnector sender;
    private LegoConnector target;

    void Start()
    {
        this.eventRegister = this.GetComponent<EventRegisterProvider>().GetEventRegister();
        this.eventNotifier = this.GetComponent<EventNotifierProvider>().GetEventNotifier();

        this.eventRegister.RegisterForEvent("onGrab", this.CreatePreviewObject);
        this.eventRegister.RegisterForEvent("onUngrab", this.DestroyPreviewObject);
        this.eventRegister.RegisterForEvent("DisplayPreviewObject", this.DisplayPreviewObject);
        this.eventRegister.RegisterForEvent("HidePreviewObject", this.HidePreviewObject);
    }

    public void UpdateSender(LegoConnector sender)
    {
        this.sender = sender;
    }

    public void UpdateTarget(LegoConnector target)
    {
        this.target = target;
    }

    private void CreatePreviewObject()
    {
        this.previewObject = Object.Instantiate(this.previewObjectTemplate);

        // takes the template and resets the layers to default and sets each as a trigger to make the GameObject not have collisions or be effected by raycasts intended for Lego GameObjects
        Collider[] colliders = this.previewObject.GetComponentsInChildren<Collider>();
        foreach(Collider collider in colliders)
        {
            collider.gameObject.layer = 0;
            collider.isTrigger = true;
        }

        this.HidePreviewObject();
    }

    public void DestroyPreviewObject()
    {
        Object.Destroy(this.previewObject);
    }

    public void DisplayPreviewObject()
    {
        // this.previewObject.transform.rotation = target.GetConnectionRotation(sender);
        // this.previewObject.transform.position = target.GetConnectionPosition(sender);
        target.SetConnectionTransform(sender.transform, previewObject.transform);

        this.previewObject.SetActive(true);
    }

    public void HidePreviewObject()
    {
        this.previewObject.SetActive(false);
    }
}