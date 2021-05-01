using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class ClickManager : MonoBehaviour
    {
        Camera cam;

        private void Start()
        {
            cam = Camera.main;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Mouse1))
            {
                Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
                if (hit)
                {
                    IClickable clickable = hit.collider.GetComponent<IClickable>();
                    clickable?.OnClick();
                }
                else
                {
                    Debug.Log("Click did not hit anything.");
                }
            }
        }
    }
}
