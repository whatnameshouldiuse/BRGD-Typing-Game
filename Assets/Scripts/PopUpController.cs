using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpController : MonoBehaviour
{
    public string popText;  //The text the player has to type to destroy this popup
    public bool topLayer;   //Boolean for if this popup is in the active layer the player interacts with

    // Start is called before the first frame update
    void Start()
    {
        //Assign popup text
        popText = "example";

        //Make this popup active (for now, until spawning/handling is finished)
        topLayer = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Winner()
    {
        Destroy(this.gameObject);
    }
}
