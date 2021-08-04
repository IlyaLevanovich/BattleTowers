using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class Enemy : MonoBehaviour, IEnemyTakeDamage
{
    [HideInInspector] public GameObject TargetToAttack;
    [SerializeField] private GameObject _playerCastle;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private  GameObject _arrow;
    private NavMeshAgent _agent;
    private float _health;
    private float _targetDistance;
    private int _armor;
    private Animator _animator;

    public void TakeDamage(float damage)
    {
        _health -= damage / _armor;
        if(_health <= 0)
            DestroyAllReferences();
    }
    public void Shoot() //Вызывается в Animation Events
    {
        _arrow.GetComponent<EnemyArrow>().Parent = gameObject;
        Instantiate(_arrow, _shootPoint.position, Quaternion.identity);
    }
     private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _health = 5;
        _armor = 2;
    }
    private void Start()
    {
        SetTarget();
        _agent.SetDestination(TargetToAttack.transform.position);
    }

    private void SetTarget()
    {
        var list = GameplayConfiguration.EnemyTargetBuild;
        TargetToAttack = list.Count > 0 ? list[Random.Range(0,list.Count)] : _playerCastle;
    }

    private void Update()
    {
        var list = GameplayConfiguration.EnemyTargetBuild;
       
        if(EnemyFightArea.targetList.Count > 0)
            TargetToAttack = EnemyFightArea.targetList.First();
        else
        {
            if(list.Count == 0)
                TargetToAttack = _playerCastle;
            else if(TargetToAttack == null)
                TargetToAttack = list[Random.Range(0,list.Count)];
        }    
        SetRotation();
        SetDestination();  
    }
    private void SetRotation() => transform.LookAt(TargetToAttack.transform, Vector3.up);
    
    private void SetDestination()
    {
        if(TargetToAttack != null)
        {
            _targetDistance = Vector3.Distance(transform.position, TargetToAttack.transform.position);
            if(_targetDistance > 3)
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

    private void DestroyAllReferences()
    {
        MyFightArea.targetList.Remove(gameObject);
        Destroy(gameObject);
    }
}
