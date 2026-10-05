using System;
using System.Collections;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.U2D.Animation;

public class WandTests
{
    private Transform characterTransform;
    private Vector3 wandPosition;

    private readonly string kScene = "Assets/LostCrypt/Scenes/Main.unity";
    
    private readonly string puppetSara = "puppet_sara";
    private readonly string saraVariant = "Sara Variant";
    private readonly string wand = "WandPosition";
    private readonly string spriteNameBefore = "Sara";
    private readonly string expectedSpriteName = "Sara_var01";
    
    private readonly string spritePath = "Assets/LostCrypt/Character/Sprites/Sara_var01.psb";

    [UnityTest]
    public IEnumerator MainScene_CharacterCanTriggerWand()
    {
        SceneManager.LoadScene(kScene, LoadSceneMode.Single);
        
        yield return SceneUtils.WaitForSceneLoad();
        
        var puppet = GameObject.Find(puppetSara);
        var spriteLibrary = puppet.GetComponent<SpriteLibrary>();

        Assert.AreEqual(spriteLibrary.spriteLibraryAsset.name, spriteNameBefore);

        var sara = GameObject.Find(saraVariant);
        wandPosition = GameObject.Find(wand).transform.position;
        sara.gameObject.transform.position = wandPosition;
        
        yield return new WaitForSeconds(12);

        Assert.AreEqual(spriteLibrary.spriteLibraryAsset.name, expectedSpriteName);
    }

    [UnityTest]
    public IEnumerator MainScene_CharacterSwapsOutfits()
    {
        SceneManager.LoadScene(kScene, LoadSceneMode.Single);
        var assetToSwap = AssetDatabase.LoadAssetAtPath<SpriteLibraryAsset>(
            spritePath);
        
        yield return SceneUtils.WaitForSceneLoad();
        
        var spriteLibraryBefore = GameObject.Find(puppetSara).GetComponent<SpriteLibrary>().spriteLibraryAsset.name;
        Debug.Log(spriteLibraryBefore);
        var playerControllerUnderTest = GameObject.Find(saraVariant).GetComponent<CharacterController2D>();

        playerControllerUnderTest.SwapSprites(assetToSwap);

        Assert.AreNotEqual(expectedSpriteName, spriteLibraryBefore);
    }
}