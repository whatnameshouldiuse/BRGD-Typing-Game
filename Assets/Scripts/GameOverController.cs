using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    public float playerScore;   //The player's score as the game progresses
    public float timeLeft;      //If the player wins, this is how much time was left

    // Start is called before the first frame update
    void Start()
    {
        //Make this object survive game over so score stuff can persist to the post-game screen
        DontDestroyOnLoad(this);

        timeLeft = 0;   //If player loses, this prevents scripting issues
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //If player destroys all popups before the timer runs out, they win!
    public void Victory()
    {
        //Record how much time was left on the timer
        timeLeft = GameObject.Find("Countdown").GetComponent<TimerController>().timer;
        
        //Load the victory scene
        SceneManager.LoadScene("VictoryScene");
    }

    //If timer runs out before player wins, they lose
    public void Defeat()
    {
        //Load the defeat scene
        SceneManager.LoadScene("DefeatScene");
    }
}
