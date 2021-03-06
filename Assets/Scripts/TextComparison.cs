using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class TextComparison : MonoBehaviour
{
    public List<GameObject> allPops = new List<GameObject>(); //List for all popups in level
    public List<GameObject> activePops = new List<GameObject>(); //List for all popups currently active/destroyable

    public GameObject feedback; //The feedback text box (probably to be deleted/changed later)
    public GameObject gameOverObject;//The handler for ending the game and scene management

    public int adsLeft;         //How many ads are left in the game

    public GameObject tutorialPopup;            //The tutorial popup object
    public TutorialController tutorialScript;   //The script for managing the tutorial



    //public string playerText;   //The text input by the player

    // Start is called before the first frame update
    void Start()
    {
        gameOverObject = GameObject.Find("Win/Loss Handler");               //Assign game over handler
        adsLeft = this.gameObject.GetComponent<PopUpManager>().StartCount;  //Set how many ads are left from starting spawn count

        tutorialPopup = GameObject.Find("TutorialPopup");                       //Get tutorial object
        tutorialScript = tutorialPopup.GetComponent<TutorialController>();      //Get tutorial script
    }

    // Update is called once per frame
    void Update()
    {
        //When the game runs out of popups to destroy, the player wins!
        if (adsLeft == 0)
        {
            gameOverObject.GetComponent<GameOverController>().Victory();
        }
    }

    public void MakeLists()
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
            if (g.GetComponent<PopUpController>().topLayer == true)
            {
                activePops.Add(g);
            }
        }
    }

    //The original text comparison algorithm improvised by Toby
    public void CompareText(string playerText)
    {
        GameObject winner = activePops[0];  //The popup with the closest match; initialize with first in list
        float highScore = 0;    //The score of the winning popup; initialize with 0 (doesn't matter, first in list will override)
        float scoremod = 0;     //The score modifier if the player successfully destroys a popup
        float oldLayer = 100;     //Layer variable for the current winner of the loop; lower z coordinates are incentivized

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
            //print(score);

            //If score is highest so far, make this the selected popup and record the score for next comparison
            //Also check layering so hidden popups don't get selected instead
            if(score >= highScore && g.transform.position.z < oldLayer)
            {
                highScore = score;
                winner = g;
                oldLayer = g.transform.position.z;
            }
        }

        //Feedback for the player based on score and score modifier assignment
        if(highScore < 0.6)
        {
            feedback.GetComponent<TextMeshProUGUI>().text = "Miss!";
        }
        if (highScore >= 0.6 && highScore < 0.8)
        {
            feedback.GetComponent<TextMeshProUGUI>().text = "Okay!";
            scoremod = 0.6f;
        }
        if (highScore >= 0.8 && highScore < 1)
        {
            feedback.GetComponent<TextMeshProUGUI>().text = "Great!";
            scoremod = 0.8f;
        }
        if (highScore == 1)
        {
            feedback.GetComponent<TextMeshProUGUI>().text = "Perfect!";
            scoremod = 1f;
        }

        //If the winner had a high enough score, it counts and the popup is destroyed
        if (highScore >= 0.6)
        {
            activePops.Remove(winner);
            allPops.Remove(winner);
            winner.GetComponent<PopUpController>().Winner(scoremod);
        }
    }

    //The second attempt at a text comparison algorithm, using Damerau-Levenshtein
    public void CompareText2(string playerText)
    {
        GameObject winner = activePops[0];  //The popup with the closest match; initialize with first in list
        int highScore = 999;    //The score of the winning popup; initialize with 999 so the first score overrides
        float scoremod = 0;     //The score modifier if the player successfully destroys a popup
        float oldLayer = 100;     //Layer variable for the current winner of the loop; lower z coordinates are incentivized

        foreach (GameObject g in activePops)
        {
            //Get the popup's text string
            string popString = g.GetComponent<PopUpController>().popText;

            //Run Damerau-Levenshtein comparing the player string with the popup string
            int score = DamerauLevenshtein(playerText, popString);

            //If score is best so far, make this the selected popup and record the score for next comparison
            //Also check layering so hidden popups don't get selected instead
            if (score <= highScore && g.transform.position.z < oldLayer)
            {
                highScore = score;
                winner = g;
                oldLayer = g.transform.position.z;
            }
        }

        print(highScore);
        print(winner.GetComponent<PopUpController>().popText);
        print(playerText);

        //Feedback for the player based on score and score modifier assignment
        if (highScore >= 4)
        {
            feedback.GetComponent<TextMeshProUGUI>().text = "Miss!";
        }
        if (highScore == 3)
        {
            feedback.GetComponent<TextMeshProUGUI>().text = "Okay!";
            scoremod = 0.6f;
        }
        if (highScore == 2)
        {
            feedback.GetComponent<TextMeshProUGUI>().text = "Great!";
            scoremod = 0.8f;
        }
        if (highScore <= 1)
        {
            feedback.GetComponent<TextMeshProUGUI>().text = "Perfect!";
            scoremod = 1f;
        }

        //If the winner had a good enough score, it counts and the popup is destroyed
        if (highScore < 3)
        {
            activePops.Remove(winner);
            allPops.Remove(winner);
            winner.GetComponent<PopUpController>().Winner(scoremod);
        }
    }

    //A ripped-off implementation of the Damerau-Levenshtein string comparison algorithm
    int DamerauLevenshtein(string input1, string input2)
    {
        string string1 = input1;
        string string2 = input2;

        if (String.IsNullOrEmpty(string1))
        {
            if (!String.IsNullOrEmpty(string2))
                return string2.Length;

            return 0;
        }

        if (String.IsNullOrEmpty(string2))
        {
            if (!String.IsNullOrEmpty(string1))
                return string1.Length;

            return 0;
        }

        int length1 = string1.Length;
        int length2 = string2.Length;

        int[,] d = new int[length1 + 1, length2 + 1];

        int cost, del, ins, sub;

        for (int i = 0; i <= d.GetUpperBound(0); i++)
            d[i, 0] = i;

        for (int i = 0; i <= d.GetUpperBound(1); i++)
            d[0, i] = i;

        for (int i = 1; i <= d.GetUpperBound(0); i++)
        {
            for (int j = 1; j <= d.GetUpperBound(1); j++)
            {
                if (string1[i - 1] == string2[j - 1])
                    cost = 0;
                else
                    cost = 1;

                del = d[i - 1, j] + 1;
                ins = d[i, j - 1] + 1;
                sub = d[i - 1, j - 1] + cost;

                d[i, j] = Math.Min(del, Math.Min(ins, sub));

                if (i > 1 && j > 1 && string1[i - 1] == string2[j - 2] && string1[i - 2] == string2[j - 1])
                    d[i, j] = Math.Min(d[i, j], d[i - 2, j - 2] + cost);
            }
        }

        return d[d.GetUpperBound(0), d.GetUpperBound(1)];
    }

    public void TutorialCompare(string playerText)
    {
        float hits = 0;         //How many correct characters
        float miss = 0;         //How mnay wrong characters
        float score = 0;        //The overall score for the popup
        string newText = playerText;    //A separate instance of the input text for modifying

        //Get the popup's text string
        string popString = tutorialScript.popText;

        //Add characters to make both strings the same length to prevent out-of-index
        int charDiff = popString.Length - newText.Length;
        if (charDiff > 0)
        {
            for (int i = 0; i < charDiff; i++)
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
        //print(score);

        //If the winner had a high enough score, it counts and the popup is destroyed
        if (score == 1)
        {
            tutorialScript.Winner();
        }
    }

    public void TutorialCompare2(string playerText)
    {
        //If the winner had a high enough score, it counts and the popup is destroyed
        if (playerText == tutorialScript.popText)
        {
            tutorialScript.Winner();
        }
    }
}
