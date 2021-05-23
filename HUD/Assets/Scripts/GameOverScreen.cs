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
        if (round == 0)
        {
            GameOver();
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        round = 3;
    }

    public void ReturnMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    void GameOver()
    {
        Cursor.lockState = CursorLockMode.Confined;
        
        gameOver.SetActive(true);

        EnemyTurret[] turrets = FindObjectsOfType<EnemyTurret>();
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        EnemyPatrol[] patrols = FindObjectsOfType<EnemyPatrol>();

        foreach (EnemyTurret t in turrets)
        {
            t.enabled = false;
        }

        foreach (Enemy e in enemies)
        {
            e.enabled = false;
        }

        foreach (EnemyPatrol p in patrols)
        {
            p.enabled = false;
        }

    }
}
