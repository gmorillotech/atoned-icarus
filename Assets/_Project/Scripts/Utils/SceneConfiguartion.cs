using UnityEngine;

public enum LevelType
{
    Standard3D,
    SideScroller
}

public class SceneConfiguration : MonoBehaviour
{
    [Header("Level Settings")]
    [SerializeField] private LevelType levelType = LevelType.Standard3D;

    public LevelType LevelType => levelType;
}