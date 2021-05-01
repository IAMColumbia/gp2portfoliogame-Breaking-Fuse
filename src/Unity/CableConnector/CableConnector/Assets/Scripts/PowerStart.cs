using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts;
using CableConnector.Models;

public class PowerStart : MonoBehaviour , IClickable
{
    public Text PowerButtonText;
    private UnityGrid grid;

    // Start is called before the first frame update
    void Start()
    {
        grid = FindObjectOfType<UnityGrid>();
    }

    private void Update()
    {
        if (grid != null)
        {
            if (grid.State != UnityGrid.GridStates.Unsolved)
                PowerButtonText.GetComponent<Text>().text = "START";
            else
            {

                PowerButtonText.GetComponent<Text>().text = "POWER";
            }
        }
    }

    public void OnClick()
    {
        PressPowerButton();
    }

    public void PressPowerButton()
    {
        if (grid != null)
        {
            if (grid.State != UnityGrid.GridStates.Unsolved)
                grid.Reset();
            else
            {
                grid.RunEvaluation();
            }
        }
    }

}
