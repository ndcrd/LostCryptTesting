using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

[Explicit, Category("Movement")]
public class MovementTest
{
    private const string KScene = "Assets/LostCrypt/Scenes/Main.unity";
    private GameObject saraUnderTest;
    private const string SaraVariant = "Sara Variant";
    private const float WandLocation = 21.080f;
    private const float TestTimeout = 25.0f;


    [UnitySetUp]
    public IEnumerator SetUp()
    {
        SceneManager.LoadScene(KScene, LoadSceneMode.Single);
        yield return SceneUtils.WaitForSceneLoad();

        saraUnderTest = GameObject.Find(SaraVariant);
        Assert.True(saraUnderTest.activeSelf);
    }

    [UnityTest]
    public IEnumerator MainSceneCharacterMoves()
    {
        yield return MoveUtils.GoLeft();
        yield return new WaitForSeconds(0.5f);
        yield return MoveUtils.GoRight();
        yield return new WaitForSeconds(0.5f);
        yield return MoveUtils.Jump();
        yield return new WaitForSeconds(0.5f);
    }

    [UnityTest]
    public IEnumerator MainSceneCharacterReachesWand()
    {
        yield return MoveUtils.GoRight();
        while (MoveUtils.GetCurrentCharacterPosition(saraUnderTest, SaraVariant).x <= WandLocation)
        {
            yield return null;
            if (Time.timeSinceLevelLoad >= TestTimeout)
            {
                Assert.Fail($"Test took Longer than {TestTimeout} seconds");
            }
        }
    }

    [UnityTest]
    public IEnumerator MainSceneCharacterDoesNotFallIntoTextures()
    {
        var leftBoundUnderTest = GameObject.Find("Character Bounds - Left");
        yield return MoveUtils.GoLeft();

        while (Time.timeSinceLevelLoad <= TestTimeout)
        {
            yield return MoveUtils.Jump();
            yield return new WaitForSeconds(0.5f);
            if (MoveUtils.GetCurrentCharacterPosition(saraUnderTest, SaraVariant).x <=
                leftBoundUnderTest.transform.position.x &&
                MoveUtils.GetCurrentCharacterPosition(saraUnderTest, SaraVariant).y <=
                leftBoundUnderTest.transform.position.y)
            {
                Assert.Fail($"Character broke scene bounds");
            }
        }
    }
}