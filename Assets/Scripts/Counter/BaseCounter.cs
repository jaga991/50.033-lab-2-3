using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class BaseCounter:MonoBehaviour, IFoodObjParent
{


    [SerializeField] private Transform counterItem;
    private FoodObject foodObject;

    public virtual void Interact(Player player)
    {
        Debug.LogError("BaseCounter.Interact(), should not be triggered here");
    }

    public virtual void InteractAlternate(Player player)
    {
        Debug.LogError("BaseCounter.InteractAlternate(), should not be trigered here");
    }

    public Transform GetFoodObjectFollowTransform()
    {
        return counterItem;
    }

    public void SetFoodObject(FoodObject foodObject)
    {
        this.foodObject = foodObject;
    }

    public FoodObject GetFoodObject()
    {
        return foodObject;
    }

    public void ClearFoodObject()
    {
        foodObject = null;
    }

    public bool HasFoodObject()
    {
        return foodObject != null;
    }
}
