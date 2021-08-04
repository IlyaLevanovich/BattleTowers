using UnityEngine;

public class EnemyArrow : MonoBehaviour
{
    [HideInInspector]public GameObject Parent;
    [SerializeField]private float _speed;
    private GameObject _target;
    private float _damage = 1f;
    
    private void Start()
    {
        transform.SetParent(Parent.transform);
        _target = Parent.GetComponent<Enemy>().TargetToAttack;
    }
    private void OnCollisionEnter(Collision collision) 
    {
        ITakeDamage takeDamage = collision.gameObject.GetComponent<ITakeDamage>();
        if(takeDamage != null)
        {
            takeDamage.TakeDamage(this._damage);
            Destroy(gameObject);
        }    
    }


    private void Update()
    {
        if(_target == null)
            Destroy(gameObject);
        else
            transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, Time.deltaTime * _speed);
    }

}
