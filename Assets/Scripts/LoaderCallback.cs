using UnityEngine;

public class LoaderCallback:MonoBehaviour
{
    private bool isFirstUpdate = true;
    private float timer = 10f;
    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            if (!isFirstUpdate)
            {
                isFirstUpdate = false;

                Loader.LoaderCallback();
            }
        }

    }
}
