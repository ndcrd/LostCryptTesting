using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneUtils
{
    public static IEnumerator WaitForSceneLoad()
    {
        bool sceneLoaded = false;

        SceneManager.sceneLoaded += (scene, loadMode) => { sceneLoaded = true; };

        yield return new WaitUntil(() => sceneLoaded);
        yield return null;
    }
}