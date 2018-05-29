using Player;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Manager
{
    public class InputManager : Manager, IPointerEnterHandler, IPointerExitHandler
    {
        private SnowMan player;
        private bool pressed = false;

        private void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<SnowMan>();
        }

        private void Update()
        {
            if (pressed)
            {
                Move(gameObject.name);
            }
            else
            {
                Move("");
            }
        }

        public void Move(string objName)
        {
            SnowMan.Direction direction = SnowMan.Direction.None;
            switch (objName)
            {
                case "LeftButton":
                    direction = SnowMan.Direction.Left;
                    break;
                case "RightButton":
                    direction = SnowMan.Direction.Right;
                    break;
                default:
                    direction = SnowMan.Direction.None;
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
