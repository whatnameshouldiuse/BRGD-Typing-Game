using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Typerspace;

public class LeaderboardScoreEntry : MonoBehaviour
{
    public int EntryNumber; //1 through 10
    public TextMeshProUGUI _entryName;
    public TextMeshProUGUI _entryScore;

    // Start is called before the first frame update
    void Start()
    {
        Leaderboard.ScoreEntry entry = Leaderboard.GetEntry(EntryNumber-1);

        _entryName.text = EntryNumber + ". " + entry.name;
        _entryScore.text = entry.score.ToString();
    }
}