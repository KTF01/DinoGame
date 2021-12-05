using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class DInosaurController : MonoBehaviour
{

    [SerializeField]
    private float jumpForce = 10;

    [SerializeField]
    public RectTransform gameOverPanel;

    public Text scoreText;

    ScoreManager sm;

    private GlobalManager globalManager;

    public Text finalScoreText;

    bool isGrounded = true;

    private Rigidbody myRigidBody;

    private Animator myAnimator;
    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody>();
        myAnimator = GetComponent<Animator>();
        sm = FindObjectOfType<ScoreManager>();
        globalManager = FindObjectOfType<GlobalManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.touchCount>0||(Input.GetKey(KeyCode.Space)))&&isGrounded && globalManager.isGameStarted)
        {
            myRigidBody.velocity = Vector3.up*jumpForce;
        }

        if (isGrounded)
            myAnimator.speed = globalManager.worldSpeed / 5;
        else myAnimator.speed = 1;
        
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGrounded = true;
            myAnimator.SetBool("isGrounded", isGrounded );
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGrounded = false;
            myAnimator.SetBool("isGrounded", isGrounded);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Cactus")
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        sm.UpdateHighscore();
        globalManager.isGameStarted = false;
        finalScoreText.text = $"Game Over!\nScore: {scoreText.text}\nBest score: {sm.highScore}";
        Time.timeScale = 0;
        gameOverPanel.gameObject.SetActive(true);
        scoreText.gameObject.SetActive(false);
    }
}
