using UnityEngine;

public class UpgradePlayerBuildings : MonoBehaviour
{
    public static string currentfarm;
	public GameObject ButtonUpgrade;

    //Методы связанные с Farm
    public void UpgradeFarm()
	{
		GameObject isFarm = GameObject.Find(currentfarm);
		var component = isFarm.GetComponent<Farm>();

		if(GameplayConfiguration.CountGold >= component.GoldPerStep * 10 && component.Level < 3)
		{
			component.Level ++;
			GameplayConfiguration.CountGold-= component.GoldPerStep * 10; //Не забыть сделать коэффициент продажи/покупки фермы
			component.GoldPerStep = component.LevelFarm[component.Level];
			CloseFarmPanel();
		}
	}

	public void SellFarm()
	{
		GameObject isFarm = GameObject.Find(currentfarm);

		GameplayConfiguration.CountGold += isFarm.GetComponent<Farm>().GoldPerStep * 5;
		Destroy(isFarm);
		CloseFarmPanel();
	}

    public void CloseFarmPanel() => Farm.FarmPanel.SetActive(false);

    //Методы связанные со StrikeTower
}
