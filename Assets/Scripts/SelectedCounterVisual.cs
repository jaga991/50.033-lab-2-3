using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private ClearCounter clearCounter;
    [SerializeField] private GameObject visualGameObject; 


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //call methods on singleton player instance
        Player.Instance.OnSelectedCounterChange += Player_OnSelectedCounterChange;
    }

    private void Player_OnSelectedCounterChange(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        if (e.selectedCounter == clearCounter) {
            Hide();
        }
        else
        {
            Show();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Show()
    {
        visualGameObject.SetActive(true);
    }

    private void Hide() { visualGameObject.SetActive(false); }
}
