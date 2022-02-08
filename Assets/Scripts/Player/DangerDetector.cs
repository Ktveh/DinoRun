using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerDetector : MonoBehaviour
{
    [SerializeField] private Transform _raycastPoint;
    [SerializeField] private float _raycastDistance;

    private void Update()
    {
        RaycastHit[] hits = Physics.RaycastAll(_raycastPoint.position, Vector3.forward, _raycastDistance);
        foreach (var hit in hits)
        {
            Block block;
            if (hit.collider.TryGetComponent<Block>(out block))
                block.AlertOfDanger();
        }
    }
}
