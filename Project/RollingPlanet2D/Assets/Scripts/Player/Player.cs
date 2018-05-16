using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace MainScene.Player
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class Player : MonoBehaviour
    {
        #region Properties
        public int Hp { get; set; } = 100;
        public float Speed { get; } = 60.0f;
        public float AvoidTime { get; set; } = 1.0f;

        private State CurrentState { get; set; }
        #endregion

        #region Cashed Components
        new protected Transform transform;
        new protected Rigidbody2D rigidbody2D;
        protected Transform parentTransform;
        protected Animator animator;
        protected Slider hpSlider;
        #endregion

        #region Enum
        public enum State { Idle, Move, Damage, Die }
        public enum Direction { Left, Right }
        #endregion

        private void Awake()
        {
            InitComponent();
        }

        private void Start()
        {
            InitVariable();
        }

        private void Update()
        {
#if UNITY_EDITOR
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                Move(Direction.Left);
                animator.SetBool("isIdle", false);
                animator.SetBool("isLeftWalking", true);
                animator.SetBool("isRightWalking", false);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                Move(Direction.Right);
                animator.SetBool("isIdle", false);
                animator.SetBool("isLeftWalking", false);
                animator.SetBool("isRightWalking", true);
            }
            else
            {
                animator.SetBool("isIdle", true);
                animator.SetBool("isLeftWalking", false);
                animator.SetBool("isRightWalking", false);
            }
#elif UNITY_ANDROID
        
#endif
        }

        /// <summary>
        /// Initializes components.
        /// </summary>
        protected void InitComponent()
        {
            Debug.Log($"Initialize {name}'s components.");
            transform = GetComponent<Transform>();
            parentTransform = (transform.parent ? transform.parent.GetComponent<Transform>() : SetParentTransform());
            animator = GetComponent<Animator>();
            rigidbody2D = GetComponent<Rigidbody2D>();
            rigidbody2D.simulated = true;
            rigidbody2D.gravityScale = 0;

            hpSlider = GameObject.Find("PlayerHP").GetComponent<Slider>();
            // InitializeRigidbody2D();
        }

        /// <summary>
        /// Initializes variables.
        /// </summary>
        protected void InitVariable()
        {
            Debug.Log($"Initialize {name}'s variables.");
            CurrentState = State.Idle;
        }

        /// <summary>
        /// Moves player to Direction.
        /// </summary>
        /// <param name="direction">Use to verify direction</param>
        public void Move(Direction direction)
        {
            int dir = 0;
            switch (direction)
            {
                case Direction.Left:
                    dir = 1;
                    // MoveLeft();
                    break;
                case Direction.Right:
                    dir = -1;
                    // MoveRight();
                    break;
            }
            MoveTo(dir);
        }

        /// <summary>
        /// Reduces player's Hp.
        /// </summary>
        /// <param name="damage"></param>
        public void Damage(int damage)
        {
            if (CurrentState == State.Move || CurrentState == State.Idle)
            {
                Hp -= damage;
                hpSlider.value = Hp * 0.01f;
                if (Hp > 0)
                {
                    Avoid(AvoidTime);
                }
                else
                {
                    Die();
                }
            }
        }

        /// <summary>
        /// Makes player avoid bullets during avoidTime.
        /// </summary>
        /// <param name="avoidTime">avoidTime</param>
        public void Avoid(float time)
        {
            CurrentState = State.Damage;
            StartCoroutine(EAvoid(time));
        }

        /// <summary>
        /// Called when player's Hp is same or lower than 0.
        /// </summary>
        public void Die()
        {
            CurrentState = State.Die;
            // make die
            StopAllCoroutines();
        }

        private Transform SetParentTransform()
        {
            Debug.Log("Exception occured. Instantiates new parent object.");
            GameObject parentObj = new GameObject("Player");
            Transform parentTransform = parentObj.GetComponent<Transform>();
            transform.SetParent(parentTransform);
            return parentTransform;
        }

        /*
        private void InitializeRigidbody2D()
        {
            myRigidbody2D.simulated = true;
            myRigidbody2D.gravityScale = 0;
        }
        */

        private void MoveTo(int direction)
        {
            CurrentState = State.Move;
            parentTransform.Rotate(0, 0, direction * Speed * Time.deltaTime, Space.Self);
        }

        private IEnumerator EAvoid(float time)
        {
            CurrentState = State.Damage;
            yield return new WaitForSeconds(time);
            CurrentState = State.Idle;
        }
    }
}