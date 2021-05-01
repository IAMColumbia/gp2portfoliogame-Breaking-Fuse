using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CableConnector.Models;
using System;
using Assets.Scripts;

public class UnityCableTile : MonoBehaviour, IClickable
{
    public enum TransformRotationDirections { Left, Right }
    public enum SelectionStates { Selected, NotSelected }

    public Sprite HiddenTexture, RevealedTexture, ConnectedTexture;
    private SpriteRenderer spriteRenderer;

    private CableTile.CableStates cableState;
    public CableTile.CableStates CableState
    {
        get { return this.cableTile.State = this.cableState; } //encapulsate block.BlockState
        set { this.cableTile.State = this.cableState = value; }
    }
    public CableTile.CableTypes CableType;
    public SelectionStates SelectionState { get; set; }
    public CableTile cableTile { get; set; }
    private GameObject manager;


    private void Start()
    {
        SetUpTile();
        UpdateTexture();
    }

    private void Update()
    {
        UpdateTexture();
    }

    protected virtual void SetUpTile()
    {
        this.SelectionState = SelectionStates.NotSelected;
        manager = FindObjectOfType<UnityGrid>().gameObject; //What if there's more than one UnityGrid?
        cableTile = new CableTile(this.CableType);
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        SetUpTextures();
        this.spriteRenderer.sprite = HiddenTexture;
        this.cableState = CableTile.CableStates.Hidden;
    }

    private void GetRandomRotation()
    {
        //CableRotator has its own "GetRandomRotation" but doesn't account for Transform rotations
        for (int i = 0; i < UnityEngine.Random.Range(0, 4); i++)
        {
            this.Rotate(TransformRotationDirections.Left);
        }
    }

    /// <summary>
    /// Load Texture Resources in case the textures haven't been set.
    /// </summary>
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

    /// <summary>
    /// Use the CableRotator class to rotate the nodes of this tile's Cable and rotate the GameObject Transform.
    /// </summary>
    /// <param name="rotDirection">The direction of the rotation (left/counter-clockwise or right/clockwise).</param>
    public virtual void Rotate(TransformRotationDirections rotDirection)
    {
        switch (rotDirection)
        {
            case TransformRotationDirections.Left:
                spriteRenderer.transform.Rotate(new Vector3(0, 0, 90));
                this.cableTile.Cable.Nodes = CableRotator.Instance.RotateLeft(this.cableTile.Cable.Nodes);
                break;
            case TransformRotationDirections.Right:
                spriteRenderer.transform.Rotate(new Vector3(0, 0, -90));
                this.cableTile.Cable.Nodes = CableRotator.Instance.RotateRight(this.cableTile.Cable.Nodes);
                break;
        }
    }

    /// <summary>
    /// Check the state of this cable and update to the appropriate texture.
    /// </summary>
    public virtual void UpdateTexture()
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
    }

    public virtual void OnClick()
    {
        if (manager.GetComponent<UnityGrid>().State == UnityGrid.GridStates.Unsolved)
            NotifyManager();
    }
    public virtual void NotifyManager()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
            manager.GetComponent<UnityGrid>().CableTileUpdate(this, KeyCode.Mouse0);
        else if (Input.GetKeyDown(KeyCode.Mouse1))
            manager.GetComponent<UnityGrid>().CableTileUpdate(this, KeyCode.Mouse1);
    }

    public void Reveal()
    {
        GetRandomRotation();
        this.cableState = CableTile.CableStates.Revealed;
    }

    public void Select()
    {
        this.spriteRenderer.color = Color.yellow;
        this.SelectionState = SelectionStates.Selected;
    }

    public void Deselect()
    {
        this.spriteRenderer.color = Color.white;
        this.SelectionState = SelectionStates.NotSelected;
    }
}
