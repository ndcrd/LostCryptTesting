using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.U2D.Animation;

public class WandTests
{
    private Transform _characterTransform;
    private float _testTimeout = 25.0f;
    private Vector3 _wandPosition;

    [UnityTest]
    public IEnumerator MainScene_CharacterReachesWandAndEquipsArmor()
    {
        SceneManager.LoadScene("Assets/LostCrypt/Scenes/Main.unity", LoadSceneMode.Single);
        
        // Skip first frame so Sara have a chance to appear on the screen
        yield return null;
        var puppet = GameObject.Find("puppet_sara");
        var spriteLibrary = puppet.GetComponent<SpriteLibrary>();
        
        Assert.AreEqual(spriteLibrary.spriteLibraryAsset.name, "Sara");

        var sara = GameObject.Find("Sara Variant");
        _wandPosition = GameObject.Find("WandPosition").transform.position;
        sara.gameObject.transform.position = _wandPosition;

        // var co = sara.GetComponent<CharacterController2D>();
        
        
        
        // Wait for Wand pickup animation to be over.
        yield return new WaitForSeconds(12);

        Assert.AreEqual(spriteLibrary.spriteLibraryAsset.name, "Sara_var01");
    }
}