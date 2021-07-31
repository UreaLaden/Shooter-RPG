using System.Collections;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;


public class with_positive_vertical_input
{
    [UnityTest]
    //Test: A player with positive vertical input moves forward
    public IEnumerator moves_forward()
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
        floor.GetComponent<Renderer>().material.SetColor("_Color",Color.blue);
        
        var playerGameObject = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        playerGameObject.GetComponent<Renderer>().material.SetColor("_Color",Color.red);
        playerGameObject.AddComponent<CharacterController>();
        
        _Player player = playerGameObject.AddComponent<_Player>();
        playerGameObject.transform.position = new Vector3(0, 1.3f, 0);

        var testPlayerInput = Substitute.For<IPlayerInput>();
        testPlayerInput.Vertical.Returns(1f);
        
        float startingZPos = player.transform.position.z;
        yield return new WaitForSeconds(5f);
        float endingZPos = player.transform.position.z;
        
        Assert.Greater(endingZPos, startingZPos);
    }
}