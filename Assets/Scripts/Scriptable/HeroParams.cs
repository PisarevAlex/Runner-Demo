using UnityEngine;

[CreateAssetMenu(fileName = "Hero Params", menuName = "SO/Hero Params")]
public class HeroParams : ScriptableObject
{
    [Header("Physics parameters")]
    [Min(0)] public float acceleration;
    [Min(0)] public float turnForce;
    [Range(0,1)] public float deceleration;
    [Min(0)] public float maxRunSpeed;
    [Min(0)] public float maxTurnSpeed;

    [Header("Animation parameters")]
    [Min(0)] public float maxTiltAngle;
    [Min(0)] public float maxRotation;
}
