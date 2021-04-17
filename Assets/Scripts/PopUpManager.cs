using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpManager : MonoBehaviour
{
    [Header("Data")]
    [Tooltip("Path to the text file of words to pull from")]
    public TextAsset WordBankEasy;
    public TextAsset WordBankMedium;
    public TextAsset WordBankHard;
    //public TextAsset WordBank;

    [Header("Spawn Properties")]
    [Tooltip("Initial number of Pop-ups present when starting the game")]
    public int StartCount = 50;
    [Tooltip("The time between each Pop-ups, in seconds, at the start of the game")]
    public float StartPopUpTime = 0.1f;
    [Tooltip("The time between each Pop-ups, in seconds")]
    public float PopUpTime = 2.0f;

    [Header("Spawn Range")]
    public float XMin = -5;
    public float YMin = 0f;
    [Space]
    public float XMax = 5;
    public float YMax = 2.5f;

    [Header("Popup Size")]
    public float MinWidth = 3;
    public float MinHeight = 3;
    [Space]
    public float MaxWidth = 7;
    public float MaxHeight = 7;

    //Scaling factor to make all popups slightly smaller
    public float scaleFactor;

    [Header("Pop-Up Game Objects")]
    public GameObject PopUpContainer;
    public GameObject[] PopUpList;

    private string[] _wordBank;
    private string[] _easyWords;
    private string[] _medWords;
    private string[] _hardWords;
    string[] currentBank;

    List<string> usedWords = new List<string>();    //list of strings used during spawning to prevent duplicates

    private bool _startRoutine = true;

    //Layer iterator to determine next popup's z coordinate (newer = closer to the screen)
    int layer = 0;

    //Audio source for spawning sound effects
    public AudioSource sound;
    public AudioClip spawnsound;

    //Background music audio source
    public AudioSource bgsound;
    
    //Script for animating the wizard
    public WizardAnimator wizScript;

    //Tutorial controller script
    public TutorialController tutScript;

    //Debug popup counter for printing
    int popupCount = 0;

    void Awake() 
    {
        //_wordBank = WordBank.text.Split('\n');
        _easyWords = WordBankEasy.text.Split('\n');
        _medWords  = WordBankMedium.text.Split('\n');
        _hardWords = WordBankHard.text.Split('\n');
    }

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(StartPopulate());

        //Get difficulty value from difficulty handler object
        int diff = GameObject.Find("Difficulty Handler").GetComponent<DifficultyScript>().diff;

        //Set number of popups based on difficulty selected
        if (diff == 0)
        {
            StartCount = 25;
        }

        if (diff == 1)
        {
            StartCount = 35;
        }

        if (diff == 2)
        {
            StartCount = 45;
        }

        //Set time to 0 so timer doesn't start until the end of the spawning routine
        //Time.timeScale = 0;

        //Set scale factor
        scaleFactor = 0.46f;

        //Set audiosources
        sound = this.gameObject.GetComponent<AudioSource>();
        bgsound = GameObject.Find("Music Handler").GetComponent<AudioSource>();

        //Set wizard script and tutorial script
        wizScript = GameObject.Find("Wizard").GetComponentInChildren<WizardAnimator>();
        tutScript = this.gameObject.GetComponentInChildren<TutorialController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_startRoutine) StopAllCoroutines();

        //if (Input.GetMouseButtonDown(0) && !_startRoutine)
        //{
        //    CreatePopUp(layer, _easyWords);
        //    layer++;
        //}
    }

    private void CreatePopUp(int layerRef, string[] wordBank)
    {
        //Calculate the layer of the next popup
        //Current depth range for popups: 10 - 60
        //This isn't hardcoded; just a convention to prevent layering conflicts in the scene
        //As such, try to spawn fewer than 50 popups (this can be changed to any number!)
        int layer = (60 - layerRef);

        GameObject spawningObject_prefab = PopUpList[Random.Range(0, PopUpList.Length)];

        GameObject spawningObject = Instantiate(spawningObject_prefab);
        spawningObject.transform.SetParent(PopUpContainer.transform);

        spawningObject.transform.position = new Vector3(Random.Range(XMin, XMax), Random.Range(YMin, YMax), layer);
        spawningObject.transform.localScale = spawningObject.transform.localScale * scaleFactor;

        //While loop to filter out previously-used words for this game
        bool goodToGo = false;      //bool to gatekeep whether the popup is good to go

        while(goodToGo == false)
        {
            string newText = currentBank[Random.Range(0, currentBank.Length)];  //randomly select word
            if (!usedWords.Contains(newText))
            {
                usedWords.Add(newText);                                             //If list doesn't contain this word yet, add it
                spawningObject.GetComponent<PopUpController>().popText = newText;   //Assign word to popup prefab
                goodToGo = true;                                                    //Switch bool to end loop
                popupCount++;                                                       //Iterate debug counter
            }
            //else
            //{
            //    print("Duplicate: " + newText);
            //}
            //spawningObject.GetComponent<PopUpController>().popText = currentBank[Random.Range(0, currentBank.Length)];
        }
    }

    public void StartGame()
    {
        //Once the tutorial is complete, begin the real game from here
        StartCoroutine(StartPopulate());
    }

    IEnumerator StartPopulate()
    {
        //Split popup spawns into thirds, with a different difficulty of word bank for each
        //Should be difficult at the bottom of the pile, then medium, then easy on top of the pile
        for (int i = 0; i < StartCount; i++)
        {
            if(i < (StartCount / 3))
            {
                currentBank = _hardWords;
            }
            if(i >= (StartCount / 3) && i < (StartCount / 1.5))
            {
                currentBank = _medWords;
            }
            if(i >= (StartCount / 1.5))
            {
                currentBank = _easyWords;
            }
            CreatePopUp(layer, currentBank);
            sound.PlayOneShot(spawnsound);
            yield return new WaitForSecondsRealtime(StartPopUpTime);
            layer++;
        }
        _startRoutine = false;

        //Start the music once popups are all spawned
        bgsound.Play();

        //Set how many ads need to be destroyed in the Text Comparison script
        this.gameObject.GetComponent<TextComparison>().adsLeft = StartCount;

        //Once the music starts, start the wizard animation on the same beat
        wizScript.isBopping = true;

        //Send text comparison script go-ahead to tabulate popups into lists
        this.gameObject.GetComponent<TextComparison>().MakeLists();

        //Start second wizard speech bubble coroutine
        //Have to use a middle-man function to keep coroutine from terminating after this one terminates
        tutScript.TutSpeech2start();

        //Start time so the timer will start
        //Time.timeScale = 1;
        GameObject.Find("Countdown").GetComponent<TimerController>().timeGo = true;

        //Print debug to console: how many popups spawned
        //print(popupCount + " popups spawned");
    }
}
