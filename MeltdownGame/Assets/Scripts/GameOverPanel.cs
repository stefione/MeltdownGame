using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class GameOverPanel : MonoBehaviourSingleton<GameOverPanel>
{
    [SerializeField] TextMeshProUGUI _GameOverText;
    [SerializeField] TextMeshProUGUI _PlacementText;
    public void Open(string text,int placement)
    {
        _GameOverText.text = text;
        _PlacementText.text="Placement: "+placement;
        gameObject.SetActive(true);
    }

    public void Button_PlayAgain()
    {
        SceneManager.LoadScene(Constants.GameScene);
    }

    public void Button_ReturnToMenu()
    {
        SceneManager.LoadScene(Constants.MainMenuScene);
    }
}
