using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverController : MonoBehaviour
{
    public float playerScore;   //The player's score as the game progresses
    public float timeLeft;      //If the player wins, this is how much time was left

    public GameObject wizardSpeechBubble;//The wizard speech bubble and text
    public TextMeshProUGUI wizardText;

    public AudioSource bg;      //Background music

    public SpriteRenderer vicGoggle;    //The victory "goggle" animation sprite
    public Sprite[] frames;             //The frames for the victory animation

    // Start is called before the first frame update
    void Start()
    {
        //Make this object survive game over so score stuff can persist to the post-game screen
        DontDestroyOnLoad(this);

        //Get the wizard (I've always wanted to say that)
        wizardSpeechBubble = GameObject.Find("Speech Bubble Text");
        wizardText = wizardSpeechBubble.GetComponent<TextMeshProUGUI>();

        //Get audio source & victory animation
        bg = GameObject.Find("Music Handler").GetComponent<AudioSource>();
        vicGoggle = GameObject.Find("VictoryGoggle").GetComponent<SpriteRenderer>();

        timeLeft = 0;   //If player loses, this prevents scripting issues
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //If player destroys all popups before the timer runs out, they win!
    public void Victory()
    {
        //Stop the timescale to stop the timer
        Time.timeScale = 0;

        //Record how much time was left on the timer
        timeLeft = GameObject.Find("Countdown").GetComponent<TimerController>().timer;

        //Make music loop in case it expires before end of scene
        bg.loop = true;

        //Start wizard's victory speech
        StartCoroutine(VicSpeech());
        
        //Load the victory scene
        //SceneManager.LoadScene("VictoryScene");
    }

    //If timer runs out before player wins, they lose
    public void Defeat()
    {
        //Load the defeat scene
        SceneManager.LoadScene("DefeatScene");
    }

    //Coroutine for wizard speech bubbles after winning
    public IEnumerator VicSpeech()
    {
        //Move speech bubble and do last few speech bubbles
        wizardSpeechBubble.transform.position = new Vector3(wizardSpeechBubble.transform.position.x - 10000, wizardSpeechBubble.transform.position.y, wizardSpeechBubble.transform.position.z);
        wizardText.text = "Phew! Glad that's over.";
        yield return new WaitForSecondsRealtime(3);
        wizardText.text = "So like I said, enjoy your browsing experience! Peace out.";
        yield return new WaitForSecondsRealtime(3);
        //Delete popup-related stuff to allow the player to resume normal browsing (supposedly)
        Destroy(wizardSpeechBubble);
        Destroy(GameObject.Find("Typing Window"));
        Destroy(GameObject.Find("Countdown"));
        Destroy(GameObject.Find("Wizard"));
        yield return new WaitForSecondsRealtime(2);

        //Start victory animation
        Color tmp = vicGoggle.color;
        tmp.a = 1f;
        vicGoggle.color = tmp;
        yield return new WaitForSecondsRealtime(0.5f);
        vicGoggle.sprite = frames[1];
        yield return new WaitForSecondsRealtime(0.5f);
        vicGoggle.sprite = frames[2];
        yield return new WaitForSecondsRealtime(0.5f);
        vicGoggle.sprite = frames[3];
        yield return new WaitForSecondsRealtime(0.5f);
        vicGoggle.sprite = frames[4];
        yield return new WaitForSecondsRealtime(0.5f);
        vicGoggle.sprite = frames[5];
        yield return new WaitForSecondsRealtime(0.5f);
        vicGoggle.sprite = frames[6];
        yield return new WaitForSecondsRealtime(0.5f);
        vicGoggle.sprite = frames[7];
        yield return new WaitForSecondsRealtime(0.5f);
        vicGoggle.sprite = frames[8];
        yield return new WaitForSecondsRealtime(2);
        vicGoggle.sprite = frames[9];
        yield return new WaitForSecondsRealtime(4);

        //Load victory scene
        SceneManager.LoadScene("VictoryScene");
    }
}