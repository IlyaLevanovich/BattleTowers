using UnityEngine;

public class SelectPositionBuild : MonoBehaviour
{
	[SerializeField] private GameObject _shopPanel;

    private void OnMouseDown()
    {
    	NewBuild.CreatePosition = new Vector3(transform.position.x, 1.5f, transform.position.z);

        _shopPanel.SetActive(true);
    }
    private void OnMouseUp()
    {
    	return;
    }
}
