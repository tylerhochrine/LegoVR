using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickManip : MonoBehaviour
{

    public GameObject prefab;
    GameObject cloneBrick = null;

    public void addBrick()
    {
        cloneBrick = Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity);
    }

    public void removeBrick()
    {
        if(cloneBrick != null)
        {
            cloneBrick = cloneBrick.transform.GetChild(0).GetChild(0).gameObject;
            cloneBrick.SetActive(false);
            //Destroy(cloneBrick);
        }
    }

    public void setBrick(GameObject b)
    {
        cloneBrick = b;
    }
}
