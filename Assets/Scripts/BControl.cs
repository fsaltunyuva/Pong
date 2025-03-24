using System;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;
public class BControl : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private int leftScore = 0, rightScore = 0;
    [SerializeField] private TextMeshProUGUI leftScoreText, rightScoreText;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI winningInfoText;
    [SerializeField] private UltraHardAIControl _ultraHardAIControlScript;
    private AudioSource audioSource;
    [SerializeField] private AudioClip paddleSFX, wallSFX, scoreSFX;


    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        Invoke("GoBall", 1);
    }

    void GoBall()
    {
        float rand = Random.Range(0, 2);
        if (rand < 1)
        {
            rb2d.AddForce(new Vector2(0.4f, -0.3f));
    
            if(_ultraHardAIControlScript != null)
                _ultraHardAIControlScript.MoveToInvokePosition();
            
        }
        else
        {
            rb2d.AddForce(new Vector2(-0.4f, -0.3f));
        }
    }

    void ResetBall()
    {
        rb2d.velocity = Vector2.zero;
        transform.position = Vector2.zero;
    }

    void RestartGame()
    {
        ResetBall();
        Invoke("GoBall", 1);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Inv. Walls Right"))
        {
            audioSource.PlayOneShot(scoreSFX);
            RestartGame();
            leftScore++;
            leftScoreText.text = leftScore.ToString();
            Debug.Log("Left Scored");
            if (leftScore == 11)
            {
                gameOverPanel.SetActive(true);
                winningInfoText.text = "Left Player Won The Game!";
                Time.timeScale = 0;
            }
        }
        else if (other.gameObject.CompareTag("Inv. Walls Left"))
        {
            audioSource.PlayOneShot(scoreSFX);
            RestartGame();
            rightScore++;
            rightScoreText.text = rightScore.ToString();
            Debug.Log("Right Scored");
            if (rightScore == 11)
            {
                gameOverPanel.SetActive(true);
                winningInfoText.text = "Right Player Won The Game!";
                Time.timeScale = 0;
            }
        }
        else if (other.gameObject.CompareTag("Inv. Walls Bottom") ||
                 other.gameObject.CompareTag("Inv. Walls Up"))
        {
            // Manually accelerate the ball after each hit
            // rb2d.velocity = rb2d.velocity.normalized * (rb2d.velocity.magnitude + 2);
            audioSource.PlayOneShot(wallSFX);
        }
        else if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("AI"))
        {
            audioSource.PlayOneShot(paddleSFX);
            // Manually accelerate the ball after each hit
            rb2d.velocity = rb2d.velocity.normalized * (rb2d.velocity.magnitude + 2);
        }
    }

    public void RestartScene()
    {
        leftScore = 0;
        rightScore = 0;
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void FixedUpdate()
    {
        if (rb2d.velocity.magnitude < 10) // Prevent the ball to get too slow
        {
            rb2d.velocity = rb2d.velocity.normalized * 10;
        }
        else if (rb2d.velocity.magnitude > 20) // Prevent the ball to get too fast
        {
            rb2d.velocity = rb2d.velocity.normalized * 20;
        }
    }
}