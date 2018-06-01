using Player;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Manager
{
    public class InputManager : Manager, IPointerEnterHandler, IPointerExitHandler
    {
        private Player.Player player;
        private bool pressed = false;

        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player.Player>();
        }

        private void Update()
        {
#if UNITY_EDITOR
#elif UNITY_ANDROID
            Debug.Log($"UNITY_ANDROID : InputManager");
            if (pressed)
            {
                Move(gameObject.name);
            }
            else
            {
                Move("");
            }
#endif
        }

        public void Move(string objName)
        {
            Player.Player.Direction direction = Player.Player.Direction.None;
            switch (objName)
            {
                case "LeftButton":
                    direction = Player.Player.Direction.Left;
                    break;
                case "RightButton":
                    direction = Player.Player.Direction.Right;
                    break;
                default:
                    direction = Player.Player.Direction.None;
                    break;
            }
            player.Move(direction);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            pressed = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            pressed = false;
        }
    }
}
