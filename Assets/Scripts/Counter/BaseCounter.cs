using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
public class BaseCounter:MonoBehaviour, IFoodObjParent
{


    [SerializeField] public Transform counterItem;
    private FoodObject foodObject;

    public static event EventHandler OnAnyObjectPlacedHere;
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
        if (foodObject != null)
        {
            OnAnyObjectPlacedHere?.Invoke(this, EventArgs.Empty);
        }
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
