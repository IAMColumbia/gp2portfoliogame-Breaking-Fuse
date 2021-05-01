using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    public Text CountdownText;
    public UnityGrid grid;
    float currentTime = 0f;
    float startingTime = 30f;

    private void Start()
    {
        grid = FindObjectOfType<UnityGrid>();
    }

    // Start is called before the first frame update
    public void Reset()
    {
        currentTime = startingTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (grid != null)
        {
            if (grid.State == UnityGrid.GridStates.Unsolved)
            {
                currentTime -= 1 * Time.deltaTime;
                CountdownText.text = currentTime.ToString("00.00");

                if (currentTime <= 0)
                {
                    currentTime = 0;
                    grid.RunEvaluation();
                }
            }
            else
            {
                CountdownText.text = "----";
            }
        }
    }
}
