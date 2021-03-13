using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpManager : MonoBehaviour
{
    [Header("Data")]
    [Tooltip("Path to the text file of words to pull from")]
    public TextAsset WordBank;

    [Header("Spawn Properties")]
    [Tooltip("Initial number of Pop-ups present when starting the game")]
    public int StartCount = 50;
    [Tooltip("The time between each Pop-ups, in seconds, at the start of the game")]
    public float StartPopUpTime = 0.1f;
    [Tooltip("The time between each Pop-ups, in seconds")]
    public float PopUpTime = 2.0f;

    [Header("Spawn Range")]
    public float XMin = -5;
    public float YMin = -1f;
    [Space]
    public float XMax = 5;
    public float YMax = 2.5f;

    [Header("Popup Size")]
    public float MinWidth = 3;
    public float MinHeight = 3;
    [Space]
    public float MaxWidth = 7;
    public float MaxHeight = 7;

    [Header("Pop-Up Game Objects")]
    public GameObject PopUpContainer;
    public GameObject[] PopUpList;

    private string[] _wordBank;
    private bool _startRoutine = true;

    //Layer iterator to determine next popup's z coordinate (newer = closer to the screen)
    int layer = 0;

    //Audio source for spawning sound effects
    public AudioSource sound;
    public AudioClip spawnsound;

    //Background music audio source
    public AudioSource bgsound;

    void Awake() 
    {
        _wordBank = WordBank.text.Split('\n');
    }

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(StartPopulate());

        //Set time to 0 so timer doesn't start until the end of the spawning routine
        Time.timeScale = 0;

        //Set audiosources
        sound = this.gameObject.GetComponent<AudioSource>();
        bgsound = GameObject.Find("Music Handler").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_startRoutine) StopAllCoroutines();

        if (Input.GetMouseButtonDown(0) && !_startRoutine)
        {
            CreatePopUp(layer);
            layer++;
        }
    }

    private void CreatePopUp(int layerRef)
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

        spawningObject.GetComponent<PopUpController>().popText = _wordBank[Random.Range(0, _wordBank.Length)];
    }

    public void StartGame()
    {
        //Once the tutorial is complete, begin the real game from here
        StartCoroutine(StartPopulate());
    }

    IEnumerator StartPopulate()
    {
        for (int i = 0; i < StartCount; i++)
        {
            CreatePopUp(layer);
            sound.PlayOneShot(spawnsound);
            yield return new WaitForSecondsRealtime(StartPopUpTime);
            layer++;
        }
        _startRoutine = false;

        //Start the music once popups are all spawned
        bgsound.Play();

        //Send text comparison script go-ahead to tabulate popups into lists
        this.gameObject.GetComponent<TextComparison>().MakeLists();

        //Start time so the timer will start
        Time.timeScale = 1;
    }
}
