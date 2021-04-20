using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddBrickCommand : ICommand
{
    Vector3 position;
    Material material;
    GameObject brick;
    GameObject brickPrefab;
    GameObject brickMesh;

    public AddBrickCommand(Vector3 position, Material material, GameObject brick, GameObject brickPrefab, GameObject brickMesh)
    {
        this.position = position;
        this.material = material;
        this.brick = brick;
        this.brickPrefab = brickPrefab;
        this.brickMesh = brickMesh;
    }

    public void Execute()
    {
        //brick = BrickManipulator.AddBrick(position, material, brickPrefab, brickMesh);
        brick = BrickManipulator.AddBrick(position, brickPrefab);
    }

    public void Undo()
    {
        BrickManipulator.RemoveBrick(brick.transform.position, material);
    }
}
