using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerTextTyper : MonoBehaviour
{
    public string textTyped;        //The text typed by the player
    public string finalText;        //The finished text output by the player by pressing enter

    public TMP_InputField inputBox; //The text input field
    public TextMeshProUGUI textBox; //The text display for the player's input

    public GameObject popHandler;           //The popup handler object
    public TextComparison handlerScript;    //The script for comparing player text to popup text

    public GameObject tutorialPopup;            //The tutorial popup object
    public TutorialController tutorialScript;   //The script for managing the tutorial

    // Start is called before the first frame update
    void Start()
    {
        textBox = this.gameObject.GetComponentInChildren<TextMeshProUGUI>();    //Get text box
        inputBox = this.gameObject.GetComponentInChildren<TMP_InputField>();    //Get input field
        textBox.text = "";                                                      //Empty output text box
        inputBox.text = "";                                                     //Empty input field
        inputBox.ActivateInputField();                                          //Activate text input without clicking

        popHandler = GameObject.Find("Popup Handler");                          //Get handler object for popups
        handlerScript = popHandler.GetComponent<TextComparison>();              //Get script for popup handler

        tutorialPopup = GameObject.Find("TutorialPopup");                       //Get tutorial object
        tutorialScript = tutorialPopup.GetComponent<TutorialController>();      //Get tutorial script
    }

    // Update is called once per frame
    void Update()
    {
        //Re-activate the input field if the player clicked off of it for some reason
        //We'll want the input field hidden eventually, so the player couldn't re-activate it on their own
        inputBox.ActivateInputField();

        //If a character was entered into the input field, copy it over to the output text via strings
        //Delete character from input field afterward
        //As a result, "inputBox.text.ToString()" gives us the most recent player input on a per-character basis
        //We can then script around the input character using the "textTyped" string in any other script
        //if (inputBox.text != "")
        //{
        //    textTyped = inputBox.text.ToString();

        //    //To make output field print all text typed per-character
        //    textBox.text = textBox.text.ToString() + textTyped;

        //    //To make output field print only the latest character
        //    //textBox.text = textTyped;

        //    //Reset input field
        //    inputBox.text = "";
        //}

        //Alternatively, just make the input box the input; this allows backspacing
        //This removes per-character implementation but is acceptable for full-word submission implementations
        textBox.text = inputBox.text;

        //If the player presses enter, package and deliver current typed text for other scripts to check against
        if (Input.GetKeyDown(KeyCode.Return))
        {
            //Package input for delivery to text comparison script
            finalText = textBox.text.ToString();

            //Empty input field for next player input
            inputBox.text = "";

            if (tutorialScript.tutorial == true)
            {
                //Run modified text comparison in TextComparison
                handlerScript.TutorialCompare2(finalText);
            }
            else
            {
                //Run text comparison with packaged input text as parameter
                handlerScript.CompareText2(finalText);
            }

        }
    }
}