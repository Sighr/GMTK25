using UnityEngine;

public class MenuManager : MonoBehaviour
{
	[Header("Configuration")]
	[SerializeField] private MainHeroMovementController movementController;
	[SerializeField] private DailyManager dailyManager;
	[SerializeField] private GameObject menu;
	[SerializeField] private GameObject hud;
	
	public void OnStartGame()
	{
		dailyManager.enabled = true;
		hud.SetActive(true);
		menu.SetActive(false);
		movementController.enabled = true;
	}
}