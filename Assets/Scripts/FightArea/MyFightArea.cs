using System.Collections.Generic;
using UnityEngine;

public class MyFightArea : MonoBehaviour
{
    public static List<GameObject> targetList;
    
    private void Start() => targetList = new List<GameObject>();

    private void OnCollisionEnter(Collision collision) 
    {
        if(collision.gameObject.GetComponent<Enemy>())
            targetList.Add(collision.gameObject);
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.GetComponent<Enemy>())
            targetList.Remove(collision.gameObject);
    }
}
