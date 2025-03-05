using System;
using UnityEngine;

public class ContainerCounter : BaseCounter
{

    [SerializeField] private FoodObjectSO foodObjectSO;

    private void Awake()
    {
        counterItem.GetComponent<SpriteRenderer>().sprite = foodObjectSO.sprite;
        counterItem.GetComponent<SpriteRenderer>().sortingOrder = 5;
    }
    private void Start()
    {
        
    }

    public override void Interact(Player player)
    {
        if (!HasFoodObject() && !player.HasFoodObject())
        {
            //spawn food object
            EventManager.Instance.TriggerEvent("PlayerPickedSomething");
            FoodObject.SpawnFoodObject(foodObjectSO, player);
        }
    }
}
