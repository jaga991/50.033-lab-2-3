using UnityEngine;

public interface IFoodObjParent
{
    public Transform GetFoodObjectFollowTransform();

    public void SetFoodObject(FoodObject foodObject);

    public FoodObject GetFoodObject();

    public void ClearFoodObject();
    public bool HasFoodObject();
}
