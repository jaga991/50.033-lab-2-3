using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class DeliveryManager : MonoBehaviour
{
    public static DeliveryManager Instance { get; private set; }

    [SerializeField] private RecipeListSO recipeListSO;
    [SerializeField] private GameStateSO gameStateSO;

    [SerializeField] private RecipeSpawnedEvent recipeSpawnedEvent;
    [SerializeField] private RecipeCompletedEvent recipeCompletedEvent;
    [SerializeField] private RecipeSuccessEvent recipeSuccessEvent;
    [SerializeField] private RecipeFailedEvent recipeFailedEvent;

    [SerializeField] private GameEventListenerGameState gameStateListener;

    private List<RecipeSO> waitingRecipeSOList;

    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;
    private int waitingRecipesMax = 4;

    private void Start()
    {
        if (gameStateListener != null)
            gameStateListener.AddResponse(RestartUI);
    }

    private void RestartUI(GameStateSO.State arg0)
    {
        Debug.Log("IN DELIVER MANAGER, RESTARTING UI PRINT LOG");
    }

 

    private void Awake()
    {
        Instance = this;
        waitingRecipeSOList = new List<RecipeSO>();
    }

    private void Update()
    {
        spawnRecipeTimer -= Time.deltaTime;
        if (spawnRecipeTimer <= 0f)
        {
            spawnRecipeTimer = spawnRecipeTimerMax;

            if (waitingRecipeSOList.Count < waitingRecipesMax)
            {
                RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[UnityEngine.Random.Range(0, recipeListSO.recipeSOList.Count)];
                waitingRecipeSOList.Add(waitingRecipeSO);
                recipeSpawnedEvent.Raise(waitingRecipeSO);
            }
        }
    }

    public void DeliverRecipe(PlateFoodObject plateFoodObject)
    {
        for (int i = 0; i < waitingRecipeSOList.Count; i++)
        {
            RecipeSO waitingRecipeSO = waitingRecipeSOList[i];

            if (waitingRecipeSO.foodObjectSOList.Count == plateFoodObject.GetFoodObjectSOList().Count)
            {
                bool plateMatchesRecipe = true;
                foreach (FoodObjectSO recipeFoodObjectSO in waitingRecipeSO.foodObjectSOList)
                {
                    bool ingredientFound = false;
                    foreach (FoodObjectSO plateFoodObjectSO in plateFoodObject.GetFoodObjectSOList())
                    {
                        if (plateFoodObjectSO == recipeFoodObjectSO)
                        {
                            ingredientFound = true;
                            break;
                        }
                    }

                    if (!ingredientFound)
                    {
                        plateMatchesRecipe = false;
                    }
                }

                if (plateMatchesRecipe)
                {
                    EventManager.Instance.TriggerEvent("RecipeSuccess");

                    gameStateSO.currentScore++;
                    waitingRecipeSOList.RemoveAt(i);
                    recipeCompletedEvent.Raise(true); // Recipe completed successfully
                    recipeSuccessEvent.Raise(gameStateSO.currentScore);
                    return;
                }
            }
        }

        Debug.Log("Player delivered an incorrect recipe");
        EventManager.Instance.TriggerEvent("RecipeFailed");
        recipeFailedEvent.Raise(gameStateSO.currentScore); // Notify UI of failed delivery
    }

    public List<RecipeSO> GetWaitingRecipeSOList()
    {
        return waitingRecipeSOList;
    }

    public int GetSuccessfulRecipesAmount()
    {
        return gameStateSO.currentScore;
    }
}
