using Bullet;
using UnityEngine;
using System.Collections;

namespace Enemy
{
    public class ThickTurret : Turret
    {
        private bool isTimeScaleChanged = false;

        private void Update()
        {
            if (Time.timeScale == 1)
            {
                isTimeScaleChanged = false;
            }
            else
            {
                isTimeScaleChanged = true;
            }
        }

        public void Shoot(int startDegree, int endDegree, Bullet bullet, float delayTime = 0.0f)
        {
            SetBullet(bullet);
            StartCoroutine(EShoot(startDegree, endDegree, bullet, delayTime));
        }

        protected IEnumerator EShoot(int startDegree, int endDegree, Bullet bullet, float delayTime = 0.0f)
        {
            yield return new WaitForSeconds(delayTime);

            GameObject currentBullet = SetBullet(bullet);

            if (startDegree > endDegree)
            {
                for (int degree = startDegree; degree >= endDegree; degree -= 5)
                {
                    Vector3 pos1 = calculateManager.GetPosition(degree - 5, radius);
                    Vector3 pos2 = calculateManager.GetPosition(degree, radius);
                    Vector3 pos3 = calculateManager.GetPosition(degree + 5, radius);

                    GameObject a = Instantiate(currentBullet, pos1, Quaternion.identity, bulletsParent);
                    GameObject b = Instantiate(currentBullet, pos2, Quaternion.identity, bulletsParent);
                    GameObject c = Instantiate(currentBullet, pos3, Quaternion.identity, bulletsParent);

                    yield return new WaitForSeconds(0.000000000001f);
                    float waitTime = 0.15f;
                    if (isTimeScaleChanged)
                    {
                        waitTime = waitTime * (1 / Time.timeScale);
                    }
                    yield return new WaitForSecondsRealtime(waitTime);
                }
            }
            else
            {
                for (int degree = startDegree; degree <= endDegree; degree += 5)
                {
                    Vector3 pos1 = calculateManager.GetPosition(degree - 5, radius);
                    Vector3 pos2 = calculateManager.GetPosition(degree, radius);
                    Vector3 pos3 = calculateManager.GetPosition(degree + 5, radius);

                    GameObject a = Instantiate(currentBullet, pos1, Quaternion.identity, bulletsParent);
                    GameObject b = Instantiate(currentBullet, pos2, Quaternion.identity, bulletsParent);
                    GameObject c = Instantiate(currentBullet, pos3, Quaternion.identity, bulletsParent);

                    yield return new WaitForSeconds(0.000000000001f);
                    float waitTime = 0.15f;
                    if (isTimeScaleChanged)
                    {
                        waitTime = waitTime * (1 / Time.timeScale);
                    }
                    yield return new WaitForSecondsRealtime(waitTime);
                }
            }            
        }
    }
}