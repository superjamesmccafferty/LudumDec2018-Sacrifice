using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using TobiasUN.Core.Events;
using TobiasUN._2D.Movement;
using TobiasUN.Core.Base.MonoBehaviours;
using TobiasUN._2D.Collisions;

namespace Sacrifice
{
    public class CharacterController2D : MonoBehaviour
    {
        [Header("Player Settings")]

        [SerializeField]
        SOPlayerSettings_BZ _player_settings;

        [SerializeField]
        [Tooltip("Contains registered checks")]
        RadialCollisionChecker2D _radial_checker;


        [SerializeField]
        ShootManager _shoot_manager;    // Needs changing from some generic ability system




        const string c_grounded_check_id = "Grounded";



        SPlayerSettings_BZ _ps;

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



        // TESTING
        ITranslator2D<Rigidbody2D> _mover;






        #region Construction

        public static DConstructionBlueprint<CharacterController2D> GetConstructionBlueprint(SPlayerSettings_BZ settings)
        {
            return (GameObject ob) =>
            {

                if (ComponentConstruction.ContainsComponent<CharacterController2D>(ob))
                {
                    Debug.LogWarning("object should not have multiple CharacterController2D");
                }

                CharacterController2D comp = ob.AddComponent<CharacterController2D>();

                comp._ps = settings;

                return comp;
            };

        }

        #endregion


        void Awake()
        {
            _ps = _player_settings.Settings;

            // Remove and expose to Editor?????
            _rb2 = GetComponent<Rigidbody2D>();

        }




        void Start()
        {
            // Will probably end up removing, need to figure out this part of the system. 
            PlayerStateManager manager = gameObject.GetComponent<PlayerStateManager>();

            // replace with some kind of enum
            _mover = new VelocityTranslatorRigidbody2D(_ps.movement_smoothing);

            // Check that radial checks are present
            if (!_radial_checker.ContainsChecks(c_grounded_check_id))
            {
                Debug.LogWarning("All checks not present in radial checker");
            }
        }


        void FixedUpdate()
        {
            bool wasGrounded = _is_grounded;
            _is_grounded = false;


            if (_radial_checker.PerformCheck("Grounded", gameObject))
            {
                _is_grounded = true;
                if (!wasGrounded)
                {
                    OnLand.Raise();
                    OnJumpStop.Raise();
                }

            };


            if (_just_shot)
            {
                _just_shot = false;
                OnAttackStop.Raise();
            }
        }


        public void Move(float move, bool jump)
        {

            if (_is_grounded || _ps.is_air_control_on)
            {

                _mover.Move(_rb2, new Vector2(move * _ps.move_speed, _rb2.velocity.y));

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
                _rb2.AddForce(new Vector2(0f, _ps.jump_force));
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