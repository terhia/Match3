using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts
{
        public class InputHandler : MonoBehaviour, IPointerDownHandler
        {
                public GameObjectUnityEvent OnPointerDownUnityEvent;

                private void OnEnable()
                {
                        if (OnPointerDownUnityEvent == null)
                        {
                                OnPointerDownUnityEvent =  new GameObjectUnityEvent();
                        }
                }

                private void OnDisable()
                {
                        OnPointerDownUnityEvent = null;
                }

                public void OnPointerDown(PointerEventData eventData)
                {
                        // ReSharper disable once UseNullPropagation
                        if (OnPointerDownUnityEvent != null)
                        {
                                OnPointerDownUnityEvent.Invoke(gameObject);
                        }
                }
        }
}