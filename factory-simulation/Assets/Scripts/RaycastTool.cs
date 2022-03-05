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
    public GameObject _buttonDistrib;
    public List<GameObject> _objects;
    private bool _saisieEnCours = false;

    private GameObject _objetSaisi = null;
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

            if (Input.GetMouseButtonDown(0))
            {
                //@TODO Interagir avec les boutons et attraper les objets
                // Animation appui bouton à faire

                // Distributeur fonctionne
                if (hitInfo.transform.gameObject == _buttonDistrib)
                {
                    GenerateRandomObject();
                }

                _touche = hitInfo.transform.gameObject;
                if (_touche.CompareTag("Attrapable"))
                {
                    _objetSaisi = _touche;
                    _objetSaisi_rigidbody = _touche.GetComponent<Rigidbody>();
                    if (!_saisieEnCours) takeObject();
                    else releaseObject();
                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                // Téléporter l'utilisateur
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
        Debug.Log("Nombre généré = " + i);
        Instantiate(_objects[i], _distributeur.transform.position + new Vector3(-0.4f, 0, 0.7f), Quaternion.identity);
    }

    void takeObject()
    {
        _saisieEnCours = true;
        _objetSaisi_rigidbody.velocity = Vector3.zero;
        _objetSaisi_rigidbody.angularVelocity = Vector3.zero;
        _objetSaisi_rigidbody.useGravity = false;
        _objetSaisi.transform.parent = transform.parent;
        _objetSaisi.transform.position = transform.parent.transform.position;
        _objetSaisi.transform.rotation = new Quaternion(0, 0, 0, 0);
        _objetSaisi.transform.Translate(Vector3.forward);

        _objetSaisi.GetComponent<Collider>().isTrigger = true;
    }
    void releaseObject()
    {
        Debug.Log("Relâchement");
        _saisieEnCours = false;
        _objetSaisi.GetComponent<Rigidbody>().useGravity = true;
        _objetSaisi.transform.parent = null;

        _objetSaisi.GetComponent<Collider>().isTrigger = false;

        _objetSaisi = null;
        _objetSaisi_rigidbody = null;
    }
}