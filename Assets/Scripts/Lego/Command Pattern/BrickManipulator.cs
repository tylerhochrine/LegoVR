using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tilia.Interactions.Interactables.Interactables;
using Tilia.Interactions.Interactables.Interactors;

public static class BrickManipulator
{
    static List<GameObject> bricks;

    public static GameObject AddBrick(Vector3 position, Material material, GameObject brick, GameObject brickMesh)
    {
        GameObject newBrick = GameObject.Instantiate(brick, position, Quaternion.identity);
        GameObject iBrickMesh = GameObject.Instantiate(brickMesh);
        iBrickMesh.transform.parent = newBrick.transform.Find("MeshContainer");
        iBrickMesh.transform.localPosition = Vector3.zero;
        newBrick.GetComponentInChildren<Renderer>().sharedMaterial = material;
        if(bricks == null)
        {
            bricks = new List<GameObject>();
        }
        bricks.Add(newBrick);
        
        return newBrick;
    }

    public static void ChangeColor(Material material, GameObject brick)
    {
        brick.GetComponentInChildren<Renderer>().sharedMaterial = material;
    }

    public static void RemoveBrick(Vector3 position, Material material)
    {
        for(int i = 0; i < bricks.Count; i++)
        {
            if(bricks[i].transform.position == position)
            {
                GameObject.Destroy(bricks[i]);
                bricks.RemoveAt(i);
                break;
            }
        }
    }
}
