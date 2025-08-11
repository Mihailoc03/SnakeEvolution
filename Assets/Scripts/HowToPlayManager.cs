using UnityEngine;

public class HowToPlayManager : MonoBehaviour
{
    public GameObject howToPlayPanel;
    public GameObject gameUI;

    private bool isFirstTime = true;

    private void Start()
    {
        if (isFirstTime)
        {
            ShowHowToPlay();
            isFirstTime = false;
        }
        else
        {
            CloseHowToPlay();
        }
    }

    public void ShowHowToPlay()
    {
        howToPlayPanel.SetActive(true);
        gameUI.SetActive(false);
        Time.timeScale = 0f;
    }

    public void CloseHowToPlay()
    {
        howToPlayPanel.SetActive(false);
        gameUI.SetActive(true);
        Time.timeScale = 1f;
    }
}
