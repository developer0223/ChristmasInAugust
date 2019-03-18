using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    public class CoroutineRefManager : Manager
    {

        private IEnumerator[] turrets;
        private IEnumerator[] enemies;

        public void SetTurretCoroutine(params IEnumerator[] turrets)
        {
            this.turrets = turrets;
        }

        public void SetEnemyCoroutine(params IEnumerator[] enemies)
        {
            this.enemies = enemies;
        }
    }
}