using UnityEngine;
using System;
using System.Collections.Generic;

[CreateAssetMenu()]
public class CuttingRecipeSO : ScriptableObject
{
    public FoodObjectSO input;
    public FoodObjectSO output;
    public int cuttingProgressMax;
}
