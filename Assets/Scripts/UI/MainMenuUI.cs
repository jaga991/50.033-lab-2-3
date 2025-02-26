using System.Collections;
using System.Threading;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{

    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private GameStateSO gameStateSO;
    [SerializeField] private TextMeshProUGUI highScoreText;
    private void Awake()
    {
        playButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.SampleScene);
        });

        quitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });

        //Display High Score
        UpdateHighScoreText();
    }

    private void UpdateHighScoreText()
    {
        highScoreText.text = $"High Score: {gameStateSO.highScore}";
    }

}
