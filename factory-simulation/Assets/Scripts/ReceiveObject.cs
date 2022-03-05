using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReceiveObject : MonoBehaviour
{
    private GameObject _receivedObject;
    public Text text;

    private int score = 0;

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Attrapable"))
        {
            _receivedObject = col.gameObject;
            _receivedObject.GetComponent<Rigidbody>().useGravity = true;
            col.isTrigger = false;
            _receivedObject.transform.parent = null;
            _receivedObject.tag = "Untagged";

            score++;
            Debug.Log("Score = " + score);
            text.text = score.ToString();
        }
    }
}
