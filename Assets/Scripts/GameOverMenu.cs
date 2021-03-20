using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
        //Application.Quit();

        //Cute message for now because why not?
        GameObject.Find("MainMenuLogo").GetComponent<TextMeshProUGUI>().text = "You cannot quit TYPERSPACE";
    }

    public void Credits()
    {
        //Go to credits scene
        SceneManager.LoadScene("CreditsScene");
    }
}
