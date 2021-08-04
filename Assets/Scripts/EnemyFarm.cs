using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemyFarm : MonoBehaviour, IEnemyTakeDamage
{
    [SerializeField] private Image _healthBar;
    private int _level;
    private float _health;

    public void TakeDamage(float damage)
    {
        _health -= (damage/25);
        
        if(_health <= 0)
        {
			GameplayConfiguration.OverwriteMyList();
			Destroy(gameObject);
            GameplayConfiguration.TargetBuild.Remove(gameObject);
        }
    }
    private void Awake()
    {
        _level = 1;
        _health = 1;
    }
    private void Start()
    {
        StartCoroutine(AddLevel());
        StartCoroutine(AddSoldier());
    }
    private void Update() => _healthBar.fillAmount = _health;

    private IEnumerator AddSoldier()
    {
        while(true)
        {
            yield return new WaitForSeconds(60f / _level);
            BattleSettings.countSoldiers ++;
        }
    }
    private IEnumerator AddLevel()
    {
        while(_level < 3)
        {
            yield return new WaitForSeconds(Random.Range(120,300));
            _level ++;   
        }
    }

}
