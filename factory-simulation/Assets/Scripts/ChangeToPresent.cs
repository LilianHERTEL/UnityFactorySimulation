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
        if (!_in.CompareTag("Jetable") && // Evite d'emballer les objets jetables
            !_in.tag.Contains("_Present") && // Evite d'emballer deux fois le même objet
            _in != GameManager.objetSaisi) // Evite d'emballer un objet saisi (évite la triche en passant directement l'objet dans la machine sans passer par le tapis roulant)
        {
            _out = Instantiate(present, _in.transform.position, Quaternion.identity);
            _out.tag = _in.tag + "_Present";
            Destroy(_in);
        }
    }
}
