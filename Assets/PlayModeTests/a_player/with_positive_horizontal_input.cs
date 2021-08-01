using System.Collections;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace a_player
{
    public class with_positive_horizontal_input
    {
        [UnityTest]
        public IEnumerator moves_right()
        {
            Helpers.CreateEnvironment();
            var player = Helpers.CreatePlayer();
            player.PlayerInput.Horizontal.Returns(1f);

            float startingXPos = player.transform.position.x;
            yield return new WaitForSeconds(5f);
            float endingXPos = player.transform.position.x;

            Assert.Greater(endingXPos, startingXPos);
        }
    }
}