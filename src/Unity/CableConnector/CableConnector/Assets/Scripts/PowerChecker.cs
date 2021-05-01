using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class PowerChecker : MonoBehaviour
{
    public Sprite NoPowerTexture, PoweredTexture;
    private GameObject GridManager;

    private void Start()
    {
        GridManager = FindObjectOfType<UnityGrid>().gameObject;
        CheckPower();
    }

    // Update is called once per frame
    void Update()
    {
        CheckPower();
    }

    void CheckPower()
    {
        if (GridManager.GetComponent<UnityGrid>().State == UnityGrid.GridStates.Solved)
        {
                this.GetComponent<SpriteRenderer>().sprite = PoweredTexture;
        }
        else
        {
                this.GetComponent<SpriteRenderer>().sprite = NoPowerTexture;
        }
    }
}
