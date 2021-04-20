using System.Collections.Generic;
using UnityEngine;

public static class BrickManipulator
{
    static List<GameObject> bricks;

    public static GameObject AddBrick(Vector3 position, Material material, GameObject brick, GameObject brickMesh)
    {
        GameObject newBrick = Object.Instantiate(brick, position, Quaternion.identity);
        // GameObject iBrickMesh = Object.Instantiate(brickMesh);
        // iBrickMesh.transform.parent = newBrick.transform.Find("MeshContainer");
        // iBrickMesh.transform.localPosition = Vector3.zero;
        
        // set each renderer material
        Renderer[] renderers = newBrick.GetComponentsInChildren<Renderer>();
        foreach(Renderer renderer in renderers)
            renderer.sharedMaterial = material;
       
        // object tracker
        if(bricks == null)
        {
            bricks = new List<GameObject>();
        }

        bricks.Add(newBrick);
        
        return newBrick;
    }

    public static GameObject AddBrick(Vector3 position, GameObject brickPrefab)
    {
        GameObject newBrick = GameObject.Instantiate(brickPrefab, position, Quaternion.identity);

        // object tracker
        if (bricks == null)
        {
            bricks = new List<GameObject>();
        }

        bricks.Add(newBrick);

        return newBrick;
    }

    public static void ChangeColor(Material material, GameObject brick)
    {
        brick.transform.Find("MeshContainer").GetChild(0).name = "Lego";
        foreach(Transform unit in brick.transform.Find("MeshContainer/Lego/LegoUnitContainer"))
        {
            if(unit.name == "BrickUnit")
            {
                unit.GetComponentInChildren<Renderer>().sharedMaterial = material;
            }
        }

        //brick.GetComponentInChildren<Renderer>().sharedMaterial = material;
    }

    public static void RemoveBrick(Vector3 position, Material material)
    {
        for(int i = 0; i < bricks.Count; i++)
        {
            if(bricks[i].transform.position == position)
            {
                GameObject.Destroy(bricks[i]);
                bricks.RemoveAt(i);
            }
        }
    }
}
