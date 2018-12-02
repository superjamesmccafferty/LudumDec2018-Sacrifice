using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using TobiasUN.Core.Events;

namespace Sacrifice
{
    public class CharacterController2D : MonoBehaviour
    {

        [Header("Movement Configuration")]

        //[SerializeField]
        //[Tooltip("The force with which the player jumps")]
        float _jump_force = 400f;

        [Range(0, .3f)]
        [SerializeField]
        float _movement_smoothing = .05f;

        [SerializeField]
        bool _is_air_control_on = false;

        [SerializeField]
        LayerMask _ground_mask;

        [SerializeField]
        Transform[] _ground_check;

        //[SerializeField]
        float _move_speed = 10;

        [SerializeField]
        ShootManager _shoot_manager;



        const float _grounded_radius = .2f;

        bool _is_grounded;

        Rigidbody2D _rb2;

        bool _is_facing_right = true;

        Vector3 _velocity = Vector3.zero;

        bool _just_shot = false;


        [Header("Events")]
        [SerializeField]
        ReliableEvent OnLand;

        [SerializeField] ReliableEvent OnJumpStart;
        [SerializeField] ReliableEvent OnJumpStop;
        [SerializeField] ReliableEvent OnRunStart;
        [SerializeField] ReliableEvent OnRunStop;
        [SerializeField] ReliableEvent OnAttackStart;
        [SerializeField] ReliableEvent OnAttackStop;






        void Awake()
        {
            _rb2 = GetComponent<Rigidbody2D>();
        }

        void Start()
        {
            PlayerStateManager manager = gameObject.GetComponent<PlayerStateManager>();
        }

        public void Init(float jump_force, float move_speed, ReliableEvent_float move_speed_change, ReliableEvent_float jump_force_change)
        {
            _jump_force = jump_force;
            _move_speed = move_speed;

            move_speed_change.Subscribe(m => _move_speed = m);
            jump_force_change.Subscribe(f => _jump_force = f);
        }

        void FixedUpdate()
        {
            bool wasGrounded = _is_grounded;
            _is_grounded = false;

            List<Collider2D> colliders = new List<Collider2D>();

            foreach (Transform t in _ground_check)
            {
                Collider2D[] temp_col = Physics2D.OverlapCircleAll(
                   t.position,
                   _grounded_radius,
                   _ground_mask);

                foreach (Collider2D col in temp_col)
                {
                    colliders.Add(col);
                }
            }

            for (int i = 0; i < colliders.Count; i++)
            {
                if (colliders[i].gameObject != gameObject)
                {
                    _is_grounded = true;
                    if (!wasGrounded)
                    {
                        OnLand.Raise();
                        OnJumpStop.Raise();
                    }
                }
            }

            if (_just_shot)
            {
                _just_shot = false;
                OnAttackStop.Raise();
            }
        }


        public void Move(float move, bool jump)
        {

            if (_is_grounded || _is_air_control_on)
            {

                // Move the character by finding the target velocity
                Vector3 targetVelocity = new Vector2(move * _move_speed, _rb2.velocity.y);

                // And then smoothing it out and applying it to the character
                _rb2.velocity = Vector3.SmoothDamp(
                    _rb2.velocity,
                    targetVelocity,
                    ref _velocity,
                    _movement_smoothing);


                // If the input is moving the player right and the player is facing left...
                if ((move > 0 && !_is_facing_right) || (move < 0 && _is_facing_right))
                    Flip();

                if (Mathf.Abs(move) > 0)
                {
                    OnRunStart.Raise();
                }
                else
                {
                    OnRunStop.Raise();
                }
            }

            if (_is_grounded && jump)
            {
                OnJumpStart.Raise();

                _is_grounded = false;
                _rb2.AddForce(new Vector2(0f, _jump_force));
                OnJumpStart.Raise();
            }
        }

        public void BumFire(float force)
        {
            _rb2.velocity += new Vector2(0, force);
        }

        public void Shoot()
        {
            _shoot_manager.Shoot(_is_facing_right);
            OnAttackStart.Raise();
            _just_shot = true;
        }


        private void Flip()
        {
            // Switch the way the player is labelled as facing.
            _is_facing_right = !_is_facing_right;

            // Multiply the player's x local scale by -1.
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }


        // --- EVENTS ---

        public CoreEventToken Subscribe_OnLand(UnityAction callback)
        {
            return OnLand.Subscribe(callback);
        }

        public CoreEventToken Subscribe_OnJumpStart(UnityAction callback)
        {
            return OnJumpStart.Subscribe(callback);
        }
        public CoreEventToken Subscribe_OnJumpStop(UnityAction callback)
        {
            return OnJumpStop.Subscribe(callback);
        }
        public CoreEventToken Subscribe_OnRunStart(UnityAction callback)
        {
            return OnRunStart.Subscribe(callback);
        }
        public CoreEventToken Subscribe_OnRunStop(UnityAction callback)
        {
            return OnRunStop.Subscribe(callback);
        }
        public CoreEventToken Subscribe_OnAttackStart(UnityAction callback)
        {
            return OnAttackStart.Subscribe(callback);
        }
        public CoreEventToken Subscribe_OnAttackStop(UnityAction callback)
        {
            return OnAttackStop.Subscribe(callback);
        }


    }
}