using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;

public class PlayerCastle : MonoBehaviour, ITakeDamage
{
    public static float Distance;
    [SerializeField] private GameObject _projectile;
    [SerializeField] private Transform shootPosition;
    
    [SerializeField] private Image _healthBar;
    [SerializeField] private float _health;
    private int _armor = 25;

    private int _level;
    public int Level { get => _level; set => _level = value; }

    private int _distanceToAttack = 5;
    private Dictionary<int,int> _minedGold;
    
    private event Action NotifyAboutGameOver;
    
    public void TakeDamage(float damage)
    {
        _health -= damage / _armor;
        if(_health <= 0)
        {
            NotifyAboutGameOver?.Invoke();
            Destroy(gameObject);
        } 
    }
    private void SubscribeMethodsToEvent() => NotifyAboutGameOver += gameObject.AddComponent<GameOver>().LoadFinalScene;
    private void Awake()
    {
        _health = 1;
        Level = 1;
        _minedGold = new Dictionary<int, int>()
        {
            {1,10},
            {2,15},
            {3,20},
        };
        SubscribeMethodsToEvent();
    }
    private void Start()
    {
        StartCoroutine(GoldMiner.GoldMining(_minedGold, Level));
        StartCoroutine(Shooter.StartFire(_projectile, shootPosition, _distanceToAttack));
    }
    private void Update()
    {
        _healthBar.fillAmount = _health;

        if(MyFightArea.targetList.Count > 0)
            Distance = Vector3.Distance(transform.position, MyFightArea.targetList.First().transform.position);
    }
    private void OnCollisionEnter(Collision collision) 
    {
        Soldier soldier = collision.gameObject.GetComponent<Soldier>();
        if(soldier != null && !GameplayConfiguration.IsBattle)
        {
            Destroy(collision.gameObject);
            EnemyFightArea.targetList.Remove(soldier.gameObject);
            GameplayConfiguration.CountSoldiers ++;
        }
    }
}
public class GoldMiner : MonoBehaviour
{
    public static IEnumerator GoldMining(Dictionary<int,int> minedGold, int level)
    {
        while(true)
        {
            float delay = 7f;
            yield return new WaitForSeconds(delay);
            GameplayConfiguration.CountGold += minedGold[level];
        } 
    }
}
public class Shooter : MonoBehaviour
{
    public static IEnumerator StartFire(GameObject projectile, Transform shootPosition, float distanceToAttack)
    {
        float delay = 2f;
        while(true)
        {
            yield return new WaitForSeconds(delay);
            if(PlayerCastle.Distance <= distanceToAttack && MyFightArea.targetList.Count > 0)
                Instantiate(projectile, shootPosition.position, Quaternion.identity);  
        }   
    }
}
public class Improvement : MonoBehaviour
{
    public void ToImprove()
    {
        var playerCastle = GetComponent<PlayerCastle>();
        int priceToUpgrade = 200 * playerCastle.Level;
        if(GameplayConfiguration.CountGold >= priceToUpgrade && playerCastle.Level < 3)
        {
            playerCastle.Level ++;
            GameplayConfiguration.CountGold -= priceToUpgrade;
        }
    }
}
public class GameOver : MonoBehaviour
{
    public void LoadFinalScene()
    {
        Destroy(gameObject);
        Menu.resultLastGame = "You Lose!";
        SceneManager.LoadScene("Menu");
    
    }
}
