using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{

    public static int targetSceneIndex;

    public enum Scene
    {
        MainMenuScene,
        SampleScene,
        ServingScene,
        LoadingScene
    }

    private static Scene targetScene;

    public static void Load(Scene targetScene)
    {

        Loader.targetScene = targetScene;

        SceneManager.LoadScene(Scene.LoadingScene.ToString());
        SceneManager.LoadScene(targetScene.ToString());
    }

    public static void LoaderCallback()
    {
        SceneManager.LoadScene(targetScene.ToString());
    }


}
