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

    }

    public void Defeat()
    {
        //If timer runs out before player wins, load the defeat scene
        SceneManager.LoadScene("DefeatScene");
    }
}
