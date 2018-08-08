using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MiniMap : MonoBehaviour, IPointerClickHandler {
    public Radar myRadar;
    public Camera portalCamera;

    private PlayerControls playerControls;

    // Use this for initialization
    void Start () {
        myRadar = GameObject.FindGameObjectWithTag("Player").GetComponent<Radar>();
        portalCamera = GameObject.Find("MiniMapCamera").GetComponent<Camera>();
        playerControls = FindObjectOfType<PlayerControls>();
	}

    public void OnPointerClick(PointerEventData eventData)
    {
        Vector2 localCursor;
        var rect1 = GetComponent<RectTransform>();
        var pos1 = eventData.position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rect1, pos1, null, out localCursor);

        int xpos = (int)(localCursor.x);
        int ypos = (int)(localCursor.y);

        if (xpos < 0) xpos = xpos + (int)rect1.rect.width / 2;
        else xpos += (int)rect1.rect.width / 2;

        if (ypos > 0) ypos = ypos + (int)rect1.rect.height / 2;
        else ypos += (int)rect1.rect.height / 2;

        float xpos2 = xpos / (int)rect1.rect.width;
        float ypos2 = ypos / (int)rect1.rect.height;

        Vector3 screenPoint = new Vector3(xpos, ypos, 0.0f);

        Ray portalRay = portalCamera.ScreenPointToRay(screenPoint);
        RaycastHit portalHit;

        int finalMask = 1 << 12; // This is the layermask for the minimap.

        if (Physics.Raycast(portalRay, out portalHit, Mathf.Infinity, finalMask))
        {
            if (playerControls.combatModeActive)
            {
                playerControls.SetCombatTargetSelection(portalHit.transform.gameObject);
            }
            else
            {
                playerControls.SetSelection(portalHit.transform.gameObject);
            }

        }
    }
}
