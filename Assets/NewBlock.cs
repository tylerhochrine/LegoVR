using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBlock : MonoBehaviour
{

    public GameObject block;

    public void addBlock()
    {
        Instantiate(block, new Vector3(0, 0, 0), Quaternion.identity);
    }
}
