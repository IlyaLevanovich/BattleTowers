using System.Collections;
using UnityEngine;
using System.Linq;

public class EnemyStrikeTower : AbstractStrikeTower, IEnemyTakeDamage
{
	[SerializeField] private GameObject _bullet;
	[SerializeField] private Transform _shootPoint;
	private bool _isReload;
	private float _health;
	private int _armor = 25;
	private float _radiusAttack = 5;

	public void TakeDamage(float damage)
    {
        _health -= damage / _armor;
		if(_health <= 0)
			DestroyAllReferences();
    }
	
	protected override void Start()
	{
		_isReload = true;
		_health = 1; 	
	}

	protected override void Update()
	{
		var list = EnemyFightArea.targetList;
		if(list.Count > 0 && _isReload)
		{
			float distance = Vector3.Distance(transform.position, list.First().transform.position);
			if(distance <= _radiusAttack)
			{
				StartCoroutine(SpawnBullet());
				_isReload = false;
			}	
		}
	}

	protected override IEnumerator SpawnBullet()
	{
		yield return new WaitForSeconds(1.5f);
		Instantiate(_bullet, _shootPoint.position, _shootPoint.rotation);
		_isReload = true;
	}

	protected override void DestroyAllReferences() => base.DestroyAllReferences();
}
