using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tilia.Interactions.Interactables.Interactables;
using Tilia.Interactions.Interactables.Interactors;

public class BrickManip : MonoBehaviour
{

    public GameObject prefab;
    GameObject cloneBrick = null;

    public void addBrick()
    {
        //GameObject player = GameObject.FindWithTag("Player");
        //Vector3 spawnPosition = player.transform.position;
        Vector3 spawnPosition = new Vector3(0, 1, 0);
        cloneBrick = Instantiate(prefab, spawnPosition, Quaternion.identity);
    }

    public void removeBrick()
    {
        if(cloneBrick != null)
        {
            //cloneBrick = cloneBrick.transform.GetChild(0).GetChild(0).gameObject;
            //cloneBrick.SetActive(false);

            InteractableFacade interactable = cloneBrick.GetComponent<InteractableFacade>();
            //InteractorFacade interactor = cloneBrick.GetComponentInParent<InteractorFacade>();
            InteractorFacade[] interactors = GameObject.FindObjectsOfType<InteractorFacade>();
            
            for (int i = 0; i < interactors.Length; i++)
            {
                interactable.Ungrab(interactors[i]);
            }

            Destroy(cloneBrick);

            //StartCoroutine(DestroyLater(cloneBrick));            
        }
    }

    IEnumerator DestroyLater(GameObject obj)
    {
        yield return new WaitForEndOfFrame();
        Destroy(obj);
    }

    public void setBrick(GameObject b)
    {
        cloneBrick = b;
    }
}
