using TMPro;
using UnityEngine;

public class GameStartCountdownUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countdownText;
    [SerializeField] private GameEventListenerGameState gameStateListener;
    [SerializeField] private GameStateSO gameStateSO;

    private void Start()
    {
        gameStateListener.response.AddListener(UpdateUI);
    }

    private void UpdateUI(GameStateSO.State state)
    {
        if (state == GameStateSO.State.CountdownToStart)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        countdownText.text = Mathf.Ceil(gameStateSO.countdownToStartTimer).ToString("#.##");
    }
}
