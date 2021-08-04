using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleSettings : MonoBehaviour
{
    public static int countSoldiers;
    [SerializeField] private GameObject _enemySoldier;
    [SerializeField] private Transform _enemyCastle;
    [SerializeField] private List<Transform> _spawnPositions;
    private float _distanceToFight;

    private void Start()
    {
        countSoldiers = 3;

        StartCoroutine(AddingSoldiers());
        StartCoroutine(Fight());
    }
    private void Update()
    {
        var list = EnemyFightArea.targetList;
        if(list.Count > 0 && countSoldiers > 0)
        {
            _distanceToFight = Vector3.Distance(_enemyCastle.position, list.First().transform.position);

            if(_distanceToFight <= 10)
                CreateSoldiers();
        }
    }

    IEnumerator AddingSoldiers()
    {
        while(countSoldiers <= 30)
        {
            yield return new WaitForSeconds(Random.Range(20,60));
            countSoldiers++;
        }
    }
    IEnumerator Fight()
    {
        while(true)
        {
            yield return new WaitForSeconds(90);
            while(countSoldiers > 0)
                CreateSoldiers();
        }
    }

    private void CreateSoldiers()
    {
        Instantiate(_enemySoldier, _spawnPositions[Random.Range(0,3)].position, Quaternion.identity);
        countSoldiers--;
    }

}
