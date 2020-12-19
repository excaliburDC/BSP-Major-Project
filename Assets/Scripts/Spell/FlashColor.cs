using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashColor : MonoBehaviour
{
	[SerializeField]
	private Color flashColor = Color.white;

	[SerializeField]
	[Range(0.0f, 1.0f)]
	private float flashAmount = 1.0f;

	[SerializeField]
	private float duration = 0.5f;

	[SerializeField]
	private SpriteRenderer spriteRenderer;

	private Material material;

	
	void Start()
	{
		if (spriteRenderer == null)
		{
			spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
		}
		material = spriteRenderer.material;
		material.color = flashColor;
	}

	
	/// <summary>
	/// Flashes the color.
	/// </summary>
	[ContextMenu("Test Flash")]
	public void Flash()
	{

		ChangeColor(flashAmount);
		Invoke("ResetColor", duration);
	}

	

	/// <summary>
	/// Resets the color.
	/// </summary>
	private void ResetColor()
	{
		ChangeColor(0.0f);
	}

	/// <summary>
	/// Flashes the color.
	/// </summary>
	private void ChangeColor(float value)
	{
		material.SetFloat("_FlashAmount", value);
	}

}
