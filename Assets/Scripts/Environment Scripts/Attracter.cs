using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attracter : MonoBehaviour
{
   public Rigidbody rb;
   private const float G = 6.674f; //Gravitational Pull
   public static List<Attracter> Attractors;
   private void FixedUpdate()
   {
      foreach (Attracter attractor in Attractors)
      {
         if (attractor != this)
         {
            Attract(attractor);
         }
      }
   }

   void OnEnable()
   {
      if (Attractors == null)
      {
         Attractors = new List<Attracter>();
         Attractors.Add(this);
      }
   }

   void OnDisable()
   {
      Attractors.Remove(this);
   }

   void Attract(Attracter objToAttract)
   {
      Rigidbody rbToAttract = objToAttract.rb;
      Vector3 direction = rb.position - rbToAttract.position;
      float distance = direction.magnitude;
      if (distance == 0)
      {
         return;
      }
      float forceMagnitude = G * (rb.mass * rbToAttract.mass) / Mathf.Pow(distance, 2);
      Vector3 force = direction.normalized * forceMagnitude;
      
      rbToAttract.AddForce(force);
   }
}
