using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// <summary>
//      Script associ� � un objet arbitraire servant de point central au programme
//
//      Contient quelques variables globales utiles dans d'autres scripts
//      Et g�re la terminaison du programme
// </summary>
public class GameManager : MonoBehaviour
{
    private bool _gameIsOver = false;
    private float _timeSeconds;
    private bool isStartedTimer = false;

    public static bool openDoor = false;
    public static float conveyor_speed = 0.5f;
    public static int score = 0;
    public static GameObject objetSaisi = null;

    [Tooltip("Limite de temps en minutes")]
    public float time = 1;

    [Tooltip("Placer ici le Text qui doit afficher le temps restant")]
    public Text textTime;

    private void Awake()
    {
        _timeSeconds = time * 60;
        DontDestroyOnLoad(gameObject); // Permet de conserver cet objet dans toutes les sc�nes
    }

    void Update()
    {
        // D�clenche le timer une seule fois, � la premi�re ouverture de la porte
        if (openDoor && !isStartedTimer) isStartedTimer = true;

        // D�cr�mente le temps restant jusqu'� ce qu'il soit �coul�
        if (!_gameIsOver)
        {
            if (_timeSeconds > 0)
            {
                if (isStartedTimer) _timeSeconds -= Time.deltaTime;
                UpdateTimeDisplay();
            }
            else
            {
                _gameIsOver = true;
                StartCoroutine(EndGame());
            }
        }
    }

    // <summary>
    //      Met � jour l'�cran d'affichage du temps restant
    // </summary>
    void UpdateTimeDisplay()
    {
        TimeSpan tmp = TimeSpan.FromSeconds(_timeSeconds);
        textTime.text = string.Format("{0:00}:{1:00}", tmp.Minutes, tmp.Seconds);
    }

    // <summary>
    //      Coroutine de fin de jeu
    //
    //      N�cessaire pour changer de sc�ne et afficher le score total
    // </summary>
    IEnumerator EndGame()
    {
        GameObject text;

        // Chargement de la sc�ne de fin
        SceneManager.LoadScene("GameOverScene");

        // Le chargement de la sc�ne se termine � la prochaine frame donc on attend
        yield return null;

        SceneManager.SetActiveScene(SceneManager.GetSceneByName("GameOverScene"));

        // Affichage du score total
        text = GameObject.Find("TextScore");
        if (text != null)
            text.GetComponent<Text>().text = score.ToString();
    }

    // <summary>
    //      Quitte le programme
    // </summary>
    public static void ExitGame()
    {
        //Debug.Log("Exiting game"); // pour tests dans l'�diteur
        Application.Quit();
    }
}