using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextComparison : MonoBehaviour
{
    public List<GameObject> allPops = new List<GameObject>(); //List for all popups in level
    public List<GameObject> activePops = new List<GameObject>(); //List for all popups currently active/destroyable

    public GameObject feedback; //The feedback text box (probably to be deleted/changed later)
    public GameObject gameOverObject;//The handler for ending the game and scene management

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

        gameOverObject = GameObject.Find("Win/Loss Handler");//Assign game over handler
    }

    // Update is called once per frame
    void Update()
    {
        //When the game runs out of popups to destroy, the player wins!
        if (allPops.Count == 0)
        {
            gameOverObject.GetComponent<GameOverController>().Victory();
        }
    }

    public void CompareText(string playerText)
    {
        GameObject winner = activePops[0];  //The popup with the closest match; initialize with first in list
        float highScore = 0;    //The score of the winning popup; initialize with 0 (doesn't matter, first in list will override)

        foreach (GameObject g in activePops)
        {
            float hits = 0;       //How many correct characters
            float miss = 0;       //How mnay wrong characters
            float score = 0;    //The overall score for the popup
            string newText = playerText;    //A separate instance of the input text for modifying

            //Get the popup's text string
            string popString = g.GetComponent<PopUpController>().popText;

            //Add characters to make both strings the same length to prevent out-of-index
            int charDiff = popString.Length - newText.Length;
            if (charDiff > 0)
            {
                for(int i = 0; i < charDiff; i++)
                {
                    newText = newText + "~";
                }
            }
            if (charDiff < 0)
            {
                for (int i = 0; i > charDiff; i--)
                {
                    popString = popString + "~";
                }
            }

            //print(newText);
            //print(popString);

            //Break up the string and compare character-for-character
            for (int i = 0; i < popString.Length; i++)
            {
                if (popString[i] == newText[i])
                {
                    hits++;
                    //print("hit");
                }
                else
                {
                    miss++;
                    //print("miss");
                }
            }

            //Calculate score
            score = hits / (hits + miss);
            print(score);

            //If score is highest so far, make this the selected popup and record the score for next comparison
            if(score > highScore)
            {
                highScore = score;
                winner = g;
            }
        }

        //Feedback for the player based on score
        if(highScore < 0.6)
        {
            feedback.GetComponent<TextMeshProUGUI>().text = "Miss!";
        }
        if (highScore >= 0.6 && highScore < 0.8)
        {
            feedback.GetComponent<TextMeshProUGUI>().text = "Okay!";
        }
        if (highScore >= 0.8 && highScore < 1)
        {
            feedback.GetComponent<TextMeshProUGUI>().text = "Great!";
        }
        if (highScore == 1)
        {
            feedback.GetComponent<TextMeshProUGUI>().text = "Perfect!";
        }

        //If the winner had a high enough score, it counts and the popup is destroyed
        if (highScore > 0.6)
        {
            activePops.Remove(winner);
            allPops.Remove(winner);
            winner.GetComponent<PopUpController>().Winner();
        }
    }
}
