using UnityEngine;

public class FoodObject : MonoBehaviour
{
    [SerializeField] private FoodObjectSO foodObjectSO;

    private ClearCounter clearCounter;

    public FoodObjectSO GetFoodObjectSO() { return foodObjectSO; }
    public void SetClearCounter(ClearCounter clearCounter)
    {
        if (this.clearCounter != null) {
            this.clearCounter.ClearFoodObject();
        }
        this.clearCounter = clearCounter;
        if (clearCounter.HasFoodObject()) {
            Debug.Log("Counter already has FoodObjet");
        }
        clearCounter.SetFoodObject(this);
        transform.parent = clearCounter.GetFoodObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }

    public ClearCounter GetClearCounter() { return clearCounter; }
}
