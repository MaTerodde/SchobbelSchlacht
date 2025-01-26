using UnityEngine;

namespace test
{
    [CreateAssetMenu(fileName = "GrabblerSO", menuName = "test", order = 1)]
    public class GrabblerSO : ScriptableObject
    {
        [SerializeField, Range(0f, 1000f)] public float range;
        [SerializeField, Range(1f, 500f)] public float hookStrengthModifier;
        [SerializeField, Range(1f, 500f)] public float acceleration;
        [SerializeField, Range(0f, 100f)] public float sensitivity;
        [SerializeField, Range(45f, 90f)] public float maxYAngle;
        [SerializeField, Range(1f, 1000f)] public float walkingSpeed;

    }
}