using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverMenu : MonoBehaviour
{
    public int diff;                    //Difficulty toggle int
    public Sprite regButton;            //Sprite for when difficulty button is not hovered over
    public SpriteState hovButton;       //Sprite for when difficulty button is hovered over

    SpriteState ss = new SpriteState(); //Spritestate for swapping button hover graphics
    public Sprite regEasy;              //Sprites for swapping difficulty graphics
    public Sprite hovEasy;
    public Sprite regMed;
    public Sprite hovMed;
    public Sprite regHard;
    public Sprite hovHard;

    public float timer; //stopwatch value to delay difficulty button press

    public AudioSource sound;   //button sound effect source and audio clip
    public AudioClip clickdown;

    // Start is called before the first frame update
    void Start()
    {
        //Get diff
        diff = GameObject.Find("Difficulty Handler").GetComponent<DifficultyScript>().diff;

        ////Get difficulty button graphic destinations
        //regButton = GameObject.Find("MedButton").GetComponentInChildren<Image>().sprite;
        //hovButton = GameObject.Find("MedButton").GetComponent<Button>().spriteState;

        //Get audiosource
        sound = this.gameObject.GetComponent<AudioSource>();

        //Get sound effect (ripping from other game object so don't have to assign in editor a million times)
        clickdown = GameObject.Find("Cursor Handler").GetComponent<CursorController>().clickdown;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;    //update stopwatch
    }

    public void Restart()
    {
        //Destroy the game over handler object to prevent replication issues
        GameObject gameOverObject = GameObject.Find("Win/Loss Handler");
        Destroy(gameOverObject);

        //Reload the main game scene; treated as a new game
        SceneManager.LoadScene("MainScene");
    }

    public void MainMenu()
    {
        //Destroy the game over handler object to prevent replication issues
        GameObject gameOverObject = GameObject.Find("Win/Loss Handler");
        Destroy(gameOverObject);

        //Load the main menu scene
        SceneManager.LoadScene("StartMenuScene");
    }

    public void Quit()
    {
        //Quit game once game is buildable
        Application.Quit();

        //Cute message for now because why not?
        //GameObject.Find("MainMenuLogo").GetComponent<TextMeshProUGUI>().text = "You cannot quit TYPERSPACE";
    }

    public void Credits()
    {
        //Go to credits scene
        SceneManager.LoadScene("CreditsScene");
    }

    public void Easy()
    {
        //Set difficulty to easy
        GameObject.Find("Difficulty Handler").GetComponent<DifficultyScript>().diff = 0;
    }

    public void Med()
    {
        //Set difficulty to medium
        GameObject.Find("Difficulty Handler").GetComponent<DifficultyScript>().diff = 1;
    }

    public void Hard()
    {
        //Set difficulty to hard
        GameObject.Find("Difficulty Handler").GetComponent<DifficultyScript>().diff = 2;
    }

    public void Diff()
    {
        //Change difficulty and update button graphics
        if (diff == 0 && timer > 0.2)
        {
            diff = 1;
            GameObject.Find("Difficulty Handler").GetComponent<DifficultyScript>().diff = 1;
            GameObject.Find("MedButton").GetComponentInChildren<Image>().sprite = regMed;
            ss.highlightedSprite = hovMed;
            GameObject.Find("MedButton").GetComponent<Button>().spriteState = ss;
            //print("Easy to Med");
            timer = 0;

            //Play button sound effect
            sound.PlayOneShot(clickdown);

        }
        if (diff == 1 && timer > 0.2)
        {
            diff = 2;
            GameObject.Find("Difficulty Handler").GetComponent<DifficultyScript>().diff = 2;
            GameObject.Find("MedButton").GetComponentInChildren<Image>().sprite = regHard;
            ss.highlightedSprite = hovHard;
            GameObject.Find("MedButton").GetComponent<Button>().spriteState = ss;
            //print("Med to Hard");
            timer = 0;

            //Play button sound effect
            sound.PlayOneShot(clickdown);
        }
        if (diff == 2 && timer > 0.2)
        {
            diff = 0;
            GameObject.Find("Difficulty Handler").GetComponent<DifficultyScript>().diff = 0;
            GameObject.Find("MedButton").GetComponentInChildren<Image>().sprite = regEasy;
            ss.highlightedSprite = hovEasy;
            GameObject.Find("MedButton").GetComponent<Button>().spriteState = ss;
            //print("Hard to Easy");
            timer = 0;

            //Play button sound effect
            sound.PlayOneShot(clickdown);
        }
    }
}
