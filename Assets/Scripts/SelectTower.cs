using UnityEngine;

public class SelectTower : MonoBehaviour
{
	private Renderer _isRenderer;
	private int _id;
	private void Start()
	{
		_isRenderer = GetComponent<Renderer>();
		_id = Random.Range(0, 1000);
	}


}
