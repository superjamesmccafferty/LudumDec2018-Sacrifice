using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sacrifice
{
    public class PlayerAnimationDriver : MonoBehaviour
    {

        [SerializeField]
        CharacterController2D _char_cont;

        [SerializeField]
        Animator _player_animator;



        public void Start()
        {

            _char_cont.Subscribe_OnAttackStart(() => Attack(true));
            _char_cont.Subscribe_OnAttackStop(() => Attack(false));
            _char_cont.Subscribe_OnJumpStart(() => Jump(true));
            _char_cont.Subscribe_OnJumpStop(() => Jump(false));
            _char_cont.Subscribe_OnRunStart(() => Run(true));
            _char_cont.Subscribe_OnRunStop(() => Run(false));
        }


        public void Jump(bool isJump)
        {
            _player_animator.SetBool("IsJump", isJump);
        }

        public void Attack(bool isAttack)
        {
            _player_animator.SetBool("IsAttack", isAttack);
        }

        public void Run(bool isRun)
        {
            _player_animator.SetBool("IsRun", isRun);
        }
    }
}