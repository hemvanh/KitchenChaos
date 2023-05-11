using UnityEngine.SceneManagement;

// Creating a static class to hold value across SCENES
// Due to objects in the OLD SCENE will be destroyed 
public static class Loader {
    public enum Scene {
        MainMenuScene,
        GameScene,
        LoadingScene
    }
    private static Scene targetScene;

    public static void Load(Scene targetScene) {
        Loader.targetScene = targetScene;
        SceneManager.LoadScene(Scene.LoadingScene.ToString());
    }

    // Will be triggered on the FIRST UPDATE of the Loading Scene
    // Means Loading Scene has been rendered
    // then we render the Target Scene
    public static void LoaderCallback() {
        SceneManager.LoadScene(targetScene.ToString());
    }
}
