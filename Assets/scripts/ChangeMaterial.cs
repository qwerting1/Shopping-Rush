using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangeMaterial : MonoBehaviour
{
    // -1 = no, 0 = looked away, 1 = previous frame, 2 = this frame
    private int isLookAt = -1;
    private TMP_Text wordText;

    
    // Start is called before the first frame update
    void Start()
    {
        var canvas = GameObject.Find("Canvas");
        var textTransform = canvas.transform.Find("Item (TMP)");
        wordText = textTransform.GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isLookAt == 2)
        {
            //print("the colour has been changed");
            //var colour = this.gameObject.GetComponent<Renderer>().material.color;
            //colour.a = 0.2f; //set the transparency of highlighted object
            //this.gameObject.GetComponent<Renderer>().material.color = colour;
            var name = this.gameObject.tag;
            wordText.text = name;
            var mat = this.gameObject.GetComponent<Renderer>().material;
            mat.EnableKeyword("_EMISSION");

            if (Input.GetButtonDown("Interact")) 
            {
                wordText.text = "";
                SendTag();
                Destroy(gameObject);
            }
        }
        if (isLookAt >= 0)
        {
            isLookAt--;
        }
        if (isLookAt == 0) {
            //print("the colour has been reset");
            //var colourReset = this.gameObject.GetComponent<Renderer>().material.color;
            //colourReset.a = 1f;
            //this.gameObject.GetComponent<Renderer>().material.color = colourReset;
            var mat = this.gameObject.GetComponent<Renderer>().material;
            mat.DisableKeyword("_EMISSION");
            wordText.text = "";
        }
    }

    void Receive(int GameObjectInstanceID)
    {
        isLookAt = 2;
    }

    public void SendTag()
    {
        GameObject otherObject = GameObject.Find("Manager");
        RandomList otherScript = otherObject.GetComponent<RandomList>();
        otherScript.ReceiveTag(gameObject.tag);
    }

}
