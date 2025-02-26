using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private Transform recipeTemplate;

    [SerializeField] private GameEventListener<RecipeSO> recipeSpawnedListener;
    [SerializeField] private GameEventListener<bool> recipeCompletedListener;

    private void Awake()
    {
        recipeTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        recipeSpawnedListener.response.AddListener(HandleRecipeSpawned);
        recipeCompletedListener.response.AddListener(HandleRecipeCompleted);
    }

    private void HandleRecipeSpawned(RecipeSO recipe)
    {
        UpdateVisual();
    }

    private void HandleRecipeCompleted(bool success)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        // Destroy previous UI elements safely
        for (int i = container.childCount - 1; i >= 0; i--)
        {
            Transform child = container.GetChild(i);
            if (child == null || child == recipeTemplate) continue;
            Destroy(child.gameObject);
        }

        // Recreate UI elements from the updated recipe list
        foreach (RecipeSO recipeSO in DeliveryManager.Instance.GetWaitingRecipeSOList())
        {
            Transform recipeTransform = Instantiate(recipeTemplate, container);
            recipeTransform.gameObject.SetActive(true);
            recipeTransform.GetComponent<DeliveryManagerSingleUI>().SetRecipeSO(recipeSO);
        }
    }
}
