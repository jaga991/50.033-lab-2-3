using UnityEngine;

public class ClearCounter : MonoBehaviour, IKitchenObjParent //interface
{
   



    [SerializeField] private FoodObjectSO foodObjectSO;
    [SerializeField] private Transform counterItem;
    [SerializeField] private ClearCounter secondClearCounter;
    [SerializeField] private bool testing;

    private FoodObject foodObject;
    private void Update()
    {
        if (testing && Input.GetKeyDown(KeyCode.T))
        {
            if (foodObject != null)
            {
                foodObject.SetClearCounter(secondClearCounter);
            }
        }
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Interact()
    {
        if (foodObject == null)
        {
            //spawn food object
            AudioManager.instance.PlaySFX(); // Plays defaultSFX

            Transform foodObjectTransform = Instantiate(foodObjectSO.prefab, counterItem);
            foodObjectTransform.GetComponent<FoodObject>().SetClearCounter(this);
        } else
        {
            //give object to player
            
        }

        

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
