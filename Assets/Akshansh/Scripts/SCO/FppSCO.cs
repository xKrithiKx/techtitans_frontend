using UnityEngine;

[CreateAssetMenu(fileName = "FppAsset", menuName = "Akshansh/Controllers")]
public class FppSCO : ScriptableObject
{
    [SerializeField] string CharacterName;
    public float MoveSpeed = 2f, MinClamp = -20, MaxClamp = 20,
        MouseSenstivity = 1f;
}
