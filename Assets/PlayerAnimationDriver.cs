using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationDriver : MonoBehaviour
{

    [SerializeField]
    Animator _player_animator;



    public void Update()
    {

        if (Input.GetKeyDown(KeyCode.U)) Jump(true);
        if (Input.GetKeyDown(KeyCode.I)) Jump(false);
        if (Input.GetKeyDown(KeyCode.O)) Attack(true);
        if (Input.GetKeyDown(KeyCode.P)) Attack(false);


    }



    public void Jump(bool isJump)
    {

        _player_animator.SetBool("IsJump", isJump);

    }

    public void Attack(bool isAttack)
    {
        _player_animator.SetBool("IsAttack", isAttack);
    }
}
