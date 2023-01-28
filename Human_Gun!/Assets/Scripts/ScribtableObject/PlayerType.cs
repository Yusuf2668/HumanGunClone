using UnityEngine;

[CreateAssetMenu(fileName = "PlayerType", menuName = "PlayerType")]
public class PlayerType : ScriptableObject
{
    public float playerRunSpeed;
    public float playerMoveSpeed;
    public float playerJumpPower;
}