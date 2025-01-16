using UnityEngine;
using TMPro;

public sealed class Interactor : MonoBehaviour
{
	[Header("General")]
    [SerializeField] private LayerMask m_Mask;
    [SerializeField] private float m_Radius;
    [SerializeField] private Transform m_Origin;
	[Header("Display")]
    [SerializeField] private TextMeshProUGUI m_Text;
	[SerializeField] private GameObject m_Button;
    [SerializeField] private Material m_SelectionMaterial;
	
	private Interactable _interactable;
	private bool _available;
	
	public void Interact()
	{
		if (!_available)
			return;
		
		_interactable.Interact();
	}
	
	private void FixedUpdate()
    {
        _available = FindInteractable(ref _interactable);
    }

    private bool FindInteractable(ref Interactable interactable)
	{
		Collider2D[] detectedColliders = Physics2D.OverlapCircleAll(m_Origin.position, m_Radius, m_Mask);
        
		if (detectedColliders.Length <= 0)
		{
			if (_available)
				ExitInteraction();
			return false;
		}
		
        if (detectedColliders[0].TryGetComponent(out Interactable futureInteractable))
        {
			m_Button.SetActive(true);
            _interactable?.ResetMaterial();
            _interactable = futureInteractable;
            _interactable.SetMaterial(m_SelectionMaterial);
            m_Text.text = _interactable.GetDescription();
            
			return true;
        }

        if (_interactable != null)
        {
			ExitInteraction();
        }

        return false;
	}
	
	private void ExitInteraction()
	{
		m_Button.SetActive(false);
        _interactable?.ResetMaterial();
        m_Text.text = string.Empty;
		_interactable = null;
	}
	
	private void OnDrawGizmosSelected()
    {
        if (m_Origin == null)
        {
            Debug.LogError("No origin specified, assign one in the inspector.");
            return;
        }

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(m_Origin.position, m_Radius);
    }
}
