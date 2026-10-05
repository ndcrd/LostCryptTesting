using System.Collections;
using NUnit.Framework;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class SceneSetupTests
{
    [UnityTest]
    public IEnumerator MainSceneSceneEffectSetToDay()
    {
        SceneManager.LoadScene("Assets/LostCrypt/Scenes/Main.unity", LoadSceneMode.Single);
        yield return null;
        var gameObjectUnderTest = GameObject.Find("FX - Day");
        Assert.That(gameObjectUnderTest.activeSelf, Is.True, "Day Time Fx is disabled in tree.");
    }
}