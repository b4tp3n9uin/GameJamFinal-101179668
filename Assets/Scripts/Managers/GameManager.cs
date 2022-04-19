using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    int score = 0;

    int randomNum;
    int lives;

    float timer;
    float initialTimer = 11.0f;

    public GameObject[] chocolates;
    public GameObject[] livesImg;

    [SerializeField]
    AIScript enemy;

    [Header("Texts:")]
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI ChocolateText;

    void Awake()
    {
        enemy = FindObjectOfType<AIScript>();
    }

    // Start is called before the first frame update
    void Start()
    {
        timer = initialTimer;

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

    public void ChocoCollide()
    {
        chocolates[randomNum].SetActive(false);
        score++;
        Debug.Log("score: " + score);

        ChocolateText.text = "Chocolates: " + score + " / " + chocolates.Length;

        if (score >= chocolates.Length)
        {
            Time.timeScale = 0;
            Debug.Log("Win");
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
                livesImg[0].SetActive(false);
                livesImg[1].SetActive(false);
                break;
            case 2:
                livesImg[0].SetActive(true);
                livesImg[1].SetActive(false);
                break;
            case 3:
                livesImg[0].SetActive(true);
                livesImg[1].SetActive(true);
                break;
            default:
                Time.timeScale = 0;
                Debug.Log("GAME OVER!!!!");
                break;
        }
    }

    public void Damage()
    {
        lives--;
        SetLivesImages();
    }
}
