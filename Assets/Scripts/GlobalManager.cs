using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalManager : MonoBehaviour
{
    public float initialWorldSpeed;
    public float speedIncreaseRate;
    public float menuWorldSpeed;
    public float worldSpeedCap;
    [HideInInspector]
    public float worldSpeed;

    [HideInInspector]
    public bool isGameStarted;

    public mesh_gen background;

    public GameObject menuUI;
    public GameObject scoreText;
    public GameObject gameOverPanel;

    private int bgGeneratorCounter = 1;
    private ScoreManager scoreManager;
    private CactusSpawner cactusSpawner;
    private mesh_gen instance1;
    // Start is called before the first frame update
    void Start()
    {
        background.amplitude = 2.17f;
        background.zMap = 6.58f;
        background.xOffset = 0;
        instance1 = Instantiate(background, new Vector3(-30, 0.5f, -2.8f), Quaternion.identity);
        scoreManager = FindObjectOfType<ScoreManager>();
        cactusSpawner = FindObjectOfType<CactusSpawner>();
        worldSpeed = menuWorldSpeed;
    }

    private void instantiateNewBg()
    {
        background.xOffset = 30*bgGeneratorCounter;
        instance1 = Instantiate(background, new Vector3(instance1.transform.position.x+ background.xSize, 0.5f, -2.8f), Quaternion.identity);
        bgGeneratorCounter++;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (instance1.transform.position.x <= -30)
        {
            instantiateNewBg();
        }
        if(worldSpeed< worldSpeedCap)
        worldSpeed += speedIncreaseRate;
    }

    public void OnStartGame()
    {
        worldSpeed = initialWorldSpeed;
        isGameStarted = true;
        menuUI.SetActive(false);
        scoreText.SetActive(true);
        scoreManager.StartScoreCount();
        cactusSpawner.StartSpawn();
    }

    public void OnRestartGame()
    {
        isGameStarted = true;
        Time.timeScale = 1;
        worldSpeed = initialWorldSpeed;
        scoreManager.ResetScore();
        gameOverPanel.SetActive(false);
        scoreText.SetActive(true);
        cactusSpawner.PuregCactuses();
    }
}
