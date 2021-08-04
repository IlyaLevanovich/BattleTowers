using System.Collections.Generic;
using UnityEngine;

public class EnemyFightArea : MonoBehaviour
{
    public static List<GameObject> targetList;

    private void Start() => targetList = new List<GameObject>();
    
    private void OnCollisionEnter(Collision collision) 
    {
        if(collision.gameObject.GetComponent<Soldier>())
            targetList.Add(collision.gameObject);
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.GetComponent<Soldier>())
            targetList.Remove(collision.gameObject);
    }
}
