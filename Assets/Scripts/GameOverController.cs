using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Victory()
    {
        //If player destroys all popups before the timer runs out, load the victory scene
        SceneManager.LoadScene("VictoryScene");
    }

    public void Defeat()
    {
        //If timer runs out before player wins, load the defeat scene
        SceneManager.LoadScene("DefeatScene");
    }
}
