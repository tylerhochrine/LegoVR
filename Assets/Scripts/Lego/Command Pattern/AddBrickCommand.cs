using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddBrickCommand : ICommand
{
    Vector3 position;
    Material material;
    GameObject brick;
    GameObject brickPrefab;

    public AddBrickCommand(Vector3 position, Material material, GameObject brick, GameObject brickPrefab)
    {
        this.position = position;
        this.material = material;
        this.brick = brick;
        this.brickPrefab = brickPrefab;
    }

    public void Execute()
    {
        brick = BrickManipulator.AddBrick(position, material, brickPrefab);
    }

    public void Undo()
    {
        BrickManipulator.RemoveBrick(brick.transform.position, material);
    }
}
