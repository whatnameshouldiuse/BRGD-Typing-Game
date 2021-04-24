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
    public bool isWinner;   //Boolean for if the popup is getting deleted
    public bool deleted;    //Boolean for if the tutorial popup has already been deleted

    public GameObject popupHandler;     //The popup handler object
    public PopUpManager managerScript;  //The spawning script for the main game

    public GameObject windowBar;        //Children objects of the tutorial handler object
    public GameObject popupSprite;

    public GameObject wizardSpeechBubble;//The wizard speech bubble and text
    public TextMeshProUGUI wizardText;

    public Vector2 size;    //Scaling vectors for popup
    public Vector2 spSize;
    public Vector2 sizeMod; //Change vector for popup's scaling

    public GameObject titleMusic;       //The audio player object for the title music

    // Start is called before the first frame update
    void Start()
    {
        //Set boolean
        tutorial = true;

        //Tutorial text
        popText = "start";

        //Assign popup text and set visualization
        this.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "start";

        //Make this popup active (for now, until spawning/handling is finished)
        topLayer = true;

        //Set the popup's score (for now, until scoring is fleshed out)
        score = 0;

        //Get popup handler and spawning script
        popupHandler = GameObject.Find("Popup Handler");
        managerScript = popupHandler.GetComponent<PopUpManager>();

        //Get title music player
        titleMusic = GameObject.Find("TitleMusic");

        //Get children objects
        windowBar = transform.Find("Window Bar").gameObject;
        popupSprite = transform.Find("Sprite").gameObject;

        //Get the wizard (I've always wanted to say that)
        wizardSpeechBubble = GameObject.Find("Speech Bubble Text");
        wizardText = wizardSpeechBubble.GetComponent<TextMeshProUGUI>();

        //Set scaling value for tutorial popup graphics
        size = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        //If the popup is being deleted
        if (isWinner && transform.localScale.x > 0)
        {
            sizeMod += new Vector2(Time.deltaTime, Time.deltaTime);
            transform.localScale = size - sizeMod;
        }

        if (isWinner && transform.localScale.x <= 0 && deleted == false)
        {
            //Move tutorial popup graphics off-screen once animation is played out
            transform.position = new Vector3(10000, 10000, 0);
            deleted = true;
        }
    }

    //When the player successfully destroys the popup, this method runs; treat as State 05
    public void Winner()
    {
        //Set tutorial boolean to false
        tutorial = false;

        //Set isWinner to true to start deletion animation
        isWinner = true;

        //Start the speech bubble progression (second coroutine starts via PopupManager script)
        StartCoroutine(TutSpeech1());
    }

    //Coroutine for wizard speech bubbles (part 1, before popups spawn in)
    public IEnumerator TutSpeech1()
    {
        wizardText.text = "Excellent! Hopefully you will never need this arcane knowledge.";
        yield return new WaitForSecondsRealtime(3);
        wizardText.text = "Now, go forth and enjoy your browsing experience!";
        yield return new WaitForSecondsRealtime(3);
        wizardText.text = "Wait... do you hear that? In the distance...";
        yield return new WaitForSecondsRealtime(3);
        managerScript.StartGame();
        titleMusic.GetComponent<TitleMusicHandler>().StopMusic();
        wizardText.text = "OH DEAR GOD NO";
    }

    //Middleman function to start the second speech bubble coroutine; otherwise the popup manager script's own coroutine
    //will terminate on the next frame, terminating this coroutine with it
    public void TutSpeech2start()
    {
        StartCoroutine(TutSpeech2());
    }

    //Coroutine for wizard speech bubbles (part 2, after popups spawn in)
    public IEnumerator TutSpeech2()
    {
        wizardText.text = "Quick! Delete the popups before they crash your computer!";
        yield return new WaitForSecondsRealtime(3);
        wizardText.text = "I'll be over here, grooving to the beat.";
        yield return new WaitForSecondsRealtime(3);
        //Enable feedback in TextComparison script
        popupHandler.GetComponent<TextComparison>().fdbready = true;
        //Move speech bubble until it's needed again
        wizardSpeechBubble.transform.position = new Vector3 (wizardSpeechBubble.transform.position.x + 10000, wizardSpeechBubble.transform.position.y, wizardSpeechBubble.transform.position.z);
        Destroy(this.gameObject);
    }
}