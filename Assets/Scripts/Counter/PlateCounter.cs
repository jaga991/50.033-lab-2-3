using UnityEngine;

public class PlateCounter : BaseCounter
{
    [SerializeField] private FoodObjectSO plateFoodObjectSO;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Interact(Player player)
    {
        if (!player.HasFoodObject())
        {
            FoodObject.SpawnFoodObject(plateFoodObjectSO, player);
            //
        }
    }

}
