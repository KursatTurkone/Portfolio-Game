using System.Collections.Generic;
using UnityEngine;

public class RagdollOperations : MonoBehaviour
{
    public List<Transform> BodyParts;
    public List<Collider> BodyPartColliders;
    public List<Rigidbody> BodyPartRigs; 
    public bool HaveMainCollider;

    public GameObject MainObject;

    public void FindAllJoints()
    {
        BodyParts.Clear();
        var joints = transform.GetComponentsInChildren<Collider>();
        foreach (var joint in joints)
        {
            BodyParts.Add(joint.transform);
        }
    }

    public void FindAllColliders()
    {
        BodyPartColliders.Clear();
        var joints = transform.GetComponentsInChildren<Collider>();
        foreach (var joint in joints)
        {
            BodyPartColliders.Add(joint);
        }
    }

    public void FindAllRigs()
    {
        BodyPartRigs.Clear();
        var rigs = transform.GetComponentsInChildren<Rigidbody>();
        foreach (var rig in rigs)
        {
            BodyPartRigs.Add(rig);
        }
    }

    public void ChangeColliderState(bool enable)
    {
        foreach (var collider in BodyPartColliders)
        {
            collider.enabled = enable; 
        }
    }
    
    public void DoRagdoll(bool isRagdoll)
    {
        foreach (Transform part in BodyParts)
        {
            if (part.TryGetComponent(out Collider col))
                col.enabled = isRagdoll;

            if (part.TryGetComponent(out Rigidbody rig))
                rig.useGravity = isRagdoll;
        }

        if (HaveMainCollider && MainObject != null)
        {
            if (MainObject.TryGetComponent(out Collider mainCol))
                mainCol.enabled = !isRagdoll;

            if (MainObject.TryGetComponent(out Rigidbody mainRig))
                mainRig.useGravity = !isRagdoll;
        }

        var animator = GetComponent<Animator>();
        if (animator != null)
            animator.enabled = !isRagdoll;
    }

    public void MoveForce(float force)
    {
        foreach (var rig in BodyPartRigs)
        {
            if (rig != null)
                rig.velocity = Vector3.forward * force;
        }
    }
}
