using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShortRifle : MonoBehaviour
{
    [Header("Settings")]
    public float FireFrequency1=.2f;
    float FireFrequency2;
    public float Range;
    public int TotalBulletAmount;
    public int MagCapacity;
    int RemainingBullet;
    public float HitPower = 25;
    public TextMeshProUGUI RemainingBullet_Text;
    public TextMeshProUGUI TotalBulletAmount_Text;
    [Header("Voices")]
    public AudioSource[] Voices;

    [Header("Effect")]
    public ParticleSystem[] Effects;

    [Header("General Settings")]
    public Camera MainCam;
    public Animator CharacterAnimator;

    void Start()
    {
        RemainingBullet = MagCapacity;
        RemainingBullet_Text.text = MagCapacity.ToString();
        TotalBulletAmount_Text.text = TotalBulletAmount.ToString();

    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
        if (CharacterAnimator.GetBool("Reload"))
        {
            ReloadTrans();
        }
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (Time.time > FireFrequency2 && RemainingBullet>0)
            {           
                Fire();
                FireFrequency2 = Time.time + FireFrequency1;
            }
            else if (RemainingBullet==0)
            {
                Voices[1].Play();
            }
          
        }
    }


    public void Fire()
    {
        RemainingBullet--;
        Effects[0].Play();
        Voices[0].Play();
        CharacterAnimator.Play("Fire");
        RaycastHit hit;
        if (Physics.Raycast(MainCam.transform.position,MainCam.transform.forward,out hit,Range))
        {
            GameObject enemy = hit.transform.gameObject;
            if (enemy.CompareTag("Enemy"))
            {
                enemy.GetComponent<Enemy>().EnemyDamage(HitPower);
                Instantiate(Effects[2], hit.point, Quaternion.LookRotation(hit.normal));
            }
            else
            {
                Instantiate(Effects[1], hit.point, Quaternion.LookRotation(hit.normal));

            }
        }
        RemainingBullet_Text.text = RemainingBullet.ToString();
        TotalBulletAmount_Text.text = TotalBulletAmount.ToString();
    }
    public void Reload()
    {
        if (TotalBulletAmount!=0 && RemainingBullet != MagCapacity)
        {
            CharacterAnimator.Play("Reload");
            if (!Voices[2].isPlaying)
            {
                Voices[2].Play();
            }
        
        }
    }

    void ReloadTrans()
    {
        TotalBulletAmount += RemainingBullet;
        if (MagCapacity >= TotalBulletAmount)
        {
            RemainingBullet = TotalBulletAmount;
            TotalBulletAmount = 0;
        }
        else
        {
            RemainingBullet = MagCapacity;
            TotalBulletAmount -= MagCapacity;
        }
        RemainingBullet_Text.text = RemainingBullet.ToString();
        TotalBulletAmount_Text.text = TotalBulletAmount.ToString();
        CharacterAnimator.SetBool("Reload", false);
    }

}
