using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyBuildingCreator : MonoBehaviour
{
    public static List<Transform> buildPositions = new List<Transform>();
    [SerializeField] private GameObject _tower;
    [SerializeField] private GameObject _farm;
    private List<GameObject> _countFarm = new List<GameObject>();
    private List<GameObject> _countTower = new List<GameObject>();
    private Dictionary<string, GameObject> _buildingType;
    private int _maxCount = 4;
    private Quaternion _correctRotation = Quaternion.Euler(new Vector3(0,90,0));

    private void Awake() => SetList();

    private void Start()
    {
        _buildingType = new Dictionary<string, GameObject>
        {
            {"Farm", _farm},
            {"Tower", _tower}
        };

        StartCoroutine(CreateBuild());
    }

    private Vector3 ChangePosition()
    {
        Vector3 acceptPosition = buildPositions.ElementAt(Random.Range(0,buildPositions.Count)).position;
        acceptPosition.y = 1.03f;
        return acceptPosition;
    }

    private List<GameObject> FindingsFarms()
    {
        EnemyFarm[] arrayEnemyFarm = GameObject.FindObjectsOfType<EnemyFarm>();
        List<GameObject> list_countFarm = new List<GameObject>();
        for(int i = 0; i < arrayEnemyFarm.Length; i ++)
        {
            list_countFarm.Add(arrayEnemyFarm[i].gameObject);
        }
        return list_countFarm;
    }
    private List<GameObject> FindingsTowers()
    {
        EnemyStrikeTower[] arrayEnemyTower = GameObject.FindObjectsOfType<EnemyStrikeTower>();
        List<GameObject> list_countTower = new List<GameObject>();
        for(int i = 0; i < arrayEnemyTower.Length; i ++)
        {
            list_countTower.Add(arrayEnemyTower[i].gameObject);
        }
        return list_countTower;
    }
    private void SetList()
    {
        _countFarm.Clear();
        _countTower.Clear();
        
        _countFarm.AddRange(FindingsFarms());
        _countTower.AddRange(FindingsTowers());
    }
    private void SelectCreateObject()
    {
        int farms = _countFarm.Count;
        int towers = _countTower.Count;
        var type = _buildingType;

        if(farms < _maxCount && towers < _maxCount)
            Instantiate(type.ElementAt(Random.Range(0, type.Count)).Value,ChangePosition(), _correctRotation);
        else if(farms < _maxCount)
            InstantiatedBuilding("Farm");
        else if(towers < _maxCount)
            InstantiatedBuilding("Tower");

    }
    private void InstantiatedBuilding(string type) => Instantiate(_buildingType[type], ChangePosition(), _correctRotation);

    private IEnumerator CreateBuild()
    {
        while(_countFarm.Count < _maxCount || _countTower.Count < _maxCount)
        {
            yield return new WaitForSeconds(Random.Range(5, 60));
            if(buildPositions.Count > 0)
            {
                SelectCreateObject();
                SetList();
                GameplayConfiguration.OverwriteMyList();
            }
            
        } 
    }
}
