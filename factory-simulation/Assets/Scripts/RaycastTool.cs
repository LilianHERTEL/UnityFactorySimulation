using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RaycastTool : MonoBehaviour
{
    public CharacterController playerController;
    public LineRenderer rayRenderer;
    public GameObject _player;
    public GameObject _distributeur;
    public List<GameObject> _objects;
    private bool _saisieEnCours = false;

    private Rigidbody _objetSaisi_rigidbody = null;

    public float _rayWidth = 0.05f;
    void Awake()
    {
        rayRenderer.startWidth = _rayWidth;
        rayRenderer.endWidth = _rayWidth;
    }

    // Late Update fonctionne de manière très similaire à Update, mais se lance après toutes les fonctions "Update"
    // ici cela peut éviter que changer la position du playerController n'entre en conflit avec le script PlayerMovement
    void LateUpdate()
    {
        GameObject _touche;

        if (Physics.Raycast(transform.position, transform.forward, out var hitInfo))
        {
            rayRenderer.SetPosition(0, transform.position);
            rayRenderer.SetPosition(1, hitInfo.point);
     
            Debug.DrawRay(transform.position, transform.forward, Color.blue);

            // Gestion du clic gauche laser
            if (Input.GetMouseButtonDown(0))
            {
                // Animation appui bouton à faire

                _touche = hitInfo.transform.gameObject;

                if (isSaisissable(_touche))
                {
                    if (!_saisieEnCours) takeObject(_touche);
                    else releaseObject();
                } 
                else {
                    switch (_touche.tag)
                    {
                        case "BoutonPorte": GameManager.openDoor = true;
                                            break;
                        case "BoutonDistrib": GenerateRandomObject();
                                              break;
                        case "ExitButton": GameManager.ExitGame();
                                           break;
                    }
                }
            }

            // Gestion du clic droit téléportation
            if (Input.GetMouseButtonDown(1))
            {
                playerController.enabled = false;
                _player.transform.position = hitInfo.point + Vector3.up; // Vector3.up corrige un problème où le personnage se retrouve parfois dans le sol à l'arrivée de la téléportation
                playerController.enabled = true;
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0) && _saisieEnCours) releaseObject();
            rayRenderer.SetPosition(0, transform.position);
            rayRenderer.SetPosition(1, transform.position + transform.forward*1000f);
        }
    }

    void GenerateRandomObject()
    {
        int i = Random.Range(0, _objects.Count);
        //Debug.Log("Nombre généré = " + i);
        Instantiate(_objects[i], _distributeur.transform.position + new Vector3(-0.4f, 0, 0.7f), Quaternion.Euler(Random.Range(0, 90), Random.Range(0, 90), Random.Range(0, 90)));
    }

    bool isSaisissable(GameObject _obj)
    {
        return (_obj.CompareTag("SaisissableBEAR") ||
                _obj.CompareTag("SaisissableBEAR_Present") ||
                _obj.CompareTag("SaisissablePENGUIN") ||
                _obj.CompareTag("SaisissablePENGUIN_Present") ||
                _obj.CompareTag("SaisissableRABBIT") ||
                _obj.CompareTag("SaisissableRABBIT_Present") ||
                _obj.CompareTag("Jetable"));
    }
    void takeObject(GameObject _obj)
    {
        GameManager.objetSaisi = _obj;
        _objetSaisi_rigidbody = _obj.GetComponent<Rigidbody>();

        _saisieEnCours = true;
        _objetSaisi_rigidbody.velocity = Vector3.zero;
        _objetSaisi_rigidbody.angularVelocity = Vector3.zero;
        _objetSaisi_rigidbody.useGravity = false;
        GameManager.objetSaisi.transform.parent = transform.parent;
        GameManager.objetSaisi.transform.position = transform.parent.transform.position;
        GameManager.objetSaisi.transform.rotation = new Quaternion(0, 0, 0, 0);
        GameManager.objetSaisi.transform.Translate(Vector3.forward);

        GameManager.objetSaisi.GetComponent<Collider>().isTrigger = true;

        //Debug.Log(_objetSaisi.tag);
    }
    void releaseObject()
    {
        //Debug.Log("Relâchement");
        GameManager.objetSaisi.GetComponent<Rigidbody>().useGravity = true;
        GameManager.objetSaisi.GetComponent<Collider>().isTrigger = false;
        GameManager.objetSaisi.transform.parent = null;

        _saisieEnCours = false;
        GameManager.objetSaisi = null;
        _objetSaisi_rigidbody = null;
    }
}