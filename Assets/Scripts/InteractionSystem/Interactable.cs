using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(SpriteRenderer))]
public abstract class Interactable : MonoBehaviour
{
    [SerializeField] private string m_Description;

    private SpriteRenderer _spriteRenderer;
    private Material _defaultMaterial;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _defaultMaterial = _spriteRenderer.material;
    }

	public void SetMaterial(Material material) 
    {
        if (material == null)
            return;

        _spriteRenderer.material = material;
    }    
	
	public void ResetMaterial()
    {
        _spriteRenderer.material = _defaultMaterial;
    }

    public abstract void Interact();

    public string GetDescription()
    {
        return m_Description;
    }
}
