using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Farm : MonoBehaviour, ITakeDamage
{
    public static GameObject FarmPanel;
    [HideInInspector] public int GoldPerStep;
    [HideInInspector] public int Level;
    [HideInInspector] public Dictionary<int, int> LevelFarm;
    [SerializeField] private Image _healthBar;
    [SerializeField] private GameObject _buttonUpgrade;
    private Text _farmInfo;
    private float _health;
    private int _armor = 20;

    public void TakeDamage(float damage)
    {
        _health -= damage / _armor;
        if(_health <= 0)
        {
            OverwriteEnemyTargets();
            RewardForDestruction();
        } 
    }

    private void InitializeInfoObjects()
    {
        FarmPanel = GameplayConfiguration.FarmPanel;
        _farmInfo = GameplayConfiguration.FarmInfo;
        _buttonUpgrade = GameObject.FindObjectOfType<UpgradePlayerBuildings>().ButtonUpgrade;
		FarmPanel.SetActive(false);
    }
    private void Start()
    {   
        InitializeInfoObjects();
        LevelFarm = new Dictionary<int,int>()
        {
            {1,10},
            {2,15},
            {3,20},
        };

        //Инициализация стартового значения и коэффициента получения золота
        Level = 1;
        _health = 1;
        GoldPerStep = LevelFarm[Level];

        StartCoroutine(StartMethodFarm());
    }
    private void Update() => _healthBar.fillAmount = _health;

    private IEnumerator StartMethodFarm() 
    {   
        while(true)
        {
            yield return new WaitForSeconds(7f);
            FarmGold();
        }  
    }
    private void FarmGold() => GameplayConfiguration.CountGold += GoldPerStep;

    private void OnMouseDown() 
    {        
        InfoAboutFarm();
        UpgradePlayerBuildings.currentfarm = transform.name;

        bool isActive = Level < 3 ? true : false;
        _buttonUpgrade.SetActive(isActive);

        FarmPanel.SetActive(true);
    }
    private void InfoAboutFarm()
    {
        _farmInfo.text = "Уровень:" + Level.ToString();
        _farmInfo.text += "\n" + "Добыча:" + GoldPerStep.ToString();
        if(Level < 3)
            _farmInfo.text += "\n" + "Upgrade:" + GoldPerStep * 10;
        _farmInfo.text += "\n" + "Sell:" + GoldPerStep * 5;
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
