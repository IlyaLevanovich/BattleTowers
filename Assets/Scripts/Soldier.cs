using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class Soldier : MonoBehaviour, ITakeDamage
{
    [HideInInspector] public GameObject TargetToAttack;
    [SerializeField] private Transform _playerCastle;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private GameObject _enemyCastle;
    [SerializeField] private GameObject _arrow;
    private float _distance;
    private string _nameTower;
    private float _health;
    private int _armor;
    private bool _isBattle;
    private NavMeshAgent _agent;
    private Animator _animator;

    public void TakeDamage(float damage)
    {
        _health -= damage / _armor;
        if(_health <= 0)
            DestoryAllReferences();
    }
    //Метод вызывается из Animation Events
    public void Shoot()
    {
        _arrow.GetComponent<Arrow>().Parent = gameObject;
        Instantiate(_arrow, _shootPoint.position, Quaternion.identity);
    }
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _health = 5;
        _armor = 2;

        SetTarget();
    }
    private void Start() => _agent.SetDestination(TargetToAttack.transform.position);  

    private void SetTarget()
    {
        var targetsList = GameplayConfiguration.TargetBuild;
        TargetToAttack = targetsList.Count > 0 ? targetsList[Random.Range(0,targetsList.Count)] : _enemyCastle;
    }

    private void Update()
    {
        _isBattle = GameplayConfiguration.IsBattle;

        if(_isBattle)
        {
            var list = GameplayConfiguration.TargetBuild;
            var area = MyFightArea.targetList;

            if(area.Count > 0)
                TargetToAttack = area.First();
            else
            {
                if(TargetToAttack == null)
                    SetTarget();
            } 
            SetRotation();
        }  
    }
    private void FixedUpdate()
    {
        CustomSetDestination();
        
        if(!_isBattle)
        {
            _agent.destination = _playerCastle.transform.position;
            _animator.StopPlayback();
        }
    }
    private void SetRotation() => transform.LookAt(TargetToAttack.transform, Vector3.up);
    
    private void CustomSetDestination()
    {
        if(TargetToAttack != null)
        {
            _distance = Vector3.Distance(transform.position, TargetToAttack.transform.position);
            if(_distance > 3f)
            {
                _agent.destination = TargetToAttack.transform.position;
                _animator.Play("Run");
            }
            else
            {
                _agent.destination = transform.position;
                _animator.StopPlayback();  
                _animator.Play("Throw");
            }
        }
    }

    private void DestoryAllReferences()
    {
        EnemyFightArea.targetList.Remove(gameObject);
        Destroy(gameObject);
    }
    
}


