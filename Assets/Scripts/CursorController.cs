using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CursorController : MonoBehaviour
{
    //Audio source for button click sound effects
    public AudioSource sound;
    public AudioClip clickdown;
    public AudioClip clickup;

    //Boolean for when a button is being clicked
    bool buttonclick;

    // Start is called before the first frame update
    void Start()
    {
        //Generate an array with all objects tagged as "Cursor"
        GameObject[] dupes = GameObject.FindGameObjectsWithTag("Cursor");

        //If the array length > 1, another title music player already exists and this one should be deleted
        if (dupes.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject); //Make persistent on scene load if not deleted as a duplication

        sound = this.gameObject.GetComponent<AudioSource>();    //Get audio source for sound effects
    }

    // Update is called once per frame
    void Update()
    {
        //Play sound effects when clicking buttons
        if (Input.GetMouseButtonDown(0) && EventSystem.current.currentSelectedGameObject != null && EventSystem.current.currentSelectedGameObject.tag == "Button")
        {
            sound.PlayOneShot(clickdown);
            //buttonclick = true;
        }

        //if (Input.GetMouseButtonUp(0) && buttonclick == true)
        //{
        //    sound.PlayOneShot(clickup);
        //    buttonclick = false;
        //}
    }
}
