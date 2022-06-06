using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("General Settings")]
    private Animator Anim;
    private Vector3 StartPosition;
    private NavMeshAgent navMesh;
    private GameObject Target;
    private float Health;

    [Header("Target Settings")]
    public float SuspicionRange = 10f;
    public float EnemyFireRange = 7f;
    public GameObject MainTarget;

    [Header("Patrol Settings")]
    public GameObject[] PartrolPoints1;
    public GameObject[] PartrolPoints2;
    public GameObject[] PartrolPoints3;
    int PartrolTarget;
    int TotalPartrolPoint;
    Coroutine PartrolSettingsCoroutine;
    Coroutine PartrolTimeControl;
    bool isTherePartrol;
    bool PartrolKey;
    bool isTherFire;
    bool ÝsThereSuspect = false;
    public int WhichPartrolPoints;
    private GameObject[] ActivePartrolPoints;
    public bool CanNpcPartrol;

    [Header("Gun Settings")]
    public AudioSource[] Voices;
    public ParticleSystem[] Effects;
    public float FireFrequency1 = .2f;
    float FireFrequency2;
    public float HitPower;
    public float Range;
    public GameObject FirePoint;


    void Start()
    {
        Health = 100;
        navMesh = gameObject.GetComponent<NavMeshAgent>();
        Anim = gameObject.GetComponent<Animator>();
        StartPosition = transform.position;
        if (CanNpcPartrol)
        {
            PartrolTimeControl = StartCoroutine(StartPartrolTime());
        }


    }

    void LateUpdate()
    {
        if (navMesh.stoppingDistance == 1 && navMesh.remainingDistance <= 1)
        {
            Anim.SetBool("Walk", false);
            transform.rotation = Quaternion.Euler(0, 180, 0);
            if (CanNpcPartrol)
            {
                isTherePartrol = false;
                PartrolTimeControl = StartCoroutine(StartPartrolTime());
                StopCoroutine(PartrolSettingsCoroutine);
            }         
            navMesh.stoppingDistance = 0;
          //  navMesh.isStopped = true;
        }

        if (PartrolKey && CanNpcPartrol)
        {
            PartrolSettingsCoroutine = StartCoroutine(NpcPartrolSettings(PartrolControlPoint()));
        }

        EnemySuspicion();
        EnemyFire();
    }

    GameObject[] PartrolControlPoint()
    {

        switch (WhichPartrolPoints)
        {
            case 1:
                ActivePartrolPoints = PartrolPoints1;
                break;
            case 2:
                ActivePartrolPoints = PartrolPoints2;
                break;
            case 3:
                ActivePartrolPoints = PartrolPoints3;
                break;
        }
        return ActivePartrolPoints;
    }

    IEnumerator NpcPartrolSettings(GameObject[] PartrolPoints)
    {
        PartrolKey = false;
        isTherePartrol = true;
        Anim.SetBool("Walk", true);
        PartrolTarget = 0;
        TotalPartrolPoint = PartrolPoints.Length - 1;

        while (true)
        {
            if (Vector3.Distance(transform.position, PartrolPoints[PartrolTarget].transform.position) < 1f)
            {
                if (TotalPartrolPoint > PartrolTarget)
                {
                    ++PartrolTarget;
                    navMesh.SetDestination(PartrolPoints[PartrolTarget].transform.position);
                }
                else
                {
                    navMesh.stoppingDistance = 1;
                    navMesh.SetDestination(StartPosition);

                }

            }
            else
            {
                if (TotalPartrolPoint > PartrolTarget)
                {
                    navMesh.SetDestination(PartrolPoints[PartrolTarget].transform.position);
                }
            }
            yield return null;
        }

    }

    IEnumerator StartPartrolTime()
    {
        while (true && !isTherePartrol)
        {
            yield return new WaitForSeconds(5f);
            PartrolKey = true;
            StopCoroutine(PartrolTimeControl);
        }
    }


    private void EnemyFire()
    {

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, EnemyFireRange);

        foreach (Collider objects in hitColliders)
        {

            if (objects.gameObject.CompareTag("Player"))
            {         
               Fire(objects.gameObject);
            }
            else
            {
               
                if (isTherFire)
                {
                    Anim.SetBool("Walk", true);
                    //   navMesh.isStopped = false;
                    Anim.SetBool("Fire", false);
                    isTherFire = false;
                }

            }   
        }
    }

    private void EnemySuspicion()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, SuspicionRange);

        foreach (Collider objects in hitColliders)
        {
            if (objects.gameObject.CompareTag("Player"))
            {
                if (Anim.GetBool("Run"))
                {
                    Anim.SetBool("Run", false);
                    Anim.SetBool("Walk", true);
                }
                else
                {
                    Anim.SetBool("Walk", true);
                }
                Target = objects.gameObject;            
                navMesh.SetDestination(Target.transform.position);
                ÝsThereSuspect = true;
                if (CanNpcPartrol)
                {
                    StopCoroutine(PartrolSettingsCoroutine);
                }                         
            }
            else
            {
                
                if (ÝsThereSuspect)
                {
                    Target = null;

                    if (transform.position != StartPosition)
                    {
                        navMesh.stoppingDistance = 1;
                        navMesh.SetDestination(StartPosition);
                        if (navMesh.remainingDistance <= 1)
                        {
                            Anim.SetBool("Walk", false);
                            transform.rotation = Quaternion.Euler(0, 180, 0);
                        }
                    }
                    ÝsThereSuspect = false;
                    if (CanNpcPartrol)
                    {
                        PartrolSettingsCoroutine = StartCoroutine(NpcPartrolSettings(PartrolControlPoint()));
                    }
                  
                }

            }
        }
    }

    private void Fire(GameObject Target2)
    {
        isTherFire = true;
        Vector3 Distance = Target2.gameObject.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(Distance, Vector3.up);
       
        Anim.SetBool("Walk", false);
        //   navMesh.isStopped = false;
        Anim.SetBool("Fire", true);
        RaycastHit hit;
        if (Physics.Raycast(FirePoint.transform.position, FirePoint.transform.forward, out hit, Range))
        {
            Color color = Color.red;
            Vector3 Pos = new Vector3(Target2.transform.position.x, Target2.transform.position.y + 1.5f, Target2.transform.position.z);
            Debug.DrawLine(FirePoint.transform.position, Pos, color);
            if (Time.time > FireFrequency2)
            {             
                Effects[0].Play();
                Voices[0].Play();
               transform.rotation = rotation;
                if (hit.transform.gameObject.CompareTag("Player"))
                {
                    hit.transform.gameObject.GetComponent<CharacterControl>().TakeDamage(HitPower);
                    Instantiate(Effects[1], hit.point, Quaternion.LookRotation(hit.normal));
                }
                else
                {
                    Instantiate(Effects[2], hit.point, Quaternion.LookRotation(hit.normal));
                }
                FireFrequency2 = Time.time + FireFrequency1;
            }

        }


    }

    public void EnemyDamage(float hit)
    {
        Health -= hit;

        if (!ÝsThereSuspect)
        {
            Anim.SetBool("Run", true);
            navMesh.SetDestination(MainTarget.transform.position);
        }
        if (Health <= 0)
        {
            Anim.Play("Dead");
            Destroy(gameObject, 2f);
        }
       

    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 7f);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 10f);

         
    }
}
