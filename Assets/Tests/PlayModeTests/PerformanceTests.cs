using System.Collections;
using NUnit.Framework;
using Unity.PerformanceTesting;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class PerformanceTests
{
    private Transform _characterTransform;
    private float _wandLocation = 21.080f;
        
    [UnityTest, Performance]
    public IEnumerator MainScene_MeasureAverageFrames()
    {
        SceneManager.LoadScene("Assets/LostCrypt/Scenes/Main.unity", LoadSceneMode.Single);
        using (Measure.Frames().Scope("Frames.MainSceneOnLoad.Unstable"))
        {
            for (var i = 0; i < 25; i++)
            {
                yield return null;
            }
        }

        using (Measure.Frames().Scope("Frames.MainSceneGameplay"))
        {
            yield return MoveUtils.GoRight();
            while (GetCurrentCharacterPosition() <= _wandLocation)
            {
                yield return null;
            }

            MoveUtils.StopMoving();
            yield return new WaitForSeconds(15);
        }
    }

    private float GetCurrentCharacterPosition()
    {
        if (_characterTransform == null)
        {
            _characterTransform = GameObject.Find("Sara Variant").transform;
        }

        return _characterTransform.position.x;
    }
}