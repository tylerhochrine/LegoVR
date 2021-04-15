using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColorCommand : ICommand
{
    Material material;
    GameObject brick;

    public ChangeColorCommand(Material material, GameObject brick)
    {
        this.material = material;
        this.brick = brick;
    }

    public void Execute()
    {
        BrickManipulator.ChangeColor(material, brick);
    }

    public void Undo() { }
}
