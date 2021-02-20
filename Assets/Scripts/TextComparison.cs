using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextComparison : MonoBehaviour
{
    public List<GameObject> allPops = new List<GameObject>(); //List for all popups in level
    public List<GameObject> activePops = new List<GameObject>(); //List for all popups currently active/destroyable

    //public string playerText;   //The text input by the player

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

    public void CompareText(string playerText)
    {
        GameObject winner = activePops[0];  //The popup with the closest match; initialize with first in list
        float highScore = 0;    //The score of the winning popup; initialize with 0 (doesn't matter, first in list will override)

        foreach (GameObject g in activePops)
        {
            int hits = 0;       //How many correct characters
            int miss = 0;       //How mnay wrong characters
            float score = 0;    //The overall score for the popup

            //Get the popup's text string
            string popString = g.GetComponent<PopUpController>().popText;

            //Break up the string and compare character-for-character
            for (int i = 0; i < popString.Length; i++)
            {
                if (popString[i] == playerText[i])
                {
                    hits++;
                }
                else
                {
                    miss++;
                }
            }

            //Calculate score
            score = hits / (hits + miss);

            //If score is highest so far, make this the selected popup and record the score for next comparison
            if(score > highScore)
            {
                highScore = score;
                winner = g;
            }
        }

        //If the winner had a high enough score, it counts and the popup is destroyed
        if(highScore > 0.6)
        {
            winner.GetComponent<PopUpController>().Winner();
        }
    }
}
