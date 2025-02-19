using UnityEngine;

public class ClearCounter : BaseCounter //inherit
{
   
    [SerializeField] private FoodObjectSO foodObjectSO;
    private void Update()
    {

    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Interact(Player player)
    {
        if (!HasFoodObject())   //no food object on counter
        {

            if (player.HasFoodObject())
            {
                player.GetFoodObject().SetFoodObjectParent(this);
                this.GetFoodObject().GetComponentInChildren<SpriteRenderer>().sortingOrder = 5;
            }
            else
            {
                //player not carrying anything
            }
        }
        else
        {
            //have food object here
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
                else
                    //player is not holding a plate but something else
                {
                    if (GetFoodObject().TryGetPlate(out plateFoodObject))
                    {
                        //counter is holding a plate
                        if(plateFoodObject.TryAddIngredient(player.GetFoodObject().GetFoodObjectSO()))
                        {
                            player.GetFoodObject().DestroySelf();
                        }
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
}
