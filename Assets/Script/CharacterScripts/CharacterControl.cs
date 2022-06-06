using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyLibraryGame;
using UnityEngine.UI;

public class CharacterControl : MonoBehaviour
{

    float inputX;
    float inputY;
    Animator Anim;
    Vector3 AvailableDirection;
    Camera MainCam;
    float MaxLenght=0;
    float RotationSpeed=10;
    float MaxSpeed;
    float[] LeftAndRightMoveValue = { 0.11f, 0.3f, 0.54f,1f };
    float[] CrouchMoveValue = { 0.11f, 0.25f, 0.5f, 0.75f,1f };
    private float Health;
    public Image HealthBar;
    public GameObject GameControl;
    MyLibraryAnim MoveAnim = new MyLibraryAnim();
    void Start()
    {
        Health = 100;
        Anim = GetComponent<Animator>();
        MainCam = Camera.main;
    }

    void LateUpdate()
    {
        MoveAnim.ForwardMove(Anim, 0.2f, 1f, "Speed", MaxLenght);
        MoveAnim.LeftMoveAnim(Anim, "LeftMove", MoveAnim.CreateAnimMoveValue(LeftAndRightMoveValue));
        MoveAnim.RightMoveAnim(Anim, "RightMove", MoveAnim.CreateAnimMoveValue(LeftAndRightMoveValue));
        MoveAnim.CrouchMoveAnim(Anim, "CrouchMove", MoveAnim.CreateAnimMoveValue(CrouchMoveValue));
        MoveAnim.BackWalkAnim(Anim, "BackWalk");
        MoveAnim.CharacterRotation(MainCam, gameObject, RotationSpeed);

    }
  
    public void TakeDamage(float Hit)
    {
        Health -= Hit;
        HealthBar.fillAmount = Health / 100;
        if (Health<=0)
        {
            GameControl.GetComponent<GameControl>().Lose();
        }
    }



}
