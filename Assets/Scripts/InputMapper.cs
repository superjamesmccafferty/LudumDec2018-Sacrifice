﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sacrifice
{
    public class InputMapper : MonoBehaviour
    {

        [SerializeField]
        KeyCode _jump_key;

        [SerializeField]
        KeyCode _left_key;

        [SerializeField]
        KeyCode _right_key;



        CharacterController2D cont;


        void Start()
        {
            cont = gameObject.GetComponent<CharacterController2D>();
        }


        void Update()
        {

            float move = 0;
            bool jump;



            if (Input.GetKey(_left_key))
            {
                move = -1;
            }
            else if (Input.GetKey(_right_key))
            {
                move = 1;
            }

            jump = Input.GetKeyDown(_jump_key) ? true : false;



            cont.Move(move, jump);
        }



    }
}