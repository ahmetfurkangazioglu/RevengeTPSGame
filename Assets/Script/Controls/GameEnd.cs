using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnd : MonoBehaviour
{
    public GameObject Boss;
    public GameObject GameControl;
  
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Girdi");
            Boss.GetComponent<Animator>().Play("Dead");
            GameControl.GetComponent<GameControl>().Win();
        }
        else
        {
            Debug.Log("GirdiElse");
        }
    }
}
