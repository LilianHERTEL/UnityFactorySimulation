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
                _objetSaisi = _touche;
                if (_touche.CompareTag("Attrapable") && !_saisieEnCours)
                {
                    _saisieEnCours = true;
                    _touche.GetComponent<Rigidbody>().useGravity = false;
                    _touche.transform.parent = transform.parent;
                    _touche.transform.position = transform.parent.transform.position;
                    _touche.transform.rotation = new Quaternion(0, 0, 0, 0);
                    _touche.transform.Translate(Vector3.forward);
                }
                else if (_touche.CompareTag("Attrapable") && _saisieEnCours)
                {
                    _saisieEnCours = false;
                    _touche.GetComponent<Rigidbody>().useGravity = true;
                    _touche.transform.parent = null;
                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                // Téléporter l'utilisateur
                playerController.enabled = false;
                _player.transform.position = hitInfo.point;
                playerController.enabled = true;
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0) && _saisieEnCours == true)
            {
                _saisieEnCours = false;
                _objetSaisi.GetComponent<Rigidbody>().useGravity = true;
                _objetSaisi.transform.parent = null;
            }
            rayRenderer.SetPosition(0, transform.position);
            rayRenderer.SetPosition(1, transform.position + transform.forward*1000f);
        }
    }

    void GenerateRandomObject()
    {
        int i = Random.Range(0, 2);
        Debug.Log("Nombre généré = " + i);
        Instantiate(_objects[i], _distributeur.transform.position + new Vector3(-0.4f, 0, 0.7f), Quaternion.identity);
    }
}