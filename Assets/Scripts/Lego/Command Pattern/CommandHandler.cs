using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tilia.Interactions.Interactables.Interactables;
using Tilia.Interactions.Interactables.Interactors;

public class CommandHandler : MonoBehaviour
{
    public GameObject brickPrefab;
    public static GameObject heldBrick = null;
    private Vector3 position = new Vector3(0, 1, 0);
    private Material material = null;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setMaterial(Material material)
    {
        this.material = material;
    }

    public void setHeldBrick(InteractorFacade interactor)
    {
        GameObject brick = interactor.GrabbedObjects[0];
        heldBrick = brick;
    }

    public void addBrick(Material material)
    {
        ICommand command = new AddBrickCommand(position, material, heldBrick, brickPrefab);
        CommandInvoker.AddCommand(command);
    }

    public void changeColor(Material material)
    {
        BrickManipulator.ChangeColor(material, heldBrick);
        //ICommand command = new ChangeColorCommand(material, heldBrick);
        //CommandInvoker.AddCommand(command);
    }

    public void removeBrick(InteractorFacade interactor)
    {
        interactor.Ungrab();
        ICommand command = new RemoveBrickCommand(position, material, heldBrick, brickPrefab);
        CommandInvoker.AddCommand(command);
    }
}
