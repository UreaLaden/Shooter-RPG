using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HumanBone
{
    public HumanBodyBones bone;
}
public class WeaponIK : MonoBehaviour
{
    public Transform targetTransform;

    public Transform aimTransform;

    public int iterations = 10;
    [Range(0,1)] public float weight= 1.0f;

    public Transform bone;

    private Transform[] boneTransforms;
    private float maxDistance;

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 targetPosition = Vector3.forward.normalized;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            targetPosition = hitInfo.transform.position;
        }
        if (Input.GetMouseButton(1))
        {
            AimAtTarget(bone, targetPosition);
        }
    }

    private void AimAtTarget(Transform bone, Vector3 targetPosition)
    {
        Vector3 aimDirection = aimTransform.forward;
        Vector3 targetDirection = targetPosition - aimTransform.position;
        Quaternion aimTowards = Quaternion.FromToRotation(aimDirection, targetDirection);
        Quaternion blendedRotation = Quaternion.Slerp(Quaternion.identity, aimTowards,weight);
        bone.rotation = blendedRotation * bone.rotation;
    }
}
