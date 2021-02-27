using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerController : MonoBehaviour
{
    public float timeLimit;         //How much time the player has from start to finish
    public float timer;             //How much time the player currently has left (starts equal to timeLimit)
    public TextMeshProUGUI text;    //The text readout

    public GameObject gameOverHandler;//The object that handles ending the game

    // Start is called before the first frame update
    void Start()
    {
        timeLimit = 10;                                         //Set time limit
        timer = timeLimit;                                      //Set starting timer value

        text = this.gameObject.GetComponent<TextMeshProUGUI>(); //Get text component
        text.text = timeLimit.ToString();                       //Assign starting time to text readout

        gameOverHandler = GameObject.Find("Win/Loss Handler");  //Assign game over handler
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;                                //Time goes down
        text.text = timer.ToString();                           //Output time left to text readout

        if (timer <= 0)                                         //Once time is up, game is over
        {
            text.text = "0";
            gameOverHandler.GetComponent<GameOverController>().Defeat();
        }
    }
}
