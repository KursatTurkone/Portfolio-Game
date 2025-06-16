using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(RagdollOperations))]
public class RagdollOperationsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        RagdollOperations ragdoll = (RagdollOperations)target;

        GUILayout.Space(10);
        EditorGUILayout.LabelField("=== DEBUG TOOLS ===", EditorStyles.boldLabel);

        if (GUILayout.Button("Find All Joints"))
        {
            ragdoll.FindAllJoints();
        }

        if (GUILayout.Button("Find All Colliders"))
        {
            ragdoll.FindAllColliders();
        }

        if (GUILayout.Button("Find All Rigs"))
        {
            ragdoll.FindAllRigs();
        }

        if (GUILayout.Button("Enable All Colliders"))
        {
            ragdoll.ChangeColliderState(true);
        }

        if (GUILayout.Button("Disable All Colliders"))
        {
            ragdoll.ChangeColliderState(false);
        }
        

        if (GUILayout.Button("Activate Ragdoll"))
        {
            ragdoll.DoRagdoll(true);
        }

        if (GUILayout.Button("Deactivate Ragdoll"))
        {
            ragdoll.DoRagdoll(false);
        }

        GUILayout.Space(10);
        EditorGUILayout.LabelField("Apply Force", EditorStyles.boldLabel);

        float forceValue = EditorGUILayout.FloatField("Force", 5f);
        if (GUILayout.Button("Apply Forward Force"))
        {
            ragdoll.MoveForce(forceValue);
        }
    }
}