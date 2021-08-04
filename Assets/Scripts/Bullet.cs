using System.Linq;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	[SerializeField]private float _speed;
    private GameObject _target;
    private float _damage = 1f;

    private void Start()
    {
    	if(EnemyFightArea.targetList.Count > 0) 
            _target = EnemyFightArea.targetList.First();
    }

    private void Update()
    {
        if(_target != null)
            transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, Time.deltaTime * _speed);
        else
            Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        ITakeDamage iTakeDamage = collision.gameObject.GetComponent<ITakeDamage>();
        if(iTakeDamage != null)
        {
            iTakeDamage.TakeDamage(this._damage);
            Destroy(gameObject);
        }
            
    }

}
