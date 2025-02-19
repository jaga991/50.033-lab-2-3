using UnityEngine;

[CreateAssetMenu(fileName = "FryingRecipeSO", menuName = "Scriptable Objects/FryingRecipeSO")]
public class FryingRecipeSO : ScriptableObject
{
    public FoodObjectSO input;
    public FoodObjectSO output;
    public float fryingTimerMax;
}
