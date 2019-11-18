using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ChickenTypeEnum{white,red,black }

[RequireComponent(typeof(MovementController))]
public class Enemy : MonoBehaviour
{
	public Boss _boss;
	public float _speed;
	public bool _facingRight;
	public bool _isTrap;
	public float _stopTimeOnFlip;
	[HideInInspector]
	public bool _dangerous;
	MovementController _movementController;
	SpriteRenderer _spriteRenderer;
	Vector2 _velocity;
	Coroutine _flipCoroutine;
	Animator _anim;
	AnimationTIme _animationTime;
	// Start is called before the first frame update
	void Start()
	{
		_dangerous = true;
		_movementController = GetComponent<MovementController>();
		_spriteRenderer = GetComponent<SpriteRenderer>();
		_anim = GetComponent<Animator>();
		_animationTime = GetComponent<AnimationTIme>();
		if (_anim != null)
		{
			_velocity.x = _speed;
			StartFacing();
		}
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
		if (_anim != null)
		{
			UpdateMove();
			UpdateFlip();
		}
	}
	void UpdateMove()
	{

		_movementController.Move(_velocity * Time.deltaTime);
	}
	void UpdateFlip()
	{
		if ((_velocity.x > 0 && _movementController._collision.right) || (_velocity.x < 0 && _movementController._collision.left))
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
		if (_flipCoroutine == null)
		{
			_flipCoroutine = StartCoroutine(FlipCoroutine());
		}
	}
	Coroutine _dieCoroutine;
	public void Die()
	{
		if (_dieCoroutine == null)
		{
			_dieCoroutine = StartCoroutine(Dying());
		}
	}
	IEnumerator Dying()
	{
		if (_boss!=null)
		{
			_boss.StartBossCameraCoroutine();
		}
		GetComponent<BoxCollider2D>().enabled=false;
		SoundManager.Instance.PlaySoundEffect("ChickenDie");
		_dangerous = false;
		_anim.Play("ChickenDie");
		_velocity.x = 0;
		yield return new WaitForSeconds(_animationTime.GetTime("ChickenDie"));

		Destroy(gameObject);
		_dieCoroutine = null;
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
