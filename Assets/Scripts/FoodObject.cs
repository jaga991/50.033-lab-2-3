using UnityEngine;

public class FoodObject : MonoBehaviour
{
    [SerializeField] private FoodObjectSO foodObjectSO;

    private IFoodObjParent foodObjectParent;

    public FoodObjectSO GetFoodObjectSO() { return foodObjectSO; }
    public void SetFoodObjectParent(IFoodObjParent foodObjParent)
    {
        if (this.foodObjectParent != null) {
            this.foodObjectParent.ClearFoodObject();
        }
        this.foodObjectParent = foodObjParent;  
        if (foodObjectParent.HasFoodObject()) {
            Debug.Log("IFoodObjectParent already has FoodObjet");
        }
        foodObjectParent.SetFoodObject(this);
        transform.parent = foodObjectParent.GetFoodObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }

    public IFoodObjParent GetFoodObjectParent() { return foodObjectParent; }

    public void DestroySelf()
    {
        //clear the parent of food object
        foodObjectParent.ClearFoodObject();
        //delete food object
        Destroy(gameObject);
    }

    public bool TryGetPlate(out PlateFoodObject plateFoodObject)
    {
        if (this is PlateFoodObject)
        {
            plateFoodObject = this as PlateFoodObject;
            return true;
        }
        else
        {
            plateFoodObject=null;
            return false;
        }
    }


    public static FoodObject SpawnFoodObject(FoodObjectSO foodObjectSO, IFoodObjParent foodObjParent)
    {
        Transform foodObjectTransform = Instantiate(foodObjectSO.prefab);
        FoodObject foodObject = foodObjectTransform.GetComponent<FoodObject>();
        foodObject.SetFoodObjectParent(foodObjParent);
        foodObjectTransform.GetComponentInChildren<SpriteRenderer>().sortingOrder = 5;

        return foodObject;

    }
}
