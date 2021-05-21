using DG.Tweening;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float timeToMove = 1.5f;


    private void Start()
    {
        MoveToTarget();
    }

    private void MoveToTarget()
    {
        transform.DOMove(target.position, timeToMove).SetEase(Ease.InOutSine);
    }
}
