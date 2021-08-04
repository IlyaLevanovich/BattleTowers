using UnityEngine;

public class EnemyCreatePosition : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;
    private bool _isEmpty;
    
    private void Start() => _isEmpty = true;

    private void Update()
    {
        Ray ray = new Ray(transform.position, transform.up);

        _isEmpty = Physics.Raycast(ray, _layerMask) ? false : true;
        
        ChangeBuildCreator();
    }

    private void ChangeBuildCreator()
    {
        if(_isEmpty && !EnemyBuildingCreator.buildPositions.Contains(gameObject.transform))
            EnemyBuildingCreator.buildPositions.Add(gameObject.transform);
        else
            EnemyBuildingCreator.buildPositions.Remove(gameObject.transform);
    }

}
