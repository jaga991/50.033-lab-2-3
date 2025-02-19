using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class PlateFoodObject : FoodObject
{
    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;
    public class OnIngredientAddedEventArgs:EventArgs
    {
        public FoodObjectSO foodObjectSO;
    }
    [SerializeField] private List<FoodObjectSO> validFoodObjectSOList;
    private List<FoodObjectSO> foodObjectSOList;


    private void Awake()
    {
        foodObjectSOList = new List<FoodObjectSO>();
    }
    public bool TryAddIngredient(FoodObjectSO foodObjectSO)
    {
        if(!validFoodObjectSOList.Contains(foodObjectSO))
        {
            return false;
        }
        if (foodObjectSOList.Contains(foodObjectSO))
        //check if plate already contains ingredient, prevent duplicate
        {
            Debug.Log("Cant add, duplicate " + foodObjectSOList);
            return false;
        }
        else
        {
            foodObjectSOList.Add(foodObjectSO);
            OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs
            {
                foodObjectSO = foodObjectSO,
            });
            return true;

        }


    }

    public List<FoodObjectSO> GetFoodObjectSOList()
    {
        return foodObjectSOList;
    }
}
