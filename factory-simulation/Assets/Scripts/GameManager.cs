using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float time = 1; // Durée du jeu en minutes

    public static bool openDoor = false;
    public static float conveyor_speed = 0.5f;

    private bool _gameIsOver = false;
    private float _timeSeconds;

    private void Awake()
    {
        _timeSeconds = time * 60;
        DontDestroyOnLoad(gameObject); // Permet de conserver cet objet dans toutes les scènes
    }

    void Update()
    {
        if (!_gameIsOver)
        {
            if (_timeSeconds > 0)
            {
                _timeSeconds -= Time.deltaTime;
            }
            else
            {
                _gameIsOver = true;
                Debug.Log("GAME OVER!");
                StartCoroutine(EndGame());
            }
        }
    }

    IEnumerator EndGame()
    {
        GameObject text;

        // CHargement de la scène de fin
        SceneManager.LoadScene("GameOverScene");
        // Le chargement de la scène se termine à la prochaine frame donc on attend :
        yield return null;
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("GameOverScene"));


        text = GameObject.Find("TextScore");
        if (text != null)
            text.GetComponent<Text>().text = 25.ToString();
        else
            Debug.Log("TEXT IS NULL");
    }

    public static void ExitGame()
    {
        Debug.Log("Exiting game"); // pour tests dans l'éditeur
        Application.Quit();
    }
}