using System.Collections;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;


/// <summary>
/// <para>Tests that a player with positive vertical input moves forward</para>
/// </summary>
public class with_positive_vertical_input
{
    [UnityTest]
    public IEnumerator moves_forward()
    {
        Helpers.CreateEnvironment();
        var player = Helpers.CreatePlayer();
        player.PlayerInput.Vertical.Returns(1f);

        float startingZPos = player.transform.position.z;
        Debug.Log($"Starting Pos Z {startingZPos}");
        yield return new WaitForSeconds(5f);
        float endingZPos = player.transform.position.z;
        Debug.Log($"Ending Pos Z {endingZPos}");

        Assert.Greater(endingZPos, startingZPos);
    }
}

public class with_negative_mouse_x
{
    [UnityTest]
    public IEnumerator turns_left()
    {
        Helpers.CreateEnvironment();
        var player = Helpers.CreatePlayer();

        player.PlayerInput.MouseX.Returns(-1f);

        var originalRotation = player.transform.rotation;
        yield return new WaitForSeconds(0.5f);

        float turnAmount = Helpers.CalculateTurn(originalRotation, player.transform.rotation);
        Assert.Less(turnAmount, 0);
    }
}