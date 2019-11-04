using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BoxCollider2D))]
public class MovementController : MonoBehaviour
{
	public int _horiztontalRayCount,_verticalRayCount;
	public LayerMask _layerObstacles;

	BoxCollider2D _boxCollider;
	Vector2 _bottomLeft, _bottomRight, _topLeft, _topRight;
	float _verticalRaySpacing;

	private void Start()
	{
		_boxCollider=GetComponent<BoxCollider2D>();
		_verticalRaySpacing = _boxCollider.bounds.size.y / (_verticalRayCount - 1);
	}
	private void Update()
	{
		
	}
	public void Move(Vector2 velocity)
	{
		CalculateBounds();
		HorizontalMove(ref velocity);
		transform.Translate(velocity);
	}
	public void HorizontalMove(ref Vector2 velocity)
	{
		for (int i = 0; i < _verticalRayCount; i++)
		{
			Vector2 origin = _bottomRight + new Vector2(0, _verticalRaySpacing * i);

			Debug.DrawLine(origin,origin + new Vector2(velocity.x,0));

			RaycastHit2D hit = Physics2D.Raycast(
			origin,
			Vector2.right,
			velocity.x,
			_layerObstacles
			);

			if (hit)
			{
				velocity.x = hit.distance;
				Debug.Log(hit.point);
			}
		}
	}

	void CalculateBounds()
	{
		_bottomLeft = new Vector2(_boxCollider.bounds.min.x, _boxCollider.bounds.min.y);
		_bottomRight = new Vector2(_boxCollider.bounds.max.x, _boxCollider.bounds.min.y);
		_topLeft = new Vector2(_boxCollider.bounds.min.x, _boxCollider.bounds.max.y);
		_topRight = new Vector2(_boxCollider.bounds.max.x, _boxCollider.bounds.max.y);
	}
}
