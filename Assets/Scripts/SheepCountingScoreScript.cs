using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SheepCounting
{
    //public class SheepCountingScoringScript : MonoBehaviour
    //{
    //    public GameObject ScoreHandler;
    //    public ScoreHandler ScoringScript;
    //    public GameObject scorereadout;
    //    public GameObject time;
    //    public GameObject fakesheep;
    //    public GameObject realsheep;

    //    // Leaderboard
    //    private GUIStyle guiStyle = new GUIStyle();
    //    public SortedList<float, string> localPastScores;
    //    public int localNumOfLeaderboardPlayers;
    //    public int maxNumOfLeaderboard;
    //    public Font font;

    //    //The fade from black image for the scene
    //    public Image darkScreen;
    //    Color fadeColor;

    //    //Bool to "turn off" update disabling of dark screen
    //    public bool disable;

    //    public GameObject NameInputObj;
    //    float newTime;

    //    // Start is called before the first frame update
    //    void Start()
    //    {
    //        ScoreHandler = GameObject.Find("ScoreHandler");
    //        scorereadout = this.gameObject;
    //        //time = scorereadout.transform.GetChild(0);
    //        //this.gameObject.TimeReadout.text = ScoreHandler.timer;

    //        ScoringScript = ScoreHandler.GetComponent<ScoreHandler>();
    //        newTime = ScoringScript.timer;

    //        //Assign dark screen's color
    //        fadeColor = darkScreen.GetComponent<Image>().color;

    //        //Assign boolean
    //        disable = false;

    //        // Gui style 
    //        guiStyle.fontSize = 32; //change the font size
    //        guiStyle.normal.textColor = Color.black; //change the font color
    //        guiStyle.alignment = TextAnchor.UpperCenter;
    //        guiStyle.font = font;

    //        // Leaderboard
    //        localPastScores = GlobalControl.Instance.pastScores;
    //        localNumOfLeaderboardPlayers = GlobalControl.Instance.numOfLeaderboardPlayers;
    //        maxNumOfLeaderboard = GlobalControl.maxNumOfLeaderboard;

    //        // InputField
    //        NameInputObj = GameObject.Find("NameInput");
    //        InputField nameInput = NameInputObj.GetComponent<InputField>();

    //        // New high score
    //        if (localNumOfLeaderboardPlayers < maxNumOfLeaderboard || localPastScores.Keys[maxNumOfLeaderboard - 1] < newTime){
    //            NameInputObj.SetActive(true);
    //            var se = new InputField.SubmitEvent();
    //            se.AddListener(SubmitName);
    //            nameInput.onEndEdit = se;
    //        }
    //        else
    //        {
    //            NameInputObj.SetActive(false);
    //        }
    //    }

    //    private void SubmitName(string username)
    //    {
    //        if (username != "")
    //        {
    //            string formattedUsername;
    //            int strLength = username.Length;

    //                string spacing = "";
    //                formattedUsername = spacing + username;
    //            addNewScore(newTime, formattedUsername);
    //            NameInputObj.SetActive(false);
    //        }
    //    }

    //    void OnGUI()
    //    {
    //        List<string> rankingTitles = new List<string>(new string[] { 
    //                " 1st", " 2nd", " 3rd", " 4th", " 5th", " 6th", " 7th", " 8th", " 9th", "10th"
    //                });

    //        int xDelta = 60;
    //        int yDelta = 430;

    //        GUI.Label(new Rect(150 + xDelta, yDelta, 100, 40), "NAME", guiStyle);
    //        GUI.Label(new Rect(350 + xDelta, yDelta, 100, 40), "SCORE", guiStyle);
    //        for(int i = 0; i < localNumOfLeaderboardPlayers; i++)
    //        {
    //            GUI.Label(new Rect(150 + xDelta, yDelta + 50 + i * 40, 100, 40), localPastScores.Values[i], guiStyle);
    //            GUI.Label(new Rect(350 + xDelta, yDelta + 50 + i * 40, 100, 40), localPastScores.Keys[i].ToString("#.000"), guiStyle);
    //        }
    //    }

    //    // Update is called once per frame
    //    void Update()
    //    {
    //        if (fadeColor.a > 0)
    //        {
    //            //Fade from black to transparent on the black image
    //            fadeColor.a -= 0.4f * Time.deltaTime;
    //            darkScreen.GetComponent<Image>().color = fadeColor;
    //        }
    //        else if (disable == false)
    //        {
    //            darkScreen.enabled = false;
    //            disable = true;
    //        }
    //    }

    //    void addNewScore(float time, string username)
    //    {
    //        localPastScores.Add(time, username);
    //        localNumOfLeaderboardPlayers = localPastScores.Count < maxNumOfLeaderboard ? localPastScores.Count : maxNumOfLeaderboard ;
    //        GlobalControl.updateLeaderBoard(localPastScores, localNumOfLeaderboardPlayers);
    //    }
    //}
}
