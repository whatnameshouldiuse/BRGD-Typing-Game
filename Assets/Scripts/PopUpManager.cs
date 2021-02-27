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
    public int StartCount = 6;
    [Tooltip("The time between each Pop-ups, in seconds, at the start of the game")]
    public float StartPopUpTime = 0.1f;
    [Tooltip("The time between each Pop-ups, in seconds")]
    public float PopUpTime = 2.0f;

    [Header("Spawn Range")]
    public float XMin = -5;
    public float YMin = -5;
    [Space]
    public float XMax = 5;
    public float YMax = 5;

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

    void Awake() 
    {
        _wordBank = WordBank.text.Split('\n');
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartPopulate());
    }

    // Update is called once per frame
    void Update()
    {
        if (!_startRoutine) StopAllCoroutines();

        if (Input.GetMouseButtonDown(0) && !_startRoutine)
        {
            CreatePopUp();
        }
    }

    private void CreatePopUp()
    {
        GameObject spawningObject_prefab = PopUpList[Random.Range(0, PopUpList.Length)];

        GameObject spawningObject = Instantiate(spawningObject_prefab);
        spawningObject.transform.SetParent(PopUpContainer.transform);

        spawningObject.transform.position = new Vector3(Random.Range(XMin, XMax), Random.Range(YMin, YMax), 0);
        spawningObject.GetComponent<PopUpController>().popText = _wordBank[Random.Range(0, _wordBank.Length)];
    }

    IEnumerator StartPopulate()
    {
        for (int i = 0; i < StartCount; i++)
        {
            CreatePopUp();
            yield return new WaitForSecondsRealtime(StartPopUpTime);
        }
        _startRoutine = false;
    }
}
