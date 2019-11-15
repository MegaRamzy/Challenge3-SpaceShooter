using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public Text ScoreText;
    public Text RestartText;
    public Text GameOverText;
    public Text winText;
    private bool gameWin;
    private bool gameOver;
    private bool restart;
    private int score;

    
    void Start()
    {
        gameWin = false;
        gameOver = false;
        restart = false;
        GameOverText.text = "";
        RestartText.text = "";
        winText.text = "";
        score = 0;
        UpdateScore();
        StartCoroutine(SpawnWaves());
    }

    private void Update()
    {
        if (restart)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene("SampleScene");
            }
        }

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }
    IEnumerator SpawnWaves()
    {
        while (true)
        {
            yield return new WaitForSeconds(startWait);
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            if (gameOver || gameWin)
            {
                RestartText.text = "Press 'SpaceBar' for Restart";
                restart = true;
                break;
            }
        }
    }

    public void AddScore (int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    public void UpdateScore()
    {
        ScoreText.text = "Points: " + score;
        if (score >= 100)
        {
            winText.text = "You Win! \nGame Created By David Kingsely!";
            gameWin = true;
            restart = true;

        }
    }

    public void Gameover()
    {
        gameOver = true;

        if (gameWin == true)
        {
            GameOverText.text ="";
        }

        else
        {
            GameOverText.text = "GameOver!";
        }
    }
}
