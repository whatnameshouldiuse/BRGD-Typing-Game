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
    //Not sure what this does but it was important in the last project so I'll keep it for now
    bool buttonclick;

    // Start is called before the first frame update
    void Start()
    {
        sound = this.gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //Play sound effects when clicking buttons
        if (Input.GetMouseButtonDown(0) && EventSystem.current.currentSelectedGameObject != null && EventSystem.current.currentSelectedGameObject.tag == "Button")
        {
            sound.PlayOneShot(clickdown);
            buttonclick = true;
        }

        if (Input.GetMouseButtonUp(0) && buttonclick == true)
        {
            sound.PlayOneShot(clickup);
            buttonclick = false;
        }
    }
}
