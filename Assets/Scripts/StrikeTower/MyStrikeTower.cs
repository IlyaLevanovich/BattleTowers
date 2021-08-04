using System.Collections;
using UnityEngine;
using System.Linq;

public class MyStrikeTower : AbstractStrikeTower, ITakeDamage
{
	[SerializeField]private GameObject _bullet;
    [SerializeField]private Transform _shootPoint;
    private bool _isReload;
    private float _health;
    private float _radiusAttack = 5;
    private int _armor = 25;
    private Transform _target;

    public void TakeDamage(float damage)
    {
        _health -= damage / _armor;
        if(_health <= 0)
        {
            OverwriteEnemyTargets();
            RewardForDestruction();
        }  
    }
    protected override void Start()
    {
        _isReload = true;
        _health = 1;
    }
    protected override void Update()
    {
        if(MyFightArea.targetList.Count > 0 && _isReload)
		{
            _target = MyFightArea.targetList.First().transform;
            float distance = Vector3.Distance(transform.position, _target.position);
            
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

    private void OverwriteEnemyTargets()
    {
        GameplayConfiguration.OverwriteEnemyList();
        GameplayConfiguration.EnemyTargetBuild.Remove(gameObject);
    }
    private void RewardForDestruction()
    {
        Destroy(gameObject);
        BattleSettings.countSoldiers += 5; 
    }
}
