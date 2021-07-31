using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetGrounded : MonoBehaviour
{
   [SerializeField] private float maxDistance;
   [SerializeField] private bool isGrounded;
   public float currentPosY;
   [SerializeField]private float gravity = 9.6f;
   [SerializeField] private Transform leftBase;
   [SerializeField] private Transform rightBase;
   private void Update()
   {
      Ray leftRay = new Ray(leftBase.position, Vector3.down);
      Ray rightRay = new Ray(rightBase.position, Vector3.down);
      isGrounded = Physics.Raycast(leftRay, out RaycastHit leftHit, maxDistance) && Physics.Raycast(rightRay, out RaycastHit rightHit, maxDistance);
      currentPosY = isGrounded ? transform.position.y : transform.position.y - Time.deltaTime;
      transform.position = new Vector3(transform.position.x, currentPosY, transform.position.z);
      Debug.Log("Is Grounded: " + isGrounded);
   }
}

