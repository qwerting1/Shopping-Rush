using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangeMaterial : MonoBehaviour
{
    // -1 = no, 0 = looked away, 1 = previous frame, 2 = this frame
    private int isLookAt = -1;
    private TMP_Text currentItemText;

    
    // Start is called before the first frame update
    void Start()
    {
        currentItemText = GameObject.Find("Item").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isLookAt == 2)
        {
            var name = this.gameObject.tag;
            currentItemText.text = name;
            var mat = this.gameObject.GetComponent<Renderer>().material;
            mat.EnableKeyword("_EMISSION");

            if (Input.GetButtonDown("Interact")) 
            {
                currentItemText.text = "";
                SendTag();
                Destroy(gameObject);
            }
        }
        if (isLookAt >= 0)
        {
            isLookAt--;
        }
        if (isLookAt == 0) {
            var mat = this.gameObject.GetComponent<Renderer>().material;
            mat.DisableKeyword("_EMISSION");
            currentItemText.text = "";
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
