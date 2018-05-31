using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MiniMap : MonoBehaviour, IPointerClickHandler {
    public Radar myRadar;
    public Camera portalCamera;
    // Use this for initialization
    void Start () {
        myRadar = GameObject.FindGameObjectWithTag("Player").GetComponent<Radar>();
        portalCamera = GameObject.Find("MiniMapCamera").GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {

    }


    public void OnPointerClick(PointerEventData eventData)
    {
        Vector2 localCursor;
        var rect1 = GetComponent<RectTransform>();
        var pos1 = eventData.position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rect1, pos1, null, out localCursor);

        Debug.Log("Local Cursor position: " + localCursor);

        int xpos = (int)(localCursor.x);
        int ypos = (int)(localCursor.y);

        if (xpos < 0) xpos = xpos + (int)rect1.rect.width / 2;
        else xpos += (int)rect1.rect.width / 2;

        if (ypos > 0) ypos = ypos + (int)rect1.rect.height / 2;
        else ypos += (int)rect1.rect.height / 2;

        Debug.Log("Corrected Cursor Pos: " + xpos + " " + ypos);

        float xpos2 = xpos / (int)rect1.rect.width;
        float ypos2 = ypos / (int)rect1.rect.height;

        Debug.Log("Pixel width : " + portalCamera.pixelWidth +", " +portalCamera.pixelHeight);
        Vector3 screenPoint = new Vector3(xpos, ypos, 0.0f);

        Ray portalRay = portalCamera.ScreenPointToRay(screenPoint);
        RaycastHit portalHit;
        // test these camera coordinates in another raycast test

        Debug.DrawRay(screenPoint, portalCamera.transform.forward * 100, Color.red, 10);

        if (Physics.Raycast(portalRay, out portalHit, Mathf.Infinity, 12))
        {
            Debug.Log("Hit something:" + portalHit.collider.gameObject);
        }
        else
        {
            Debug.Log("Hit nothing in the portal");
        }
    }
}
