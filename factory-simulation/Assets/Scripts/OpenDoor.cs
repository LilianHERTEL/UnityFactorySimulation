using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// <summary>
//      Script associ� � la porte
// </summary>
public class OpenDoor : MonoBehaviour
{
    GameObject doorLeft, doorRight; // Objets enfants porte gauche et droite

    Coroutine currentCoroutine = null; // Conserve la coroutine en cours d'ex�cution

    [Tooltip("Temps d'ouverture (/fermeture) de la porte (en secondes)")]
    public float animationLength = 1;

    [Tooltip("Temps durant lequel la porte reste ouverte pour laisser passer le joueur (en secondes)")]
    public float waitingLength = 1;

    void Start()
    {
        // R�cup�ration des portes
        doorLeft = transform.GetChild(0).gameObject;
        doorRight = transform.GetChild(1).gameObject;
    }

    void Update()
    {
        // Si bouton appuy� ET coroutine pas d�j� en cours d'ex�cution
        if (GameManager.openDoor && currentCoroutine == null)
            currentCoroutine = StartCoroutine(TranslateX()); // D�clenchement de la coroutine
    }

    // <summary>
    //      Coroutine d'ouverture + fermeture des portes
    //
    //      Effectue une translation de chaque c�t� de la porte vers l'ext�rieur
    //      Laisse passer le joueur durant waitingLength secondes
    //      Puis referme les portes
    // </summary>
    IEnumerator TranslateX()
    {
        Vector3 xyz_left, xyz_right;
        float t;

        if (currentCoroutine != null)
            Debug.Log("Already running");

        // Calcul des positions initiales de la porte
        xyz_left = doorLeft.transform.position;
        xyz_right = doorRight.transform.position;
        for (t = 0f; t < 1; t += 0.01f) // Ouverture des portes
        {
            doorLeft.transform.position = xyz_left + Vector3.left * (t + 0.01f);
            doorRight.transform.position = xyz_right + Vector3.right * (t + 0.01f);
            yield return new WaitForSecondsRealtime(animationLength * 0.01f); // Point o� l'ex�cution se met en pause jusqu'� la prochaine frame
        }

        // Attente pour laisser passer le joueur
        yield return new WaitForSecondsRealtime(waitingLength);

        // Calcul des nouvelles positions portes ouvertes
        xyz_left = doorLeft.transform.position;
        xyz_right = doorRight.transform.position;
        for (t = 0f; t < 1; t += 0.01f) // Fermeture des portes
        {
            doorLeft.transform.position = xyz_left - Vector3.left * (t + 0.01f);
            doorRight.transform.position = xyz_right - Vector3.right * (t + 0.01f);
            yield return new WaitForSecondsRealtime(animationLength * 0.01f); // Point o� l'ex�cution se met en pause jusqu'� la prochaine frame
        }

        GameManager.openDoor = false;
        currentCoroutine = null;
    }
}
