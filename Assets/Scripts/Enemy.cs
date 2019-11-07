using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovementController))]
public class Enemy : MonoBehaviour
{
	public float _speed;
	public bool _facingRight;
	public float _stopTimeOnFlip;
	MovementController _movementController;
	SpriteRenderer _spriteRenderer;
	Vector2 _velocity;
	Coroutine _flipCoroutine;
	Animator _anim;
    // Start is called before the first frame update
    void Start()
    {
		_movementController = GetComponent<MovementController>();
		_spriteRenderer = GetComponent<SpriteRenderer>();
		_anim = GetComponent<Animator>();
		_velocity.x = _speed;
		StartFacing();
    }
	void StartFacing()
	{
		if (_facingRight)
		{
			_velocity.x = _speed;
		}
		else if (!_facingRight)
		{
			_velocity.x = -_speed;
			_spriteRenderer.flipX = !_spriteRenderer.flipX;
		}
	}
    // Update is called once per frame
    void Update()
    {
		UpdateMove();
		UpdateFlip();

    }
	void UpdateMove()
	{
		
		_movementController.Move(_velocity * Time.deltaTime);
	}
	void UpdateFlip()
	{
		if ((_velocity.x>0&&_movementController._collision.right)||(_velocity.x<0&&_movementController._collision.left))
		{
			Flip();
		}
		else if (_movementController._collision.frontPit)
		{
			Flip();
		}
	}
	void Flip()
	{
		if (_flipCoroutine==null)
		{
			_flipCoroutine = StartCoroutine(FlipCoroutine());
		}
	}
	IEnumerator FlipCoroutine()
	{
		float actualVelocity=_velocity.x;
		_anim.Play("Idle");
		_velocity.x = 0;
		yield return new WaitForSeconds(_stopTimeOnFlip);
		_spriteRenderer.flipX = !_spriteRenderer.flipX;
		_velocity.x =actualVelocity* -1;
		_anim.Play("Run");
		_flipCoroutine = null;
	}
}
