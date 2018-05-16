using UniRx;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
namespace Test
{
    public class UniRxTest : MonoBehaviour
    {
        [SerializeField]
        private Button button;

        void Start()
        {
            UniRx();
        }

        void UniRx()
        {
            button?.onClick
                .AsObservable()
                .Subscribe(_ =>
                {
                    Debug.Log($"UniRx button clicked.");
                });
        }
    }
}