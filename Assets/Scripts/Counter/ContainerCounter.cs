using UnityEngine;

public class ContainerCounter : BaseCounter
{

    [SerializeField] private FoodObjectSO foodObjectSO;

    public override void Interact(Player player)
    {
        if (!HasFoodObject() && !player.HasFoodObject())
        {
            //spawn food object

            FoodObject.SpawnFoodObject(foodObjectSO, player);
        }
    }
}
