using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopUpController : MonoBehaviour
{
    public string popText;  //The text the player has to type to destroy this popup
    public bool topLayer;   //Boolean for if this popup is in the active layer the player interacts with
    public float score;     //The base score value for this popup's word

    // Start is called before the first frame update
    void Start()
    {
        //Assign popup text and set visualization
        //popText = "example";
        this.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = popText;

        //Make this popup active (for now, until spawning/handling is finished)
        topLayer = true;

        //Set the popup's score (for now, until scoring is fleshed out)
        score = 100;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //When the player successfully destroys the popup, this method runs
    public void Winner(float scoreMod)
    {
        //Calculate the points the player earns and add them to the score total
        GameObject.Find("Win/Loss Handler").GetComponent<GameOverController>().playerScore += (score * scoreMod);

        //Calculate score and feed back into player's score
        Destroy(this.gameObject);
    }
}
