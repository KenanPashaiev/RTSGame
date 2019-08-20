using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private LayerMask clickableLayer;

    public LevelUpInterface levelUpInterface;
    private LevelUpInterface levelUpInterfaceClone;
}
