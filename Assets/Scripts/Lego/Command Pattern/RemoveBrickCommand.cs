using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveBrickCommand : ICommand
{
    Vector3 position;
    Material material;
    GameObject brick;
    GameObject brickPrefab;

    public RemoveBrickCommand(Vector3 position, Material material, GameObject brick, GameObject brickPrefab)
    {
        this.position = position;
        this.material = material;
        this.brick = brick;
        this.brickPrefab = brickPrefab;
    }

    public void Execute()
    {
        position = brick.transform.position;
        BrickManipulator.RemoveBrick(position, material);
    }

    public void Undo()
    {
        brick = BrickManipulator.AddBrick(position, material, brickPrefab);
    }
}
