using UnityEngine;

/// <summary>
/// The class that constantly controls the walkable area
/// </summary>

public class WalkableAreaCheck : MonoBehaviour
{
    [SerializeField] private BoxCollider2D checkZoneCollider;

    void Start()
    {
        InvokeRepeating("OnUpdateGraph", 0, 0.5f);
    }
    private void OnUpdateGraph()
    {
        AstarPath.active.UpdateGraphs(checkZoneCollider.bounds);
    }

}
