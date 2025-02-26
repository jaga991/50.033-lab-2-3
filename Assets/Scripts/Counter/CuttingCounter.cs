using System;
using UnityEngine;

public class CuttingCounter : BaseCounter, IHasProgress
{
    public static event EventHandler OnAnyObjectCut;

    //create a event for onprogresschange
    public event EventHandler<IHasProgress.OnProgressChangeEventArgs> OnProgressChanged;
    public class OnProgressChangeEventArgs: EventArgs
    {
        public float ProgressNormalized;
    }

    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;

    private int cuttingProgress;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Interact(Player player)
    {
        if (!HasFoodObject())   //no food object on counter
        {

            if (player.HasFoodObject())
            {
                if (HasRecipeWithInput(player.GetFoodObject().GetFoodObjectSO()))
                {
                    player.GetFoodObject().SetFoodObjectParent(this);
                    this.GetFoodObject().GetComponentInChildren<SpriteRenderer>().sortingOrder = 5;

                    //start cutting progress
                    cuttingProgress = 0;
                    CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetFoodObject().GetFoodObjectSO());

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangeEventArgs
                    {
                        ProgressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax

                    });
                    
                }

            }
            else
            {
                //player not carrying anything
            }
        }
        else
        {
            //hve food object here
            if (player.HasFoodObject())
            {
                //Player carrying something
                if (player.GetFoodObject().TryGetPlate(out PlateFoodObject plateFoodObject))
                {
                    //player holding a plate
                    if (plateFoodObject.TryAddIngredient(GetFoodObject().GetFoodObjectSO()))
                    //if succeed add ingredient, can destroy itself
                    {
                        GetFoodObject().DestroySelf();
                    }

                }
            }
            else
            {
                //player not carrying anything
                GetFoodObject().SetFoodObjectParent(player);
            }
        }
    }

    public override void InteractAlternate(Player player)
    {
        if(HasFoodObject() && HasRecipeWithInput(GetFoodObject().GetFoodObjectSO())) 
            //has foodObject here, and can be cut
        {
            cuttingProgress++;
            OnAnyObjectCut?.Invoke(this, EventArgs.Empty);


            CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetFoodObject().GetFoodObjectSO());
            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangeEventArgs
            {
                ProgressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax

            });
            if (cuttingProgress >= cuttingRecipeSO.cuttingProgressMax)
            {
                FoodObjectSO outputFoodObjectSO = GetOutputForInput(GetFoodObject().GetFoodObjectSO());
                GetFoodObject().DestroySelf();
                FoodObject.SpawnFoodObject(outputFoodObjectSO, this);
                //increment cutting progress
            }

        }
    }

    private bool HasRecipeWithInput(FoodObjectSO inputFoodObjectSO)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputFoodObjectSO);
        return cuttingRecipeSO != null;
    }
    private FoodObjectSO GetOutputForInput(FoodObjectSO inputFoodObjectSO)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputFoodObjectSO);
        if(cuttingRecipeSO != null)
        {
            return cuttingRecipeSO.output;
        } else
        {
            return null;
        }
    }

    private CuttingRecipeSO GetCuttingRecipeSOWithInput(FoodObjectSO inputFoodObjectSO)
    {
        //check if inputFoodObjectSO exist in any of the CuttingRecipeSOArr inputs

        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray)
        {
            if (cuttingRecipeSO.input == inputFoodObjectSO)
            {
                return cuttingRecipeSO; 
            }
        }
        return null;
    }
         
}
