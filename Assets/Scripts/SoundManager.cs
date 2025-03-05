using UnityEngine;
using UnityEngine.Audio;


public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClipRefsSO audioClipRefsSO;
    [SerializeField] private AudioMixer audioMixer;

    public static SoundManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        EventManager.Instance.Subscribe("RecipeFailed", DeliveryManager_OnRecipeFailed);
        EventManager.Instance.Subscribe("RecipeSuccess", DeliveryManager_OnRecipeSuccess);
        EventManager.Instance.Subscribe("PlayerPickedSomething", Player_OnPickedSomething);
        EventManager.Instance.Subscribe("ObjectPlaced", BaseCounter_OnAnyObjectPlacedHere);
        EventManager.Instance.Subscribe("ObjectTrashed", TrashCounter_OnAnyObjectTrashed);
        EventManager.Instance.Subscribe("ObjectCut", CuttingCounter_OnAnyObjectCut);
    }

    private void OnDestroy()
    {
        // Unsubscribe to prevent memory leaks
        if (EventManager.Instance != null)
        {
            EventManager.Instance.Unsubscribe("RecipeFailed", DeliveryManager_OnRecipeFailed);
            EventManager.Instance.Unsubscribe("RecipeSuccess", DeliveryManager_OnRecipeSuccess);
            EventManager.Instance.Unsubscribe("PlayerPickedSomething", Player_OnPickedSomething);
            EventManager.Instance.Unsubscribe("ObjectPlaced", BaseCounter_OnAnyObjectPlacedHere);
            EventManager.Instance.Unsubscribe("ObjectTrashed", TrashCounter_OnAnyObjectTrashed);
            EventManager.Instance.Unsubscribe("ObjectCut", CuttingCounter_OnAnyObjectCut);
        }
    }

    private void CuttingCounter_OnAnyObjectCut(object sender)
    {
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        PlaySound(audioClipRefsSO.chop, cuttingCounter.transform.position);
    }

    private void TrashCounter_OnAnyObjectTrashed(object sender)
    {
        TrashCounter trashCounter = sender as TrashCounter;
        PlaySound(audioClipRefsSO.trash, trashCounter.transform.position);
    }

    private void BaseCounter_OnAnyObjectPlacedHere(object sender)
    {
        BaseCounter baseCounter = sender as BaseCounter;
        PlaySound(audioClipRefsSO.objectDrop, baseCounter.transform.position);
    }

    private void Player_OnPickedSomething(object sender)
    {
        PlaySound(audioClipRefsSO.objectPickup, Player.Instance.transform.position);
    }

    private void DeliveryManager_OnRecipeSuccess(object sender)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(audioClipRefsSO.deliverySuccess, deliveryCounter.transform.position);
    }

    private void DeliveryManager_OnRecipeFailed(object sender)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(audioClipRefsSO.deliveryFail, deliveryCounter.transform.position);
    }

    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volume = 1f)
    {
        if (audioClipArray.Length > 0)
        {
            AudioClip clip = audioClipArray[Random.Range(0, audioClipArray.Length)];
            PlaySound(clip, position, volume);
        }
    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volume = 1f)
    {
        if (audioClip != null)
        {
            AudioSource.PlayClipAtPoint(audioClip, position, volume);
        }
    }
}

