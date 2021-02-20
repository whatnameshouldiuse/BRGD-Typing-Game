using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopUpController : MonoBehaviour
{
    public string popText;  //The text the player has to type to destroy this popup
    public bool topLayer;   //Boolean for if this popup is in the active layer the player interacts with

    // Start is called before the first frame update
    void Start()
    {
        //Assign popup text and set visualization
        //popText = "example";
        this.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = popText;

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
