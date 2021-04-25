using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    PlayerController pController;
    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public Text scoreText;
    public Text gameOverText;
    public Button specialButton;
    public Button restartButton;
    public RawImage gameOverImage;
    public float waitOnDeath;
    private int score;
    private Color curColor;
    public float fadeRate;

    public AudioSource backgroundMusic;
    public AudioSource gameOverSound;
    private bool restart;

    private void Start()
    {
        restart = false;
        score = 0;
        UpdateScore();
        StartCoroutine("SpawnVawes");
        gameOverText.text = "";
        restartButton.gameObject.SetActive(false);
        curColor = gameOverImage.color;
        curColor.a = 0.0f;
        gameOverImage.color = curColor;
        //Camera.main.orthographicSize = Mathf.Max(Screen.width, Screen.height) / 2;
        restartButton.onClick.AddListener(Restart);
    }
    IEnumerator SpawnVawes()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            if (restart == false)
            {
                for (int i = 0; i < hazardCount; i++)
                {
                    GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                    Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                    Quaternion spawnRotation = Quaternion.identity;
                    Instantiate(hazard, spawnPosition, spawnRotation);
                    yield return new WaitForSeconds(spawnWait);
                }
            }
            yield return new WaitForSeconds(waveWait);
        }
        
    }
    public void AddScore(int scoreValue)
    {
        score += scoreValue;
        UpdateScore();
    }
    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }
    public IEnumerator GameOver()
    {
        backgroundMusic.Stop();
        yield return new WaitForSeconds(waitOnDeath);
        scoreText.text = "";
        specialButton.gameObject.SetActive(false);
        gameOverSound.Play();
        while ((1.0f - curColor.a) >= 0.01f)
        {
            curColor.a += fadeRate;
            gameOverImage.color = curColor;
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.5f);
        gameOverText.text = "Score: " + score;
        restartButton.gameObject.SetActive(true);
        restart = true;
    }
    public void Restart()
    {
            SceneManager.LoadScene("Main");
    }
}
