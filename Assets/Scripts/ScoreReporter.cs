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
    public float time;      //The time left over

    // Start is called before the first frame update
    void Start()
    {
        gameOverObject = GameObject.Find("Win/Loss Handler");               //Get game over object
        gameOverScript = gameOverObject.GetComponent<GameOverController>(); //Get game over script

        score = gameOverScript.playerScore;                                 //Get score
        time = gameOverScript.timeLeft;                                     //Get time left (0 if player loses)

        //If the player wins
        if(time != 0)
        {
            //Add time bonus to score
            score += (Mathf.RoundToInt(time)) * 100;

            //Set score and time readouts
            GameObject.Find("Score Readout").GetComponent<TextMeshProUGUI>().text = "Score: " + score;
            GameObject.Find("Time Left Readout").GetComponent<TextMeshProUGUI>().text = "Time left: " + time + " seconds!";
        }

        //If the player loses
        else
        {
            //Set score readout
            GameObject.Find("Score Readout").GetComponent<TextMeshProUGUI>().text = "Score: " + score;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
