using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialController : MonoBehaviour
{
    public string popText;  //The text the player has to type to destroy this popup
    public bool topLayer;   //Boolean for if this popup is in the active layer the player interacts with
    public float score;     //The base score value for this popup's word

    public bool tutorial;   //Boolean for it the game is still in tutorial mode

    public GameObject popupHandler;     //The popup handler object
    public PopUpManager managerScript;  //The spawning script for the main game

    // Start is called before the first frame update
    void Start()
    {
        //Set boolean
        tutorial = true;

        //Tutorial text
        popText = "start";

        //Assign popup text and set visualization
        this.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "Type \"" + popText + "\" to start!";

        //Make this popup active (for now, until spawning/handling is finished)
        topLayer = true;

        //Set the popup's score (for now, until scoring is fleshed out)
        score = 0;

        //Get popup handler and spawning script
        popupHandler = GameObject.Find("Popup Handler");
        managerScript = popupHandler.GetComponent<PopUpManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //When the player successfully destroys the popup, this method runs
    public void Winner()
    {
        //Set tutorial boolean to false
        tutorial = false;

        //Start the spawning routine for the main game
        managerScript.StartGame();

        //Once score is handled, delete this object once and for all
        Destroy(this.gameObject);
    }
}
