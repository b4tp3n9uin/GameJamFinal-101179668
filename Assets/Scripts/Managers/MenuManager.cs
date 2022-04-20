using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Pannels:")]
    public GameObject HtpPannel;
    public GameObject CreditsPannel;

    public GameObject Buttons;
    void Start()
    {
        Buttons.SetActive(true);

        HtpPannel.SetActive(false);
        CreditsPannel.SetActive(false);
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void HowToPlay()
    {
        Buttons.SetActive(false);
        HtpPannel.SetActive(true);
    }

    public void Credits()
    {
        Buttons.SetActive(false);
        CreditsPannel.SetActive(true);
    }

    public void Close()
    {
        Buttons.SetActive(true);

        HtpPannel.SetActive(false);
        CreditsPannel.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();

        Debug.Log("Quit");
    }

}
