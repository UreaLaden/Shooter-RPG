using System.Collections;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.TestTools;

namespace state_machine
{
    public class entity_state_machine
    {
        [UnityTest]
        public IEnumerator starts_in_idle_state()
        {
            yield return Helpers.LoadEntityStateMachineTestScene();
            var stateMachine = GameObject.FindObjectOfType<EntityStateMachine>();
            Debug.Log(stateMachine.CurrentStateType);
            Assert.AreEqual(typeof(Idle),stateMachine.CurrentStateType);
        }

        [UnityTest]
        public IEnumerator transitions_to_chase_player_when_in_range()
        {
            yield return Helpers.LoadEntityStateMachineTestScene();
            var player = Helpers.GetPlayer();
            var entityStateMachine = GameObject.FindObjectOfType<EntityStateMachine>();
            
            player.transform.position = entityStateMachine.transform.position + new Vector3(5.9f, 0f, 0f);
            Debug.Log(Vector3.Distance(entityStateMachine.transform.position,player.transform.position) + $"$State: {entityStateMachine.CurrentStateType}");
            yield return null;
            Assert.AreEqual(typeof(Idle),entityStateMachine.CurrentStateType);
            player.transform.position = entityStateMachine.transform.position + new Vector3(4.9f, 0f, 0f);
            Debug.Log(Vector3.Distance(entityStateMachine.transform.position,player.transform.position) + $"$State: {entityStateMachine.CurrentStateType}");
            yield return null;
            Assert.AreEqual(typeof(Pursue),entityStateMachine.CurrentStateType);
        }
        
        [UnityTest]
        public IEnumerator switches_to_dead_only_one_health_reaches_zero()
        {
            yield return Helpers.LoadEntityStateMachineTestScene();
            var stateMachine = GameObject.FindObjectOfType<EntityStateMachine>();
            var entity = stateMachine.GetComponent<Entity>();
            
            yield return null;
            Assert.AreEqual(typeof(Idle),stateMachine.CurrentStateType);
            
            entity.TakeHit(entity.Health-1);
            yield return null;
            Assert.AreEqual(typeof(Idle),stateMachine.CurrentStateType);
            
            entity.TakeHit(entity.Health);
            yield return null;
            Assert.AreEqual(typeof(Dead),stateMachine.CurrentStateType);
        }
    }
}