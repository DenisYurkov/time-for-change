using DG.Tweening;
using UnityEngine;

namespace TimeForChange.UI
{
    public class Icon : MonoBehaviour
    {
        [SerializeField] private Vector3 _rotateValue;

        private void Start() =>
            transform.DORotate(_rotateValue, 0.3f, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Yoyo);
    }
}
