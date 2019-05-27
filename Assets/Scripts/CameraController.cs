using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private float moveSpeed;
    private Transform _target;

    void Update()
    {

        if (_target != null)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(_target.position.x + offset.x, offset.y, offset.z), moveSpeed);
        }
        else
        {
            if (Player.Instance)
                _target = Player.Instance.transform;
        }
    }
}
