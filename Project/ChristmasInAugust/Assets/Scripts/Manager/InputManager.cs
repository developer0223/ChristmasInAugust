using UnityEngine;
using UnityEngine.EventSystems;

using Utility;

namespace Manager
{
    public class InputManager : Manager, IPointerEnterHandler, IPointerExitHandler
    {
        private Player.Player player;
        private bool pressed = false;

        private void Start()
        {
            // player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player.Player>();
            player = FindComponent<Player.Player>(Data.Tags.PLAYER);
        }

        private void Update()
        {
//#if UNITY_EDITOR
//#elif UNITY_ANDROID
            if (pressed)
            {
                Move(gameObject.name);
            }
            else
            {
                Move("");
            }
//#endif
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
