using System.Collections;
using System.Linq;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace ValidationTests
{
    [TestFixture]
    [TestFixtureSource(typeof(GameplayScenesProvider))]
    public class SceneValidationTests
    {
        private readonly string _scenePath;

        public SceneValidationTests(string scenePath)
        {
            _scenePath = scenePath;
        }

        [OneTimeSetUp]
        public void LoadScene()
        {
            SceneManager.LoadScene(_scenePath);
        }

        [UnityTest]
        public IEnumerator SaraAndWandArePresent()
        {
            yield return WaitForSceneLoad();
            var wand = GameObject.Find("Wand");
            var sara = GameObject.Find("Sara Variant");

            Assert.NotNull(wand, "Wand object exists");
            Assert.NotNull(sara, "Sara object exists");
        }

        private static IEnumerator WaitForSceneLoad()
        {
            while (!SceneManager.GetActiveScene().isLoaded)
            {
                yield return null;
            }
        }
    }

    public class GameplayScenesProvider : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            return (from scene in EditorBuildSettings.scenes
                where scene.enabled && scene.path != null
                select scene.path).GetEnumerator();
        }
    }
}