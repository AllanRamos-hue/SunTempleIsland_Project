using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    private PlayerLife player;
    public GameObject gameOver;


    void Start()
    {
      player = GetComponent<PlayerLife>();
    }

    void Update()
    {
        if (player.Die() == true) 
        {
            gameOver.SetActive(true);

            Cursor.lockState = CursorLockMode.None;
        }
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
