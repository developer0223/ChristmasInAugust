using Utility;
using Manager;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Player
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class Player : MonoBehaviour
    {
        #region Properties
        public int Hp { get; set; } = 100;
        public float Speed { get; set; } = 50.0f;
        public float AvoidTime { get; set; } = 3.0f;
        public float BlinkingTime { get; set; } = 0.2f;

        private State CurrentState { get; set; }
        private bool IsBlinking { get; set; } = false;
        #endregion

        #region Cashed Components
        new protected Transform transform;
        new protected Rigidbody2D rigidbody2D;
        protected Transform parentTransform;
        protected Animator animator;
        protected Image hpBar;
        protected GameManager gameManager;
        protected SpriteRenderer spriteRenderer;
        #endregion

        #region Enum
        public enum State { Idle, Move, Damage, Die }
        public enum Direction { Left, Right, None}
        #endregion

        #region Variables
        public const string IDLE = "isIdle";
        public const string LEFT = "isLeftWalking";
        public const string RIGHT = "isRightWalking";

        private bool isAvoiding = false;
        public bool isLeftPressed = false;
        public bool isRightPressed = false;
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
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                Move(Direction.Right);
            }
            else
            {
                Move(Direction.None);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                gameManager.GetOrCreateManager<ItemManager>().UseWatchItem();
            }
#elif UNITY_ANDROID
            if (isLeftPressed)
            {
                Move(Direction.Left);
            }
            else if (isRightPressed)
            {
                Move(Direction.Right);
            }
            else
            {
                Move(Direction.None);
            }
#endif
        }

        /// <summary>
        /// Initializes components.
        /// </summary>
        protected void InitComponent()
        {
            Debug.Log($"Initialize {name}'s components.");
            transform = GetComponent<Transform>();
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            parentTransform = (transform.parent ? transform.parent.GetComponent<Transform>() : SetParentTransform());
            animator = GetComponent<Animator>();
            rigidbody2D = GetComponent<Rigidbody2D>();
            rigidbody2D.simulated = true;
            rigidbody2D.gravityScale = 0;

            hpBar = GameObject.Find("HpBarFill").GetComponent<Image>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        /// <summary>
        /// Initializes variables.
        /// </summary>
        protected void InitVariable()
        {
            Debug.Log($"Initialize {name}'s variables.");
            CurrentState = State.Idle;
        }

        public bool PlayAnim(Direction direction, ref int dir)
        {
            if (CurrentState == State.Die)
            {
                return true;
            }
            switch (direction)
            {
                case Direction.Left:
                    dir = 1;
                    animator.SetBool(LEFT, true);
                    animator.SetBool(RIGHT, false);
                    animator.SetBool(IDLE, false);
                    break;
                case Direction.Right:
                    dir = -1;
                    animator.SetBool(LEFT, false);
                    animator.SetBool(RIGHT, true);
                    animator.SetBool(IDLE, false);
                    break;
                case Direction.None:
                    CurrentState = State.Idle;
                    animator.SetBool(LEFT, false);
                    animator.SetBool(RIGHT, false);
                    animator.SetBool(IDLE, true);
                    break;
            }
            return false;
        }

        /// <summary>
        /// Moves player to Direction.
        /// </summary>
        /// <param name="direction">Use to verify direction</param>
        public void Move(Direction direction)
        {
            int dir = 0;
            if(PlayAnim(direction, ref dir))
            { 
                return;
            }
            MoveTo(dir);
        }

        /// <summary>
        /// Reduces player's Hp.
        /// </summary>
        /// <param name="damage"></param>
        public void Damage(int damage)
        {
            if (!isAvoiding)
            {
                Hp -= damage;
                hpBar.fillAmount = Hp * 0.01f;
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
            if (CurrentState != State.Damage && CurrentState != State.Die)
            {
                CurrentState = State.Damage;
                StartCoroutine(EAvoid(time));
            }
        }

        /// <summary>
        /// Called when player's Hp is same or lower than 0.
        /// </summary>
        public void Die()
        {
            CurrentState = State.Die;

            gameManager.GetOrCreateManager<ScoreManager>().IsAlive = false;
            gameManager.GetOrCreateManager<ScoreManager>().RenewalScore();
            StopAllCoroutines();
            gameManager.GetOrCreateManager<EffectManager>().FadeIn(GameObject.Find("BlackWall").GetComponent<Image>(), 2.0f, (x) =>
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("Restart");
            });
        }

        private Transform SetParentTransform()
        {
            Debug.Log("Exception occured. Instantiates new parent object.");
            GameObject parentObj = new GameObject(Data.Tags.PLAYER);
            Transform parentTransform = parentObj.GetComponent<Transform>();
            transform.SetParent(parentTransform);
            return parentTransform;
        }

        private void MoveTo(int direction)
        {
            CurrentState = State.Move;
            parentTransform.Rotate(0, 0, direction * Speed * Time.deltaTime, Space.Self);
        }

        private IEnumerator EAvoid(float avoidTime)
        {
            Debug.Log("avoid");
            CurrentState = State.Damage;
            isAvoiding = true;
            float currentTime = 0.0f;
            Color color = spriteRenderer.color;
            bool blink = false;

            while (currentTime < avoidTime)
            {
                if (blink)
                {
                    IsBlinking = false;
                    color.a = 1;
                }
                else
                {
                    IsBlinking = true;
                    color.a = 0.5f;
                }
                spriteRenderer.color = color;
                blink = !blink;

                float waitTime = BlinkingTime;
                currentTime += waitTime;
                yield return new WaitForSeconds(waitTime);
            }

            color.a = 1f;
            spriteRenderer.color = color;

            CurrentState = State.Idle;
            isAvoiding = false;
        }
    }
}