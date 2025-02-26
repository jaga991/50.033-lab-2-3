using UnityEngine;

[CreateAssetMenu(fileName = "CounterSO", menuName = "Scriptable Objects/CounterSO")]
public class CounterSO : ScriptableObject
{
    public GameObject counterPrefab;
    public Vector3 counterPosition;
    public FoodObjectSO foodObjectSO;
    public int cuttingState;
    public float cookingTimer;
}


