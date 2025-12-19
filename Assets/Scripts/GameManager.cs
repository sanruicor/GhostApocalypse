using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject redGhostPrefab;
    public GameObject pinkGhostPrefab;
    public GameObject superPower;
    public ScoreBoard scoreBoard;
    public LifeBoard lifeBoard;
    private AudioSource audioSource;
    public AudioClip spawnGhostClip;
    public AudioClip ghostHitClip;
    private bool hasPlayedGhostHit = false;
    private bool gameOver = false;
    public GameObject gameOverLabel;

    private int lifeCount = 4;
    private int score = 0;
    private bool hasSuperPower = false;
    private bool hasReachSuperPowerFlag = false;
    private int nextSuperPowerScore = 1000;

    private Vector3[] spawnPoints = { new Vector3(-10f, 2.45f, 0f),
                                      new Vector3(-10f, 1.66f, 0f),
                                      new Vector3(-10f, 0f, 0f),
                                      new Vector3(-10f, -1.24f, 0f),
                                      new Vector3(-10f, -3.14f, 0f)};


    public bool HasSuperPower => hasSuperPower;

    public bool GameOver => gameOver;

    void Awake()
    {
        instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        superPower.SetActive(false);
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver)
        {
            return;
        }

        if (Random.Range(0f, 1f) < 0.001f)
        {
            // Se debe espaunear un fantasma
            Vector3 spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            audioSource.PlayOneShot(spawnGhostClip);
            if (Random.Range(0f, 1f) < 0.1f)
            {
                Instantiate(pinkGhostPrefab, spawnPoint, Quaternion.identity);
            }
            else
            {
                Instantiate(redGhostPrefab, spawnPoint, Quaternion.identity);
            }
        }
    }

    void LateUpdate()
    {
        if (gameOver)
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                RestartGame();
            }
            
            return;
        }

        //Si el superpoder se ha alcanzado en el frame actual
        //(gracias a los puntos de los fantasmas destruidos con el superpoder)
        //(lo cual sabemos con la variable hasReachSuperPowerFlag)
        //no se debe quitar el superpower
        if (Input.GetKeyDown(KeyCode.Space) && hasSuperPower && !hasReachSuperPowerFlag)
        {
            hasSuperPower = false;
            superPower.SetActive(hasSuperPower);
        }

        hasReachSuperPowerFlag = false;
        hasPlayedGhostHit = false;
    }

    public void AddPoints(int points)
    {
        score += points;
        Debug.Log($"[GameManager] new score {score}");

        scoreBoard.SetScore(score);

        if (!hasPlayedGhostHit)
        {
            audioSource.PlayOneShot(ghostHitClip);
            hasPlayedGhostHit = true;
        }

        //Comprobamos si se alcanza el superPower
        if (score >= nextSuperPowerScore)
        {
            hasReachSuperPowerFlag = true;
            hasSuperPower = true;
            superPower.SetActive(hasSuperPower);
            nextSuperPowerScore += 2000;
        }
    }

    public void LooseLife()
    {
        lifeCount--;
        Debug.Log($"[GameManager] loose 1 life");

        lifeBoard.SetLifeCount(lifeCount);

        if (lifeCount <= 0)
        {
            gameOver = true;
            gameOverLabel.SetActive(true);
        }
    }

    private void RestartGame()
    {
        superPower.SetActive(false);
        hasSuperPower = false;
        nextSuperPowerScore = 1000;
        hasReachSuperPowerFlag = false;
        hasPlayedGhostHit = false;

        score = 0;
        scoreBoard.SetScore(0);

        lifeCount = 4;
        lifeBoard.SetLifeCount(lifeCount);

        gameOver = false;
        gameOverLabel.SetActive(false);
    }
}
