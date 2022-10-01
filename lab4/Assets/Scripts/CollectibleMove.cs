using UnityEngine;
using DG.Tweening;

public class CollectibleMove : MonoBehaviour

{
    [SerializeField] private Transform tweenEndPoint;
    void Start()
    {
        transform.DOMove(tweenEndPoint.position, 6f).SetEase(Ease.InOutQuad).SetLoops(-1, LoopType.Yoyo);
    }
}