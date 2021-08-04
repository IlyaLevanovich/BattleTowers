using UnityEngine;

public class NewBuild: MonoBehaviour
{
	public static Vector3 CreatePosition;
	[SerializeField] private GameObject _farm;
	[SerializeField] private GameObject _tower;
	[SerializeField] private GameObject _shopPanel;
	[SerializeField]private int _farmPrice;
	[SerializeField]private int _towerPrice;
	private Vector3 _correctRotation = new Vector3(0, 90, 0);

	private void InstantiateBuilding(GameObject buildingName, Vector3 position)
	{
		Quaternion newRotation = Quaternion.Euler(_correctRotation);
		Instantiate(buildingName, position, newRotation);
	}

	public void OnCreateFarm()
	{
		
		CorrectPosition();
		if(GameplayConfiguration.CountGold >= _farmPrice)
		{
			InstantiateBuilding(_farm, CreatePosition);
			GameplayConfiguration.CountGold -= _farmPrice;
			CloseShop();
			GameplayConfiguration.OverwriteEnemyList();
		}
	}
	public void OnCreateTower()
	{
		CorrectPosition();
		if(GameplayConfiguration.CountGold >= _towerPrice)
		{
			InstantiateBuilding(_tower, CreatePosition);
			GameplayConfiguration.CountGold -= _towerPrice;
			CloseShop();
			GameplayConfiguration.OverwriteEnemyList();
		}
	}
	public void CloseShop() => _shopPanel.SetActive(false);
	
	private void CorrectPosition() => CreatePosition.y = 1f;
}
