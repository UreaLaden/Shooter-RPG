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
        while (operation.isDone == false)
            yield return null;
    }

    /// <summary>
    /// <para> Creates a sphere at the players feet to be used as a ground check </para>
    /// </summary>
    /// <param name="_collider">Capsule Collider attached to the GameObject</param>
    /// <param name="groundLayer">Layer that should be perceived as the ground</param>
    /// <returns>Boolean verifying whether player is on the ground or not</returns>
    public static bool IsGrounded(CapsuleCollider _collider, LayerMask groundLayer)
    {
        float radius = _collider.radius * .9f;
        Vector3 pos = _collider.gameObject.transform.position + Vector3.up * (radius * .9f);
        return Physics.CheckSphere(pos, radius, groundLayer);
    }

    /// <summary>
    /// <para>This allows a navMeshAgent to slowly manipulate tight turns</para>
    /// </summary>
    /// <param name="_navMeshAgent">The NavMeshAgent executing the movement</param>
    /// <param name="gameObject">The agent the navmesh is connected to</param>
    public static void NegotiateTurn(NavMeshAgent _navMeshAgent, GameObject gameObject)
    {
        if (_navMeshAgent.hasPath)
        {
            Vector3 toTarget = _navMeshAgent.steeringTarget - gameObject.transform.position;
            float turnAngle = Vector3.Angle(gameObject.transform.forward, toTarget);
            _navMeshAgent.acceleration = turnAngle * _navMeshAgent.speed;
        }
    }
    /// <summary>
    /// a NavMeshAgent extension method to get the remaining distance at any moment, or any point of the path
    /// </summary>
    /// <param name="navMeshAgent">The Agent</param>
    /// <returns>The agents remaining distance</returns>
    public static float GetPathRemainingDistance(NavMeshAgent navMeshAgent)
    {
        if (navMeshAgent.pathPending ||
            navMeshAgent.pathStatus == NavMeshPathStatus.PathInvalid ||
            navMeshAgent.path.corners.Length == 0)
        {
            return -1f;
        }

        float distance = 0.0f;
        for (int i = 0; i < navMeshAgent.path.corners.Length-1; i++)
        {
            distance += Vector3.Distance(navMeshAgent.path.corners[i], navMeshAgent.path.corners[i + 1]);
        }

        return distance;
    }
}