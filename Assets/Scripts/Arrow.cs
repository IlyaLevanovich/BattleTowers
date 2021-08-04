using UnityEngine;

public class Arrow : MonoBehaviour
{   
    [HideInInspector]public GameObject Parent;
    [SerializeField]private float _speed;
    private GameObject _target;
    private float _damage = 1f;
    
    private void Start()
    {
        transform.SetParent(Parent.transform);
        _target = Parent.GetComponent<Soldier>().TargetToAttack;
        SetRotation();
    }
    private void Update() 
    {
        if(_target == null)
            Destroy(gameObject);
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, Time.deltaTime * _speed );
            SetRotation();
        }
    }
    private void SetRotation() => transform.LookAt(_target.transform, Vector3.up);

    private void OnCollisionEnter(Collision collision)
    {
        IEnemyTakeDamage iEnemy = collision.gameObject.GetComponent<IEnemyTakeDamage>();
        if(iEnemy != null)
        {
            iEnemy.TakeDamage(this._damage);
            Destroy(gameObject);
        }
    }

}
