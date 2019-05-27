using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Door : MonoBehaviour
{
    [SerializeField] float openingAngle;
    [SerializeField] float animationTime;

    private void Start()
    {
        GameManager.Instance.RoundEndAction += OpenDoor;
        
    }
    private void OnEnable()
    {
        transform.DOKill();
    }

    private void OpenDoor()
    {
        if(this)
            transform.DORotate(new Vector3(transform.rotation.eulerAngles.x, openingAngle, transform.rotation.y), animationTime);
    }
}
