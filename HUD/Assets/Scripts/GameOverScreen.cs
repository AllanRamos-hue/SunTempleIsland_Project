using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public GameObject gameOver;
    PlayerLife player;
    public static int round = 3;

    private void Start()
    {
        player = FindObjectOfType<PlayerLife>();
    }
    void Update()
    {
        if (player.Die()) --round;

        if (round == 0) gameOver.SetActive(true);

    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

}
