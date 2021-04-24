using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreReporter : MonoBehaviour
{
    public GameObject gameOverObject;   //The game over handler object from the previous game, persisting through scene load
    public GameOverController gameOverScript;//The controller script for the game over object

    public float score;     //The player's score
    public float timeLeft;  //The time left over

    public int diff;        //Difficulty int toggle

    public float time;      //How much time you took

    public int minLeft;             //Minutes and seconds conversions
    public int secLeft;
    public string minLeftString;
    public string secLeftString;

    // Start is called before the first frame update
    void Start()
    {
        gameOverObject = GameObject.Find("Win/Loss Handler");               //Get game over object
        gameOverScript = gameOverObject.GetComponent<GameOverController>(); //Get game over script

        score = gameOverScript.playerScore;                                 //Get score
        timeLeft = gameOverScript.timeLeft;                                 //Get time left (0 if player loses)

        diff = GameObject.Find("Difficulty Handler").GetComponent<DifficultyScript>().diff; //Get difficulty

        //If the player wins
        if(timeLeft != 0)
        {
            //Add time bonus to score
            score += (Mathf.RoundToInt(timeLeft)) * 100;
            
            //Apply difficulty modifier to score
            if (diff == 0)
            {
                score = score * 0.5f;
            }
            if (diff == 2)
            {
                score = score * 1.5f;
            }

            time = 120 - timeLeft;
            minLeft = (Mathf.FloorToInt(time / 60));               //Calculate min and sec ints from sec float readout
            minLeftString = minLeft.ToString();                    //Create strings with these ints
            secLeft = Mathf.FloorToInt(time - (minLeft * 60));
            secLeftString = secLeft.ToString();

            if (secLeft < 10)
            {
                secLeftString = "0" + secLeft;      //Add a 0 to the string if necessary for formatting
            }

            //Set score and time readouts
            GameObject.Find("Score Readout").GetComponent<TextMeshProUGUI>().text = score.ToString();
            GameObject.Find("Time Left Readout").GetComponent<TextMeshProUGUI>().text = minLeftString + ":" + secLeftString;
        }

        //If the player loses
        else
        {
            //Apply difficulty modifier to score
            if (diff == 0)
            {
                score = score * 0.5f;
            }
            if (diff == 2)
            {
                score = score * 1.5f;
            }
            
            //Set score readout
            GameObject.Find("Score Readout").GetComponent<TextMeshProUGUI>().text = "Score: " + score;
        }

        //Delete win/loss object so it doesn't duplicate later
        Destroy(gameOverObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
