using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    int score = 0;

    int randomNum;
    int lives;
    bool isPaused;

    float timer;
    float initialTimer = 11.0f;

    public GameObject[] chocolates;
    public GameObject[] livesImg;

    [SerializeField]
    AIScript enemy;

    [Header("Texts:")]
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI ChocolateText;

    [Header("Pannels")]
    public GameObject PausePannel;
    public GameObject LosePannel;
    public GameObject WinPannel;

    [Header("Audio")]
    public AudioSource Eat;
    public AudioSource Collide;
    public AudioSource Respawn;
    public AudioSource Win;
    public AudioSource Lose;
    public AudioSource open;

    void Awake()
    {
        enemy = FindObjectOfType<AIScript>();
    }

    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;

        timer = initialTimer;

        PausePannel.SetActive(false);
        LosePannel.SetActive(false);
        WinPannel.SetActive(false);

        for (int i = 0; i < chocolates.Length; i++)
        {
            chocolates[i].SetActive(false);
        }

        lives = 3;

        SetLivesImages();

        ChocolateText.text = "Chocolates: " + score + " / " + chocolates.Length;

        randomNum = Random.Range(0, chocolates.Length);
        chocolates[randomNum].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        Timer();
    }

    public void Pause()
    {
        // Pause
        if (!isPaused)
        {
            isPaused = true;
            Time.timeScale = 0;
            PausePannel.SetActive(true);
        }
        // Unpause
        else
        {
            isPaused = false;
            Time.timeScale = 1;
            PausePannel.SetActive(false);
        }
    }

    public void PlayAgainPressed()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void MenuPressed()
    {
        SceneManager.LoadScene("MenuScreen");
    }

    public void ChocoCollide()
    {
        Eat.Play();

        chocolates[randomNum].SetActive(false);
        score++;
        Debug.Log("score: " + score);

        ChocolateText.text = "Chocolates: " + score + " / " + chocolates.Length;

        if (score >= chocolates.Length)
        {
            Win.Play();
            Time.timeScale = 0;
            WinPannel.SetActive(true);
            return;
        }
        else 
        {
            timer = initialTimer;
            randomNum = Random.Range(0, chocolates.Length);
            chocolates[randomNum].SetActive(true);
            enemy.IncreaseSpeed();
        }
    }

    void Timer()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime * 1;

            timeText.text = "Time Till Spawn: " + (int)timer;
        }
        else
        {
            Respawn.Play();
            timer = initialTimer;

            chocolates[randomNum].SetActive(false);
            randomNum = Random.Range(0, chocolates.Length);
            chocolates[randomNum].SetActive(true);
        }
    }

    void SetLivesImages()
    {
        switch (lives)
        {
            case 1:
                Collide.Play();
                livesImg[0].SetActive(false);
                livesImg[1].SetActive(false);
                break;
            case 2:
                Collide.Play();
                livesImg[0].SetActive(true);
                livesImg[1].SetActive(false);
                break;
            case 3:
                open.Play();
                livesImg[0].SetActive(true);
                livesImg[1].SetActive(true);
                break;
            default:
                Lose.Play();
                Time.timeScale = 0;
                LosePannel.SetActive(true);
                break;
        }
    }

    public void Damage()
    {
        lives--;
        SetLivesImages();
    }
}
