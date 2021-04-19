using UnityEngine;

[RequireComponent(typeof(EventRegisterProvider))]
public class PreviewManager : MonoBehaviour
{
    private EventRegister eventRegister;

    [SerializeField]
    private GameObject previewObjectTemplate;
    private GameObject previewObject;
    private LegoConnector sender;
    private LegoConnector target;

    void Start()
    {
        eventRegister = GetComponent<EventRegisterProvider>().GetEventRegister();

        eventRegister.RegisterForEvent("onGrab", CreatePreviewObject);
        eventRegister.RegisterForEvent("onUngrab", DestroyPreviewObject);
        eventRegister.RegisterForEvent("DisplayPreviewObject", DisplayPreviewObject);
        eventRegister.RegisterForEvent("HidePreviewObject", HidePreviewObject);
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
        previewObject = Instantiate(previewObjectTemplate);

        // takes the template and resets the layers to default and sets each as a trigger to make the GameObject not have collisions or be effected by raycasts intended for Lego GameObjects
        Collider[] colliders = previewObject.GetComponentsInChildren<Collider>();
        foreach(Collider collider in colliders)
        {
            collider.gameObject.layer = 0;
            collider.isTrigger = true;
        }

        HidePreviewObject();
    }

    public void DestroyPreviewObject()
    {
        Destroy(previewObject);
    }

    public void DisplayPreviewObject()
    {
        target.SetConnectionTransform(sender.transform, previewObject.transform);
        previewObject.SetActive(true);
    }

    public void HidePreviewObject()
    {
        previewObject.SetActive(false);
    }
}