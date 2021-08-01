using System.Collections;
using System.Collections.Generic;
using NSubstitute;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public static class Helpers
{
    /// <summary>
    /// <para>Creates a test environment containing a floor and light object</para>
    /// </summary>
    public static IEnumerator LoadMovementTestScene()
    {
        var operation = SceneManager.LoadSceneAsync(Utilities.Scenes.MovementTests.ToString());
        while (operation.isDone == false)
        {
            yield return null;
        }
    }

    /// <summary>
    /// Creates a Player GameObject for testing 
    /// </summary>
    /// <returns>Object of type Player</returns>
    public static Player GetPlayer()
    {
        Player player = GameObject.FindObjectOfType<Player>();
        GameObject child = GameObject.CreatePrimitive(PrimitiveType.Cube);
        child.AddComponent<Animator>();
        child.transform.SetParent(player.transform);
        player.PlayerInput = Substitute.For<IPlayerInput>();
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

    public static IEnumerator LoadEntityStateMachineTestScene()
    {
        var operation = SceneManager.LoadSceneAsync(Utilities.Scenes.EntityStateMachineTests.ToString());
        while(operation.isDone == false)
            yield return null;
    }
}