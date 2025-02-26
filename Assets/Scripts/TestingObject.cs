using UnityEngine;

public class TestingObject : MonoBehaviour
{
    public static TestingObject Instance { get; private set; }

    [SerializeField] private TestingObjectSO testingData; // Assign in Inspector

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // Persist across scenes
    }

    private void Start()
    {
        SetTestValue(999);
    }

    public static void EnsureInstanceExists()
    {
        if (Instance == null)
        {
            GameObject obj = new GameObject("TestingObject");
            Instance = obj.AddComponent<TestingObject>();

            // Ensure SO is assigned (optional)
            TestingObjectSO testingData = Resources.Load<TestingObjectSO>("TestingData");
            if (testingData != null)
            {
                Instance.testingData = testingData;
            }

            DontDestroyOnLoad(obj);
            Debug.Log("Created TestingObject dynamically!");
        }
    }

    public int GetTestValue()
    {
        return testingData != null ? testingData.testValue : 0;
    }

    public void SetTestValue(int newValue)
    {
        if (testingData != null)
        {
            testingData.testValue = newValue;
        }
    }
}
