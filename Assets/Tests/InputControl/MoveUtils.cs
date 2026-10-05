using System.Collections;
using UnityEngine;

public static class MoveUtils
{
    public static IEnumerator Jump()
    {
        TestInputControl.Jump = true;
        yield return null;
        TestInputControl.Jump = false;
    }

    public static IEnumerator GoRight()
    {
        TestInputControl.MoveLeft = false;
        yield return null;
        TestInputControl.MoveRight = true;
    }

    public static IEnumerator GoLeft()
    {
        TestInputControl.MoveRight = false;
        yield return null;
        TestInputControl.MoveLeft = true;
    }
    
    public static void StopMoving()
    {
        TestInputControl.MoveRight = false;
        TestInputControl.MoveLeft = false;
    }
    
    public static Vector3 GetCurrentCharacterPosition(GameObject character, string gameObjectName)
    {
        if (character == null)
        {
            character = GameObject.Find(gameObjectName);
        }

        return character.transform.position;
    }
}