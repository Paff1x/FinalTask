using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{

    [SerializeField] private float rotateSpeed;
    [SerializeField] private float moveSpeed;
    [SerializeField] Transform[] extremePoints;
    private Transform nextExtremePoint;
    private int i = 0;

    private void Start()
    {
        if (moveSpeed != 0)
        {
            transform.position = extremePoints[0].position;
            nextExtremePoint = extremePoints[0];
        }
    }
    private void Update()
    {
        if(moveSpeed!=0)
        {
            if(Vector3.Distance(transform.position, nextExtremePoint.position) < 0.5f)
            {
                i++;
                nextExtremePoint = extremePoints[i % extremePoints.Length];
            }
            transform.position = Vector3.MoveTowards(transform.position, nextExtremePoint.position, moveSpeed*Time.deltaTime);
        }
        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
    }
}
