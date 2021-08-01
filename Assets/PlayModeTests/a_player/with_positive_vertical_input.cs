using System.Collections;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace a_player
{
    /// <summary>
    /// <para>Tests that a player with positive vertical input moves forward</para>
    /// </summary>
    public class with_positive_vertical_input
    {
        [UnityTest]
        public IEnumerator moves_forward()
        {
            yield return Helpers.LoadMovementTestScene();
            var player = Helpers.GetPlayer();
            player.PlayerInput.Vertical.Returns(1f);

            float startingZPos = player.transform.position.z;
            yield return new WaitForSeconds(5f);
            float endingZPos = player.transform.position.z;

            Assert.Greater(endingZPos, startingZPos);
        }
    }
    
    public class with_negative_vertical_input
    {
        [UnityTest]
        public IEnumerator moves_backward()
        {
            yield return Helpers.LoadMovementTestScene();
            var player = Helpers.GetPlayer();
            player.PlayerInput.Vertical.Returns(-1f);

            float startingZPos = player.transform.position.z;
            yield return new WaitForSeconds(.5f);
            float endingZPos = player.transform.position.z;

            Assert.Less(endingZPos, startingZPos);
        }
    }

    public class with_negative_mouse_x
    {
        [UnityTest]
        public IEnumerator turns_left()
        {
            yield return Helpers.LoadMovementTestScene();
            var player = Helpers.GetPlayer();

            player.PlayerInput.MouseX.Returns(-1f);

            var originalRotation = player.transform.rotation;
            yield return new WaitForSeconds(0.5f);

            float turnAmount = Helpers.CalculateTurn(originalRotation, player.transform.rotation);
            Assert.Less(turnAmount, 0);
        }
    }
    
    public class with_positive_mouse_x
    {
        [UnityTest]
        public IEnumerator turns_right()
        {
            yield return Helpers.LoadMovementTestScene();
            var player = Helpers.GetPlayer();

            player.PlayerInput.MouseX.Returns(1f);

            var originalRotation = player.transform.rotation;
            yield return new WaitForSeconds(0.5f);

            float turnAmount = Helpers.CalculateTurn(originalRotation, player.transform.rotation);
            Assert.Greater(turnAmount, 0);
        }
    }
    
    
}