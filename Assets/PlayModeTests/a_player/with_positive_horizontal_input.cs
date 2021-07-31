using System.Collections;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class with_positive_horizontal_input
{
    [UnityTest]
    public IEnumerator moves_right()
    {
        //Add lighting to the scene
        GameObject light = new GameObject("Light");
        var lightComp = light.AddComponent<Light>();
        lightComp.type = LightType.Directional;
        light.transform.localRotation = Quaternion.Euler(50,-30,0);
        light.transform.position = new Vector3(0, 5, 0);
        
        var floor = GameObject.CreatePrimitive(PrimitiveType.Cube);
        floor.transform.localScale = new Vector3(50, 0.1f, 50);
        floor.transform.position = Vector3.zero - Vector3.one;
        floor.GetComponent<Renderer>().material.SetColor("_Color",Color.red);
        
        var playerGameObject = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        playerGameObject.GetComponent<Renderer>().material.SetColor("_Color",Color.blue);
        playerGameObject.AddComponent<CharacterController>();
        
        _Player player = playerGameObject.AddComponent<_Player>();
        playerGameObject.transform.position = new Vector3(0, 1.3f, 0);

        var testPlayerInput = Substitute.For<IPlayerInput>();
        testPlayerInput.Horizontal.Returns(1f);

        float startingXPos = player.transform.position.x;
        yield return new WaitForSeconds(5f);
        float endingXPos = player.transform.position.x;
        
        Assert.Greater(endingXPos,startingXPos);
    }
}