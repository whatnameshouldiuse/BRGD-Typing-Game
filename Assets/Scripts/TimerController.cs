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
    public Sprite clockFace;        //The countdown clock face
    public GameObject clockHandHolder;  //The countdown clock hand's empty holder parent object (required to spin hand correctly)

    public GameObject gameOverHandler;//The object that handles ending the game

    public Vector3 oldRot;          //The previous frame's clock hand rotation
    public Vector3 newRot;          //The current frame's eventual clock hand rotation

    public int minLeft;             //Minutes and seconds left
    public int secLeft;
    public string minLeftString;
    public string secLeftString;

    public bool timeGo;             //Boolean for if the timer should start

    // Start is called before the first frame update
    void Start()
    {
        timeLimit = 120;                                        //Set time limit
        timer = timeLimit;                                      //Set starting timer value

        text = this.gameObject.GetComponentInChildren<TextMeshProUGUI>(); //Get text component
        text.text = "02:00";                                   //Assign starting time to text readout; assuming 120 seconds

        gameOverHandler = GameObject.Find("Win/Loss Handler");  //Assign game over handler
        clockHandHolder = GameObject.Find("clock hand holder"); //Assign clock hand holder
    }

    // Update is called once per frame
    void Update()
    {
        if (timeGo == true)
        {
            timer -= Time.deltaTime;                                //Time goes down
            minLeft = (Mathf.FloorToInt(timer / 60));               //Calculate min and sec ints from sec float readout
            minLeftString = minLeft.ToString();                     //Create strings with these ints
            secLeft = Mathf.FloorToInt(timer - (minLeft * 60));
            secLeftString = secLeft.ToString();

            if (secLeft < 10)
            {
                secLeftString = "0" + secLeft;      //Add a 0 to the string if necessary for formatting
            }

            text.text = "0" + minLeftString + ":" + secLeftString;  //Output time left to text readout

            if (timer <= 0)                                         //Once time is up, game is over
            {
                text.text = "00:00";                                                       //Hard-code timer readout
                clockHandHolder.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));//Hard-code clock hand positioning
                gameOverHandler.GetComponent<GameOverController>().Defeat();                //Initiate defeat methods
            }

            //Since the game lasts 120 seconds currently, we rotate at 3 * deltaT so that 3 * 120 = 360
            oldRot = clockHandHolder.transform.rotation.eulerAngles;      //Get previous frame's clock hand rotation
            newRot.z = oldRot.z - (Time.deltaTime * 3);                   //Change rotation as function of time left
            clockHandHolder.transform.rotation = Quaternion.Euler(newRot);//Assign new rotation
        }
    }
}
