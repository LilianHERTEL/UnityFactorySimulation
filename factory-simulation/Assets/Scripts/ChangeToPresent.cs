using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// <summary>
//      Script associ� � la machine d'emballage (sur le Trigger)
// </summary>
public class ChangeToPresent : MonoBehaviour
{
    private GameObject _in, _out;

    [Tooltip("Apparence de paquet cadeau voulue")]
    public GameObject present;

    // <summary>
    //      Transforme une peluche (non jetable) en paquet cadeau
    //
    //      D�truit la peluche et instancie un cadeau avec le bon tag � la m�me position
    // </summary>
    private void OnTriggerExit(Collider col)
    {
        _in = col.gameObject;
        if (!_in.CompareTag("Jetable") && // Evite d'emballer les objets jetables
            !_in.tag.Contains("_Present") && // Evite d'emballer deux fois le m�me objet
            _in != GameManager.objetSaisi) // Evite d'emballer par erreur un objet saisi
                                           // (�vite aussi la triche en passant directement
                                           // l'objet dans la machine sans passer par le tapis roulant)
        {
            _out = Instantiate(present, _in.transform.position, Quaternion.identity);
            _out.tag = _in.tag + "_Present";
            Destroy(_in);
        }
    }
}
