using UnityEngine;
using System.Linq;

public class MyBullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    private GameObject _target;
    private float _damage = 1f;

    private void Start()
    {
    	if(MyFightArea.targetList.Count > 0) 
            _target = MyFightArea.targetList.First();
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
    	IEnemyTakeDamage iEnemy = collision.gameObject.GetComponent<IEnemyTakeDamage>();
        if(iEnemy != null)
        {
            iEnemy.TakeDamage(this._damage);
            Destroy(gameObject);
        }
    }

}
