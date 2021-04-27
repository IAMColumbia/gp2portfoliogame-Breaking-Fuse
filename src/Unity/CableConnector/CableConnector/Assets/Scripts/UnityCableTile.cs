using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CableConnector.Models;

public class UnityCableTile : MonoBehaviour
{
    //GameObject properties to visually represent this Tile
    public Sprite HiddenTexture, RevealedTexture, ConnectedTexture;
    public float SpriteRotation { get; set; }

    public CableTile cableTile;
    private SpriteRenderer spriteRenderer;

    public CableTile.CableStates CableState;
    public CableTile.CableTypes CableType;

    private void Start()
    {
        cableTile = new CableTile(CableType);
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        SetUpTile();
        Draw();
    }

    [ExecuteInEditMode]
    protected virtual void SetUpTile()
    {
        SpriteRotation = 0f;
        cableTile = new CableTile(this.CableType);
        SetUpTextures();
        this.spriteRenderer.sprite = HiddenTexture;
        this.CableState = CableTile.CableStates.Revealed;
    }

    protected virtual void SetUpTextures()
    {
        //Assign Texture for "Hidden" State
        HiddenTexture = Resources.Load<Sprite>("cableTile_Hidden");

        //Assign Texture for "Revealed" State based on CableType
        switch (CableType)
        {
            case CableTile.CableTypes.Straight:
                RevealedTexture = Resources.Load<Sprite>("cableTile_Revealed_Straight") as Sprite;
                break;
            case CableTile.CableTypes.Curved:
                RevealedTexture = Resources.Load<Sprite>("cableTile_Revealed_Curved") as Sprite;
                break;
            case CableTile.CableTypes.FourWay:
                RevealedTexture = Resources.Load<Sprite>("cableTile_Revealed_FourWay") as Sprite;
                break;
        }

        //Assign Texture for "Connected" State based on CableType
        switch (CableType)
        {
            case CableTile.CableTypes.Straight:
                ConnectedTexture = Resources.Load<Sprite>("cableTile_Connected_Straight") as Sprite;
                break;
            case CableTile.CableTypes.Curved:
                ConnectedTexture = Resources.Load<Sprite>("cableTile_Connected_Curved") as Sprite;
                break;
            case CableTile.CableTypes.FourWay:
                ConnectedTexture = Resources.Load<Sprite>("cableTile_Connected_FourWay") as Sprite;
                break;
        }
    }


    //Check State of cableTile and update Texture
    public virtual void Draw()
    {
        switch (CableState)
        {
            case CableTile.CableStates.Hidden:
                this.spriteRenderer.sprite = HiddenTexture;
                break;
            case CableTile.CableStates.Revealed:
                this.spriteRenderer.sprite = RevealedTexture;
                break;
            case CableTile.CableStates.Connected:
                this.spriteRenderer.sprite = ConnectedTexture;
                break;
        }
        spriteRenderer.transform.Rotate(new Vector3(0, 0, 0));
    }

}
