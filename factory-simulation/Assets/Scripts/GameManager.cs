using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float time = 10; // Durée du jeu en minutes

    public static bool openDoor = false;
    public static float conveyor_speed = 0.5f;
    
    void Update()
    {
        if (time > 0)
        {
            time -= Time.deltaTime;
        }
        else
        {
            SceneManager.LoadScene("GameOverScene");
        }
    }
}
