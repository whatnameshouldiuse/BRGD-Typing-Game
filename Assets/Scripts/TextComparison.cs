using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextComparison : MonoBehaviour
{
    public List<GameObject> allPops = new List<GameObject>(); //List for all popups in level
    public List<GameObject> activePops = new List<GameObject>(); //List for all popups currently active/destroyable

    // Start is called before the first frame update
    void Start()
    {
        //Populate list of popups with all popups
        foreach (Transform child in transform)
        {
            if (child.gameObject.name.Contains("Pop"))
            {
                allPops.Add(child.gameObject);
            }
        }

        //Populate list of active popups for starting layer
        foreach (GameObject g in allPops)
        {
            if(g.GetComponent<PopUpController>().topLayer == true)
            {
                activePops.Add(g);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
