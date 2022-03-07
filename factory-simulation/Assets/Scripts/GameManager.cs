using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// <summary>
//      Script associé à un objet arbitraire servant de point central au programme
//
//      Contient quelques variables globales utiles dans d'autres scripts
//      Et gère la terminaison du programme
// </summary>
public class GameManager : MonoBehaviour
{
    private bool _gameIsOver = false;
    private float _timeSeconds;
    private bool isStartedTimer = false;

    public static int nbMistakes = 0;

    public static bool openDoor = false;
    public static float conveyor_speed = 0.5f;
    public static int score = 0;
    public static GameObject objetSaisi = null;

    [Tooltip("Limite de temps en minutes")]
    public float time = 1;

    [Tooltip("Placer ici le Text qui doit afficher le temps restant")]
    public Text textTime;

    [Tooltip("Placer ici le Text qui doit afficher le temps total")]
    public Text textTimeTotal;

    public GameObject player;

    private void Awake()
    {
        textTimeTotal.text = time.ToString();
        _timeSeconds = time * 60;
        
        DontDestroyOnLoad(gameObject); // Permet de conserver cet objet dans toutes les scènes
    }

    void Update()
    {
        // Déclenche le timer une seule fois, à la première ouverture de la porte
        if (openDoor && !isStartedTimer) isStartedTimer = true;

        // Gère le temps et le game over
        if (!_gameIsOver)
        {
            if (_timeSeconds <= 0 || nbMistakes >= 10)
            {
                _gameIsOver = true;
                StartCoroutine(EndGame());
            }
            else if (isStartedTimer)
            {
                _timeSeconds -= Time.deltaTime;
                UpdateTimeDisplay();
            }
        }
    }

    // <summary>
    //      Met à jour l'écran d'affichage du temps restant
    // </summary>
    void UpdateTimeDisplay()
    {
        TimeSpan tmp = TimeSpan.FromSeconds(_timeSeconds);
        textTime.text = string.Format("{0:00}:{1:00}", tmp.Minutes, tmp.Seconds);
    }

    // <summary>
    //      Coroutine de fin de jeu
    //
    //      Nécessaire pour changer de scène et afficher le score total
    // </summary>
    IEnumerator EndGame()
    {
        //GameObject text;
        //Canvas resultScreen;

        // Chargement de la scène de fin
        SceneManager.LoadScene("GameOverScene");

        // Le chargement de la scène se termine à la prochaine frame donc on attend
        yield return null;

        SceneManager.SetActiveScene(SceneManager.GetSceneByName("GameOverScene"));

        // Affichage échec
        if (nbMistakes >= 10)
        {
            GameObject.Find("TextResult").GetComponent<Text>().text = "Échec ! Vous avez fait trop de fautes.";
        }
        else // Affichage succès
        {
            // Le texte par défaut en cas de succès est "Temps écoulé ! Score : "

            // Affichage du score total
            GameObject.Find("TextScore").GetComponent<Text>().text = score.ToString();
        }
    }

    // <summary>
    //      Quitte le programme
    // </summary>
    public static void ExitGame()
    {
        //Debug.Log("Exiting game"); // pour tests dans l'éditeur
        Application.Quit();
    }
}