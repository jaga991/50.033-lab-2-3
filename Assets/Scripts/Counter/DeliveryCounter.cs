using UnityEngine;

public class DeliveryCounter : BaseCounter
{

    public static DeliveryCounter Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
    public override void Interact(Player player)
    {
        if (player.HasFoodObject())
        {
            //make sure has plate before agree to receive
            if(player.GetFoodObject().TryGetPlate(out PlateFoodObject plateFoodObject))
            {
                DeliveryManager.Instance.DeliverRecipe(plateFoodObject);

                player.GetFoodObject().DestroySelf();

            }
        }
    }
}
