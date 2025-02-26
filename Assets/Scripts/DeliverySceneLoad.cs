using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeliverySceneLoad : MonoBehaviour
{


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Break();
        Debug.Log("Collision detected with: " + collision.gameObject.name); // Debug message

        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player detected! Loading scene..."); // Confirm Player collision
            SceneManager.LoadScene(2);
        }
    }
}
