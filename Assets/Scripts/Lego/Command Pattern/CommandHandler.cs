using UnityEngine;
using Tilia.Interactions.Interactables.Interactors;

public class CommandHandler : MonoBehaviour
{
    public GameObject brickPrefab;
    public static GameObject heldBrick = null;
    private Vector3 position = new Vector3(0, 1, 0);
    private Material material = null;
    [HideInInspector]
    public GameObject brickMesh;

    public void setMaterial(Material material)
    {
        this.material = material;
    }

    public void setHeldBrick(InteractorFacade interactor)
    {
        GameObject brick = interactor.GrabbedObjects[0];
        heldBrick = brick;
    }

    public void setBrickMesh(GameObject brickMesh)
    {
        this.brickMesh = brickMesh;
    }

    public void addBrick(Material material)
    {
        ICommand command = new AddBrickCommand(position, material, heldBrick, brickPrefab, brickMesh);
        CommandInvoker.AddCommand(command);
    }

    public void changeColor(Material material)
    {
        if (heldBrick != null)
        {
            BrickManipulator.ChangeColor(material, heldBrick);
        }
        //ICommand command = new ChangeColorCommand(material, heldBrick);
        //CommandInvoker.AddCommand(command);
    }

    public void removeBrick(InteractorFacade interactor)
    {
        if (heldBrick != null)
        {
            interactor.Ungrab();
            material = heldBrick.GetComponentInChildren<Renderer>().sharedMaterial;
            ICommand command = new RemoveBrickCommand(position, material, heldBrick, brickPrefab, brickMesh);
            CommandInvoker.AddCommand(command);
        }
    }
}
