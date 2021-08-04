using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayConfiguration : MonoBehaviour
{
	public static int CountSoldiers;
	public static int CountGold;
	public static List<GameObject> TargetBuild = new List<GameObject>();
	public static List<GameObject> EnemyTargetBuild = new List<GameObject>();
	public static bool IsBattle = false;

	public static GameObject FarmPanel;
	public static Text FarmInfo;

	[SerializeField]private Text _battleButtonInfo;
	[SerializeField] private Text _goldInfo;

	[SerializeField]private GameObject _soldier;
	[SerializeField] private GameObject _playerCastle;
	[SerializeField]private List<Transform> _soldierSpawnPosition;

	[SerializeField] private Image _buttonState;
	[SerializeField]private Text _countOfSoldiers;

	private void Awake()
	{
		FarmPanel = GameObject.Find("PanelFarm");
		FarmInfo = GameObject.Find("FarmInfo").GetComponent<Text>();

		OverwriteMyList();
		OverwriteEnemyList();
	}
	private void Start()
	{	
		CountSoldiers = 1;
		CountGold = 50;
	}
	
	public static void OverwriteMyList()
	{
		TargetBuild.Clear();
		SelectTower[] array = GameObject.FindObjectsOfType<SelectTower>();
		for(int i = 0; i < array.Length; i++)
			TargetBuild.Add(array[i].gameObject);
	}
	public static void OverwriteEnemyList()
	{
		EnemyTargetBuild.Clear();
		MyBuildings[] array = GameObject.FindObjectsOfType<MyBuildings>();
		for(int i = 0; i < array.Length; i++)
			EnemyTargetBuild.Add(array[i].gameObject);
	}

	private void Update()
	{
		_countOfSoldiers.text = CountSoldiers.ToString();
		_goldInfo.text = "Gold:" + CountGold.ToString();
	}

	public void Battle()
	{
		if(!IsBattle)
		{
			_battleButtonInfo.text = "Back";
			StartCoroutine(OnCreateSoldier());
			IsBattle = true;
		}
		else
		{
			_battleButtonInfo.text = "Battle";
			IsBattle = false;
		}
	}

	private IEnumerator OnCreateSoldier()
	{
		while(CountSoldiers > 0)
		{
			Instantiate(_soldier, _soldierSpawnPosition[Random.Range(0, 3)].position, Quaternion.identity);
			CountSoldiers--;
			_countOfSoldiers.text = CountSoldiers.ToString();
			yield return new WaitForSeconds(0.1f);
		}
	}

	public void OnBuySoldiers()
	{
		if(CountGold >= 50)
		{
			CountSoldiers++;
			CountGold -= 50;
			if(IsBattle)
				StartCoroutine(OnCreateSoldier());
		}
	}

	public void SetGameState()
	{
		var timeState = Time.timeScale;

		if(timeState == 1)
		{
			Time.timeScale = 0;
			_buttonState.sprite = Resources.Load<Sprite>("Sprites/Play");
		}
		else
		{
			Time.timeScale = 1;
			_buttonState.sprite = Resources.Load<Sprite>("Sprites/Pause");
		}
	}
}



