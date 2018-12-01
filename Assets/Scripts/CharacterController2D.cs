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

        [SerializeField]
        [Tooltip("The force with which the player jumps")]
        float _jump_force = 400f;

        [Range(0, .3f)]
        [SerializeField]
        float _movement_smoothing = .05f;

        [SerializeField]
        bool _is_air_control_on = false;

        [SerializeField]
        LayerMask _ground_mask;

        [SerializeField]
        Transform _ground_check;

        [SerializeField]
        float _move_speed = 10;

        [SerializeField]
        ShootManager _shoot_manager;





        const float _grounded_radius = .2f;

        bool _is_grounded;

        Rigidbody2D _rb2;

        bool _is_facing_right = true;

        Vector3 _velocity = Vector3.zero;



        [Header("Events")]
        [Space]

        [SerializeField]
        ReliableEvent OnLand;








        void Awake()
        {
            _rb2 = GetComponent<Rigidbody2D>();
        }

        void FixedUpdate()
        {
            bool wasGrounded = _is_grounded;
            _is_grounded = false;

            Collider2D[] colliders = Physics2D.OverlapCircleAll(
                _ground_check.position,
                _grounded_radius,
                _ground_mask);

            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                {
                    _is_grounded = true;
                    if (!wasGrounded)
                        OnLand.Raise();
                }
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
            }

            if (_is_grounded && jump)
            {
                _is_grounded = false;
                _rb2.AddForce(new Vector2(0f, _jump_force));
            }
        }

        public void Shoot()
        {
            _shoot_manager.Shoot(_is_facing_right);
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
    }
}