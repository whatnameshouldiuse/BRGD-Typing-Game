using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Typerspace;

public class PlayerPrefLeaderboardCreate : MonoBehaviour
{
    void Start()
    {
        int tenthScore = PlayerPrefs.GetInt("leaderboard[9].score", 0);
        if (tenthScore == 0)
        {
            Leaderboard.Record("Barbie", 1000);
            Leaderboard.Record("Britney", 900);
            Leaderboard.Record("Clippy", 800);
            Leaderboard.Record("Doctor", 700);
            Leaderboard.Record("Dramagotchi", 600);
            Leaderboard.Record("Psychiatrist", 500);
            Leaderboard.Record("Ratz", 400);
            Leaderboard.Record("Slinky", 300);
            Leaderboard.Record("Snerf", 200);
            Leaderboard.Record("Clicky", 100);
        }
    }
}
