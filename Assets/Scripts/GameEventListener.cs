using UnityEngine;
using UnityEngine.Events;

public class GameEventListener<T> : MonoBehaviour
{
    [SerializeField] public GameEvent<T> gameEvent;
    [SerializeField] public UnityEvent<T> response; // Triggers a UI update or other response

    private void OnEnable()
    {
        if (gameEvent != null)
            gameEvent.RegisterListener(this);
    }

    private void OnDisable()
    {
        if (gameEvent != null)
            gameEvent.UnregisterListener(this);
    }

    public void OnEventRaised(T data)
    {
        response.Invoke(data); // Calls the assigned UnityEvent function
    }

    //  Allows other scripts to add responses dynamically
    public void AddResponse(UnityAction<T> callback)
    {
        if (response != null)
            response.AddListener(callback);
    }
}
