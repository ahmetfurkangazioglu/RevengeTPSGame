using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyLibraryGame {
    public class MyLibraryAnim : MonoBehaviour
    {
        float MaxSpeedClass;
        float MaxInputClass;
        public List<float> CreateAnimMoveValue(float[] AnimMoveValues)
        {
            List<float> AnimMoveValue = new List<float>();
            foreach (var item in AnimMoveValues)
            {
                AnimMoveValue.Add(item);
            }
            return AnimMoveValue;
        }
        public void LeftMoveAnim(Animator Anim, string AnimName, List<float> AnimMoveValue)
        {
          
            if (Input.GetKey(KeyCode.A))
            {
                if (Input.GetKey(KeyCode.W))
                {
                    Anim.SetFloat(AnimName, AnimMoveValue[2]);
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    Anim.SetFloat(AnimName, AnimMoveValue[3]);

                }
                else if (Input.GetKey(KeyCode.LeftShift))
                {
                    Anim.SetFloat(AnimName, AnimMoveValue[1]);

                }
                else
                {
                    Anim.SetFloat(AnimName, AnimMoveValue[0]);

                }

            }
            if (Input.GetKeyUp(KeyCode.A))
            {
                Anim.SetFloat(AnimName, 0f);
            }
        }
        public void RightMoveAnim(Animator Anim, string AnimName, List<float> AnimMoveValue)
        {

            if (Input.GetKey(KeyCode.D))
            {
                if (Input.GetKey(KeyCode.W))
                {
                    Anim.SetFloat(AnimName, AnimMoveValue[2]);
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    Anim.SetFloat(AnimName, AnimMoveValue[3]);

                }
                else if (Input.GetKey(KeyCode.LeftShift))
                {
                    Anim.SetFloat(AnimName, AnimMoveValue[1]);

                }
                else
                {
                    Anim.SetFloat(AnimName, AnimMoveValue[0]);

                }

            }
            if (Input.GetKeyUp(KeyCode.D))
            {
                Anim.SetFloat(AnimName, 0f);
            }
        }
        public void CrouchMoveAnim(Animator Anim, string AnimName, List<float> AnimMoveValue)
        {

            if (Input.GetKey(KeyCode.C))
            {
                if (Input.GetKey(KeyCode.W))
                {
                    Anim.SetFloat(AnimName, AnimMoveValue[1]);
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    Anim.SetFloat(AnimName, AnimMoveValue[4]);

                }
                else if (Input.GetKey(KeyCode.A))
                {
                    Anim.SetFloat(AnimName, AnimMoveValue[3]);

                }
                else if (Input.GetKey(KeyCode.D))
                {
                    Anim.SetFloat(AnimName, AnimMoveValue[2]);

                }
                else
                {
                    Anim.SetFloat(AnimName, AnimMoveValue[0]);

                }

            }
            if (Input.GetKeyUp(KeyCode.C))
            {
                Anim.SetFloat(AnimName, 0f);
            }
        }
        public void BackWalkAnim(Animator Anim, string AnimName)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                Anim.SetBool(AnimName, true);
            }
            if (Input.GetKeyUp(KeyCode.S))
            {
                Anim.SetBool(AnimName, false);
            }
        }
        public void ForwardMove(Animator Anim,float WalkMaxSpeed, float RunMaxSpeed,string AnimName, float MaxLenght)
        {
            if (Input.GetKey(KeyCode.W))
            {
                MaxSpeedClass = WalkMaxSpeed;
                MaxInputClass = 1;
       
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    MaxSpeedClass = RunMaxSpeed;
                    MaxInputClass = 1;

                }
            }
            else
            {
                MaxSpeedClass = 0f;
                MaxInputClass = 0;
      
            }

            Anim.SetFloat(AnimName, Vector3.ClampMagnitude(new Vector3(MaxInputClass, 0, 0), MaxSpeedClass).magnitude, MaxLenght, Time.deltaTime * 10);

        }
        public void CharacterRotation(Camera Cam,GameObject Character,float RotationSpeed)
        {
            Vector3 CamOfset = Cam.transform.forward;
            CamOfset.y = 0;
            Character.transform.forward = Vector3.Slerp(Character.transform.forward, CamOfset, Time.deltaTime * RotationSpeed);
        }
    }
}
