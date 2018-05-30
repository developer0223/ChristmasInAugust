using Bullet;
using UnityEngine;
using System.Collections;

namespace Enemy
{
    public class ThickTurret : Turret
    {
        private void Start()
        {
            Shoot(0, 360, 3.0f, Bullet.SilverStar);
        }

        public void Shoot(int startDegree, int endDegree, float playTime, Bullet bullet)
        {
            SetBullet(bullet);
            StartCoroutine(EShoot(startDegree, endDegree, playTime, bullet));
        }

        protected IEnumerator EShoot(int startDegree, int endDegree, float playTime, Bullet bullet)
        {
            for (int degree = startDegree; degree <= endDegree; degree += 3)
            {
                /*
                float x1 = GetX(degree - 5);
                float y1 = GetY(degree - 5);
                
                float x2 = GetX(degree);
                float y2 = GetY(degree);
                
                float x3 = GetX(degree + 5);
                float y3 = GetY(degree + 5);
                */

                Vector3 pos1 = calculateManager.GetPosition(degree - 5, radius);
                Vector3 pos2 = calculateManager.GetPosition(degree, radius);
                Vector3 pos3 = calculateManager.GetPosition(degree + 5, radius);

                /*
                GameObject a = Instantiate(currentBullet, new Vector3(x1, y1, 0), Quaternion.identity);
                GameObject b = Instantiate(currentBullet, new Vector3(x2, y2, 0), Quaternion.identity);
                GameObject c = Instantiate(currentBullet, new Vector3(x3, y3, 0), Quaternion.identity);
                */

                GameObject a = Instantiate(currentBullet, pos1, Quaternion.identity);
                GameObject b = Instantiate(currentBullet, pos2, Quaternion.identity);
                GameObject c = Instantiate(currentBullet, pos3, Quaternion.identity);

                // yield return new WaitForSeconds((endDegree - startDegree) / 10 / playTime);
                yield return new WaitForSecondsRealtime(0.1f);
            }
        }
    }
}