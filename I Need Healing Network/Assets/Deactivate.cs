using UnityEngine;

public class Deactivate : MonoBehaviour {

	public void Disable()
    {
        transform.GetComponent<UnityEngine.UI.Button>().interactable = false;
    }
}
