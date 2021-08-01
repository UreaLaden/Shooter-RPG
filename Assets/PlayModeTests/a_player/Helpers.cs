using System.Collections.Generic;
using NSubstitute;
using UnityEngine;
using UnityEngine.AI;

public static class Helpers
{
    /// <summary>
    /// <para>Creates a test environment containing a floor and light object</para>
    /// </summary>
    public static void CreateEnvironment()
    {
        var floor = GameObject.CreatePrimitive(PrimitiveType.Cube);
        floor.transform.localScale = new Vector3(50, 0, 50);
        floor.transform.position = Vector3.zero - Vector3.one;
        floor.GetComponent<Renderer>().material.SetColor("_Color", Color.green);


        //Add lighting to the scene
        GameObject light = new GameObject("Light");
        var lightComp = light.AddComponent<Light>();
        lightComp.type = LightType.Directional;
        light.transform.localRotation = Quaternion.Euler(50, -30, 0);
        light.transform.position = new Vector3(0, 5, 0);
    }

    /// <summary>
    /// Creates a Player GameObject for testing 
    /// </summary>
    /// <returns>Object of type Player</returns>
    public static Player CreatePlayer()
    {
        var playerGameObject = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        playerGameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        playerGameObject.AddComponent<CharacterController>();
        playerGameObject.AddComponent<NavMeshAgent>();

        Player player = playerGameObject.AddComponent<Player>();

        playerGameObject.transform.position = new Vector3(0, 1.3f, 0);

        var testPlayerInput = Substitute.For<IPlayerInput>();
        player.PlayerInput = testPlayerInput;
        return player;
    }

    /// <summary>
    ///     <para>Returns a negative value if turning left and positive if turning right.</para>
    /// </summary>
    /// <param name="originalRotation">The players current rotation before calculation</param>
    /// <param name="transformRotation">The players current rotation after calculation</param>
    public static float CalculateTurn(Quaternion originalRotation, Quaternion transformRotation)
    {
        var cross = Vector3.Cross(originalRotation * Vector3.forward, transformRotation * Vector3.forward);
        var dot = Vector3.Dot(cross, Vector3.up);
        return dot;
    }
}