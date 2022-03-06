using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReceiveObject : MonoBehaviour
{
    private GameObject _receivedObject;
    public Text text;
    public string type;

    private int score = 0;

    private void OnTriggerEnter(Collider col)
    {
        if (type != "Jetable")
        {
            if (col.gameObject.CompareTag(type + "_Present"))
            {
                _receivedObject = col.gameObject;
                _receivedObject.GetComponent<Rigidbody>().useGravity = true;
                col.isTrigger = false;
                _receivedObject.transform.parent = null;
                _receivedObject.tag = "Untagged";

                score++;
                text.text = score.ToString();
            }
        }
        else // Si Jetable (donc quand le script est associé à la poubelle)
        {/*
            if (col.gameObject.CompareTag("Jetable"))
            {
                Destroy(col.gameObject);
                score++;
                text.text = score.ToString();
            }*/
        }
    }
}
