using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionMover : MonoBehaviour {

    public Transform parent;
    public Sprite selectedTexture;
    public Sprite targetedTexture;

    private Image currentTexture;

    // Use this for initialization
    private void Start()
    {
        currentTexture = GetComponentInChildren<Image>();
    }
    // Update is called once per frame
    void Update() {
        if (parent)
        {
            transform.position = parent.transform.position;
           // Vector3 parentBounds = parent.GetComponent<Renderer>().bounds.size;
          //  Debug.Log(parentBounds);
            // todo scale the selection box to the appropriate size dynamically  this.transform.localScale = new Vector3(parentBounds.x, parentBounds.z, 1);
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }

    public void CombatTargeting(){
        currentTexture.sprite = targetedTexture;
    }

    public void NonCombatTargeting()
    {
        currentTexture.sprite = selectedTexture;
    }
}
