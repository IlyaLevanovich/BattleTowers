using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyCastle : MonoBehaviour, IEnemyTakeDamage
{
    [SerializeField] private GameObject _projectile;
    [SerializeField] private Transform _shootPosition;
    private float _health;
    private float _rangeAttack = 5;
    private float _armor = 25;
    private bool _isReload;
    private float _distance;
    private event Action NotifyAboutGameOver;

    public void TakeDamage(float damage)
    {
        _health -= damage / _armor;

        if(_health <= 0)
            NotifyAboutGameOver();    
    }
    private void Awake() => NotifyAboutGameOver += GameOver;
    private void Start()
    {
        _isReload = true;
        _health = 1;
    }
    private void Update()
    {
        var list = EnemyFightArea.targetList;
        if(list.Count > 0 && _isReload)
        {
            _distance = Vector3.Distance(transform.position, list.First().transform.position);
            if(_distance <= _rangeAttack)
            {
                StartCoroutine(Shoot());
                _isReload = false;
            }     
        }
            
    } 
    private void GameOver()
    {
        Destroy(gameObject);
        Menu.resultLastGame = "You Win!";
        SceneManager.LoadScene("Menu");
    }
    private IEnumerator Shoot()
    {
        float reload = 2f;
        yield return new WaitForSeconds(reload);
        Instantiate(_projectile, _shootPosition.position, Quaternion.identity);  
        _isReload = true; 
    }
}
