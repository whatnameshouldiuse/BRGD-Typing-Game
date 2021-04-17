using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyScript : MonoBehaviour
{
    public int diff;    //Difficulty toggle: 0 = easy, 1 = medium, 2 = hard

    // Start is called before the first frame update
    void Start()
    {
        //Default difficulty: medium
        diff = 1;

        //Generate an array with all objects tagged as "TitleMusic"
        GameObject[] dupes = GameObject.FindGameObjectsWithTag("Difficulty");

        //If the array length > 1, another title music player already exists and this one should be deleted
        if (dupes.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject); //Make persistent on scene load if not deleted as a duplication
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
