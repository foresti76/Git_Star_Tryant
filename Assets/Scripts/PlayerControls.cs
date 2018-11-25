using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControls : MonoBehaviour {

    public bool uiOpen = false;
    public bool combatModeActive = false;
    public GameObject selectedObject;
    public GameObject selectionObject;
    public GameObject miniMapSelectionObject;
    public Texture combatCursor;  // Your cursor texture

    Text selectionText;
    GameObject playerShip;
    ShipMovement shipMovement;
    WeaponController[] myWeaponControllers;
    //MiningLaser miningLaser;
    Radar myRadar;
    TractorBeam myTractorBeam;
    SelectionMover selectionObjectMover;
    SelectionMover miniMapSelectionObjectMover;
    float fAngle; //angle to rotate the combat cursor.

    float cursorSizeX = 32.0f;  // Your cursor size x
    float cursorSizeY = 32.0f;  // Your cursor size y

    // Use this for initialization
    void Start () {
        playerShip = GameObject.FindGameObjectWithTag("Player");
        shipMovement = playerShip.GetComponent<ShipMovement>();
        // todo set up fire groups
        myWeaponControllers = playerShip.GetComponentsInChildren<WeaponController>();
        //miningLaser = GetComponent<MiningLaser>();
        myRadar = GetComponent<Radar>();
        selectionText = GameObject.Find("PlayerSelectionText").GetComponent<Text>();
        selectionObject = GameObject.Find("SelectionCanvas");
        miniMapSelectionObject = GameObject.Find("MiniMapSelectionCanvas");
        selectionObjectMover = selectionObject.GetComponent<SelectionMover>();
        miniMapSelectionObjectMover = miniMapSelectionObject.GetComponent<SelectionMover>();
        myTractorBeam = playerShip.GetComponent<TractorBeam>();
    }

    // Update is called once per frame
    void Update () {
        if (combatModeActive)
        {
            Vector3 v3Pos;
            
            v3Pos = Camera.main.WorldToScreenPoint(playerShip.transform.position) - Input.mousePosition;
            fAngle = Mathf.Atan2(v3Pos.x, v3Pos.y) * Mathf.Rad2Deg; //todo remove radian conversion to Degrees and and add 2 Pi istead of 360.
            if (fAngle < 0.0f) { fAngle += 360.0f; }
        }


        if (!uiOpen)
        {
            // movement controls
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            {
                shipMovement.acclerating = true;
            }
            else if (shipMovement.acclerating)
            {
                shipMovement.acclerating = false;
            }

            if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            {
                shipMovement.decelerating = true;
            }
            else if (shipMovement.decelerating)
            {
                shipMovement.decelerating = false;
            }

            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                shipMovement.turningRight = true;
            }
            else if (shipMovement.turningRight)
            {
                shipMovement.turningRight = false;
            }

            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                shipMovement.turningLeft = true;
            }
            else if (shipMovement.turningLeft)
            {
                shipMovement.turningLeft = false;
            }

            if (Input.GetKey(KeyCode.Space))
            {
                shipMovement.stopping = true;
            }
            else if (shipMovement.stopping)
            {
                shipMovement.stopping = false;
            }

            //firing
            if (Input.GetButton("Fire1") && combatModeActive == true)
            {
                foreach (WeaponController weaponController in myWeaponControllers)
                {
                    weaponController.firing = true;
                }
            } else
            {
                foreach (WeaponController weaponController in myWeaponControllers)
                {
                    weaponController.firing = false;
                }
            }

            foreach (WeaponController weaponController in myWeaponControllers)
            {
                Vector3 targetPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
                targetPos.x = targetPos.x * Screen.width;
                targetPos.y = targetPos.y * Screen.height;

                weaponController.targetPos = targetPos;
            }

            if (Input.GetButton("Fire1") && combatModeActive == false)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                int layer1 = 10; //AI ships/shot layer
                int layer2 = 13; // Level layer
                int layerMask1 = 1 << layer1;
                int layerMask2 = 1 << layer2;
                int finalMask = layerMask1 | layerMask2; // Or, (1 << layer1) | (1 << layer2)

                if (Physics.Raycast(ray, out hit, 100.0f, finalMask))
                {
                    if (hit.transform.CompareTag("Asteroid") || hit.transform.CompareTag("AIShip"))
                    {
                        SetSelection(hit.transform.gameObject);
                    }
                }

            }
            //todo this should be set to use a generic subsystem type and not hard coded to the mining laser
            //if (Input.GetKey(KeyCode.Keypad1) && miningLaser.firingLaser == false && GetComponentInParent<Rigidbody>().velocity.magnitude == 0)
            //{
            //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //    RaycastHit hit;
            //    int layerMask = 1 << 16;
            //    //Debug.DrawRay(ray.origin, ray.direction, Color.red, 3.0f);
            //    if (Physics.Raycast(ray, out hit, 50.0f, layerMask))
            //    {
            //        if (hit.transform.CompareTag("Asteroid") || hit.transform.CompareTag("AIShip"))
            //        {
            //            myRadar.target= hit.transform.gameObject;
            //        }
            //    }
            //}


            if (Input.GetKeyDown(KeyCode.C))
            {
                if(combatModeActive == false)
                {
                    initateCombatMode();
                }
                else
                {
                    deactivateCombatMode();
                }
            }

            //Todo Lock onto a target
            if (Input.GetKeyDown(KeyCode.R))
            {
                if(combatModeActive == false)
                {
                    initateCombatMode();
                }
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                int layer1  = 10; //AI ships/shot layer
                int layer2 = 13; // Level layer
                int layerMask1 = 1 << layer1;
                int layerMask2 = 1  << layer2;
                int finalMask = layerMask1 | layerMask2; // Or, (1 << layer1) | (1 << layer2)

                if (Physics.Raycast(ray, out hit, 100.0f, finalMask))
                {
                    if (hit.transform.CompareTag("Asteroid") || hit.transform.CompareTag("AIShip"))
                    {
                        SetCombatTargetSelection(hit.transform.gameObject);
                    }
                }
            }
            
            if (!myRadar.detections.Contains(selectedObject))
            {
                RemoveSelection();
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (myTractorBeam.tractorBeamEngaged)
            {
                myTractorBeam.DisngageTractorBeam();
            }
            else
            { 
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                int layer1 = 10; //AI ships/shot layer
                int layer2 = 13; // Level layer
                int layerMask1 = 1 << layer1;
                int layerMask2 = 1 << layer2;
                int finalMask = layerMask1 | layerMask2; // Or, (1 << layer1) | (1 << layer2)

                if (Physics.Raycast(ray, out hit, 100.0f, finalMask) && !myTractorBeam.tractorBeamEngaged)
                {
                    if (hit.transform.CompareTag("Asteroid") || hit.transform.CompareTag("AIShip") || hit.transform.CompareTag("Loot"))
                    {
                        myTractorBeam.EngageTractorBeam(hit.transform.gameObject);
                    }
                }
            }
        }
    }

    void initateCombatMode()
    {
        combatModeActive = true;
        Debug.Log("Combat Mode Active");
        Cursor.visible = false;
        // todo make the turrets come out and change the UI to combat configuration
    }

    void deactivateCombatMode()
    {
        combatModeActive = false;
        selectionObjectMover.NonCombatTargeting();
        miniMapSelectionObjectMover.NonCombatTargeting();
        Debug.Log("Combat Mode Deactivated");
        Cursor.visible = true;
        // todo make the turrets go away and change the UI to non-combat configuration
    }

    public void SetSelection(GameObject selection)
    {
        selectedObject = selection;
        selectionText.text = selection.name;
        selectionObjectMover.parent = selection.transform;
        selectionObjectMover.NonCombatTargeting();
        selectionObject.SetActive(true);
        miniMapSelectionObjectMover.parent = selection.gameObject.transform.Find("MiniMapIcon").transform;
        miniMapSelectionObjectMover.NonCombatTargeting();
        miniMapSelectionObject.SetActive(true);
    }

    public void SetCombatTargetSelection(GameObject target)
    {
        myRadar.target = target;
        myRadar.RadarLock();
        selectedObject = target;
        selectionText.text = target.name;
        selectionObjectMover.parent = target.transform;
        selectionObjectMover.CombatTargeting();
        selectionObject.SetActive(true);
        miniMapSelectionObjectMover.parent = target.gameObject.transform.Find("MiniMapIcon").transform;
        miniMapSelectionObjectMover.CombatTargeting();
        miniMapSelectionObject.SetActive(true);
    }

    public void RemoveSelection()
    {
        selectionObjectMover.parent = null;
        miniMapSelectionObjectMover.parent = null;
        selectedObject = null;
        myRadar.target = null;
        selectionText.text = "None";
    }

    void OnGUI()
    {
        if (combatModeActive)
        { 
            float x = Event.current.mousePosition.x;
            float y = Event.current.mousePosition.y;
            Rect rect = new Rect(x - cursorSizeX / 2.0f, y - cursorSizeY / 2.0f, cursorSizeX, cursorSizeY);
            Vector2 pivot = new Vector2(x, y);
            GUIUtility.RotateAroundPivot(fAngle + 90f, pivot);
            GUI.DrawTexture(rect, combatCursor);
        }
    }
}
