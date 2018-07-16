using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Player;

namespace Manager
{
    public class InputManagerSecond : Manager
    {

        private Player.Player player;

        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player.Player>();
        }

        public void SetLeftPressedTrue()
        {
            player.isLeftPressed = true;
        }

        public void SetLeftPressedFalse()
        {
            player.isLeftPressed = false;
        }

        public void SetRightPressedTrue()
        {
            player.isRightPressed = true;
        }

        public void SetRightPressedFalse()
        {
            player.isRightPressed = false;
        }
    }
}