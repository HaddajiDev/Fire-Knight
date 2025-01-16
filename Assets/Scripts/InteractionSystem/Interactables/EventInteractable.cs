using UnityEngine;
using UnityEngine.Events;

public class EventInteractable : Interactable
{
	[SerializeField] private UnityEvent m_OnInteract;
	
	public override void Interact()
	{
		Debug.Log("Interacted");
		m_OnInteract?.Invoke();
	}
}
