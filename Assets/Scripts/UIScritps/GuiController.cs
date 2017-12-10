using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuiController : MonoBehaviour {

    public Texture yourCursor;  // Your cursor texture
    float cursorSizeX = 32.0f;  // Your cursor size x
    float cursorSizeY = 32.0f;  // Your cursor size y
    GameObject player;
    public float fAngle;

    void Start()
    {
       // Cursor.visible = false;
       player = GameObject.FindGameObjectWithTag ( "Player" );
    }

    void Update()
    {
        Vector3 v3Pos;

        v3Pos = Camera.main.WorldToScreenPoint(player.transform.position) - Input.mousePosition;
        fAngle = Mathf.Atan2(v3Pos.x, v3Pos.y) * Mathf.Rad2Deg; //todo remove radian conversion to Degrees and and add 2 Pi istead of 360.
        if (fAngle < 0.0f) {fAngle += 360.0f; }
        //Debug.Log("Cursor Angle :" + fAngle);

    }

   void OnGUI()
    {
        var matx = GUI.matrix;
        float x = Event.current.mousePosition.x;
        float y = Event.current.mousePosition.y;
        Rect rect = new Rect(x, y, cursorSizeX, cursorSizeY);
        Vector2 pivot = new Vector2(x + cursorSizeX / 2.0f, y + cursorSizeY / 2.0f);
        GUIUtility.RotateAroundPivot(fAngle +90f, pivot);
        GUI.DrawTexture(rect, yourCursor);
        GUI.matrix = matx;
    }
}
