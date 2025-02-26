using UnityEngine;
using UnityEngine.UI;

public class PlateIconsSingleUI : MonoBehaviour
{
    [SerializeField] private Image image;
    public void SetFoodObjectSO(FoodObjectSO foodObjectSO)
    {
        image.sprite = foodObjectSO.sprite;
    }
}
