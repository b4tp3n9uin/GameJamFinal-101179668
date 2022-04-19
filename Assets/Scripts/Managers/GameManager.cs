using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    int score = 0;

    int randomNum;

    public GameObject[] chocolates;

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
        for (int i = 0; i < chocolates.Length; i++)
        {
            chocolates[i].SetActive(false);
        }

        ChocolateText.text = "Chocolates: " + score + " / " + chocolates.Length;

        randomNum = Random.Range(0, chocolates.Length);
        chocolates[randomNum].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
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
            randomNum = Random.Range(0, chocolates.Length);
            chocolates[randomNum].SetActive(true);
            enemy.IncreaseSpeed();
        }
    }
}
