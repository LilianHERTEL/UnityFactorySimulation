using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeToPresent : MonoBehaviour
{
    private GameObject _in, _out;

    public GameObject present;
    private void OnTriggerExit(Collider col)
    {
        _in = col.gameObject;
        if (!_in.CompareTag("Jetable") && !_in.tag.Contains("_Present")) // Evite d'emballer les objets jetables et �vite d'emballer deux fois le m�me objet
        {
            _out = Instantiate(present, _in.transform.position, Quaternion.identity);
            _out.tag = _in.tag + "_Present";
            Destroy(_in);
        }
    }
}
