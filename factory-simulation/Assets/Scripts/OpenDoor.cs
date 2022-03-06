using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    GameObject doorLeft, doorRight;

    // Start is called before the first frame update
    void Start()
    {
        doorLeft = transform.GetChild(0).gameObject;
        doorRight = transform.GetChild(1).gameObject;
        translateX();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void translateX()
    {
        Vector3 xyz_left = doorLeft.transform.position;
        Vector3 xyz_right = doorRight.transform.position;
        for (float t = 0; t < 0.9; t += 0.01f)
        {
            doorLeft.transform.position = xyz_left + Vector3.left * t;
            doorRight.transform.position = xyz_right + Vector3.right * t;
        }
    }
}
