using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PlateIconsUI : MonoBehaviour
{
    [SerializeField] private PlateFoodObject plateFoodObject;
    [SerializeField] private Transform iconTemplate;


    private void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
    }
    private void Start()
    {
        plateFoodObject.OnIngredientAdded += PlateFoodObject_OnIngredientAdded;
    }

    private void PlateFoodObject_OnIngredientAdded(object sender, PlateFoodObject.OnIngredientAddedEventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {

        foreach(Transform child in transform)
        {
            if (child == iconTemplate) continue;
            Destroy(child.gameObject);
        }
        foreach (FoodObjectSO foodObjectSO in plateFoodObject.GetFoodObjectSOList())
        {
            Transform iconTransform = Instantiate(iconTemplate, transform); //make sure to include transform to reference parent object
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<PlateIconsSingleUI>().SetFoodObjectSO(foodObjectSO);
            //
        }
    }
}
 