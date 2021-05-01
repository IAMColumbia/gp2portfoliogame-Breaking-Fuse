using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static int HighestWins { get; private set; }
    public static int CurrentWins { get; private set; }

    public Text HighestWinsText;
    public Text CurrentWinsText;

    private void Update()
    {
        if (CurrentWins >= HighestWins)
            HighestWins = CurrentWins;

        HighestWinsText.text = "Highest Win Streak: " + HighestWins;
        CurrentWinsText.text = "Wins: " + CurrentWins;

    }

    public static void Win()
    {
        CurrentWins++;
        if (CurrentWins >= HighestWins)
            HighestWins = CurrentWins;

    }

    public static void Lose()
    {
        CurrentWins = 0;
    }
}
