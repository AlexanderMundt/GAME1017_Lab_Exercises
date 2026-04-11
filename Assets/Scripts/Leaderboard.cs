/* Completed By:    Alexander Mundt - 101632886
 * Assignment:      Assignment 2
 * Class:           GAME-1017
 * Professor:       Ernie Burrows
 */
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Leaderboard : MonoBehaviour
{
    [SerializeField] private TMP_Text leaderboardText;

    public void Initialize(List<float> leaderboard)
    {
        string leaderboardString = "<b>Leaderboard</b>";

        for (int i = 0; i < leaderboard.Count; i++)
        {
            leaderboardString += "\n#" + (i + 1) + " " + FormatScore(leaderboard[i]);
        }

        leaderboardText.text = leaderboardString;
    }

    private string FormatScore(float score)
    {
        int minutes = (int)score / 60;
        float seconds = score % 60.0f;
        return $"{minutes:00}:{seconds:00.00}";
    }
}
