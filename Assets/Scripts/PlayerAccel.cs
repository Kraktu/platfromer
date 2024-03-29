﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovementController))]
public class PlayerAccel : MonoBehaviour
{
	AnimationTIme _animationTIme;
	Animator anim;
	float acceleration=1;
	public float maxFallingSpeed;
	[Tooltip("Number of meter by second")]
	public float maxSpeed;
	[Tooltip("Time in seconds to reach max speed")]
	public float timeTomaxSpeed;
	float minSpeedThreshold;
	public float _hitBounceBack;
	int generatedNumber=5;

	[Tooltip("Unity value of max jump height")]
	public float jumpHeight;
	[Tooltip("Time in seconds to reach the jump height")]
	public float timeToMaxJump;
	[Tooltip("Can i change direction in air?")]
	[Range(0, 1)]
	public float airControl;
	public int _maxAirJump;
	int _jumpCount;
	public string[] _footstepsreverb;

	bool _turnedRight = true, _isJumping = false, _wallJumpedLeft = false, _wallJumpedRight = false, _wallJumped=false, _hitted;
	public bool _freeze { get; private set; }
	float gravity;
	float jumpForce;
	int horizontal = 0;
	Coroutine _gettingHit;

	Vector2 velocity = new Vector2();
	MovementController movementController;

	public GameObject _basicProjectiles, _bouncingProjectiles, _unblockedProjectiles;
	[HideInInspector]
	public GameObject _choosedProjectile;
	public float _shootingDelay = 1, _projectileAccel = 300;
	float _lastShootTime=0;

	// Start is called before the first frame update
	void Start()
	{
		transform.localScale = new Vector3(1, 1, 1);
		acceleration *= (2f*maxSpeed)/ Mathf.Pow(timeTomaxSpeed,2);
		minSpeedThreshold = acceleration / Application.targetFrameRate * 2f;
		movementController = GetComponent<MovementController>();
		_animationTIme = GetComponent<AnimationTIme>();
		// Math calculation for gravity and jumpForce
		gravity = -(2 * jumpHeight) / Mathf.Pow(timeToMaxJump, 2);
		jumpForce = Mathf.Abs(gravity) * timeToMaxJump;
		anim = GetComponent<Animator>();
		_choosedProjectile = _basicProjectiles;
		StartCoroutine(Shooting());
	}

	// Update is called once per frame
	void Update()
	{
		if (transform.position.y<=-12)
		{
			Die();
		}
		if (movementController._collision.bottom || movementController._collision.top)
			velocity.y = 0;

		horizontal = 0;

		if (Input.GetKey(KeyCode.D) && !_freeze)
		{
			horizontal += 1;
		}
		if (Input.GetKey(KeyCode.Q) && !_freeze)
		{
			horizontal -= 1;
		}
		if (Input.GetKey(KeyCode.S)&&Input.GetKey(KeyCode.Space)&& (movementController._onOneWayPlatform==true))
		{
			transform.Translate(Vector2.down*0.5f);
		}
		if (velocity.x!= 0 && movementController._collision.bottom && _freeze == false)
		{
			anim.Play("FrogRun");
		}
		if (velocity.x==0&& movementController._collision.bottom && _freeze == false)
		{
			anim.Play("FrogIdle");
		}
		UpdateJump();

		float controlModifier = 1f;
		if (!movementController._collision.bottom)
		{
			controlModifier = airControl;
		}

		velocity.x += horizontal * acceleration * controlModifier * Time.deltaTime;

		if (Mathf.Abs(velocity.x) > maxSpeed)
			velocity.x = maxSpeed*horizontal;

		if (horizontal == 0)
		{
			if (velocity.x > minSpeedThreshold)
				velocity.x -= acceleration * Time.deltaTime;
			else if (velocity.x < -minSpeedThreshold)
				velocity.x += acceleration * Time.deltaTime;
			else
				velocity.x = 0;
		}

		velocity.y += gravity * Time.deltaTime;
		if (velocity.y<maxFallingSpeed)
		{
			velocity.y = maxFallingSpeed;
		}
		movementController.Move(velocity * Time.deltaTime);
		if(velocity.x<0&&_turnedRight==true)
		{
			transform.localScale=new Vector3(-1, 1, 1);
			_turnedRight = false;
		}
		if (velocity.x>0&&_turnedRight==false)
		{
			transform.localScale = new Vector3(1, 1, 1);
			_turnedRight = true;
		}
	}

	void UpdateJump()
	{
		if (movementController._collision.bottom)
		{
			_jumpCount = 0;
			_isJumping = false;
			_wallJumpedLeft = false;
			_wallJumpedLeft = false;
			_wallJumpedRight = false;
		}
		if (!movementController._collision.bottom&&_isJumping==false)
		{
			_jumpCount = 1;
		}

		if (Input.GetKeyDown(KeyCode.Space) && _jumpCount <= _maxAirJump && !_freeze)
		{
			JumpA();
			_jumpCount++;
			anim.Play("FrogJump");
			if (_jumpCount>1)
			{
				anim.Play("FrogDoubleJump");
			}

		}
		if (movementController._collision.left&&_wallJumpedLeft==false&&!movementController._collision.bottom)
		{
			_jumpCount--;
			_wallJumpedLeft=true;
			_wallJumpedRight = false;
			_wallJumped = true;
			anim.Play("FrogWallJump");
			
		}
		if (movementController._collision.right&&_wallJumpedRight==false&&!movementController._collision.bottom)
		{
			_jumpCount--;
			_wallJumpedRight = true;
			_wallJumpedLeft = false;
			_wallJumped = true;
			anim.Play("FrogWallJump");
			
		}
	}
	public void AnimationFootStepSound()
	{
		int previousGeneratedNumber;

		previousGeneratedNumber = generatedNumber;
		while (previousGeneratedNumber==generatedNumber)
		{
			generatedNumber = Random.Range(0, _footstepsreverb.Length);
		}
		Debug.Log(generatedNumber);
		SoundManager.Instance.PlaySoundEffect(_footstepsreverb[generatedNumber], Random.Range(0.5f, 1.5f), Random.Range(0.5f, 1.5f));
	}
	public void JumpA()
	{
		SoundManager.Instance.PlaySoundEffect("PlayerJump",Random.Range(0.8f,1.2f),-5);
		_isJumping = true;
		velocity.y = jumpForce;
		if (_wallJumped==true&& _wallJumpedRight == true)
		{
			velocity.x = -10;
		}
		if (_wallJumped == true&& _wallJumpedLeft == true)
		{
			velocity.x = 10;
		}
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		Enemy enemy = collision.gameObject.GetComponent<Enemy>();
		CameraScroll cameraScroll = collision.gameObject.GetComponent<CameraScroll>();

		if (enemy != null)
		{
			if (_gettingHit == null)
			{
				_gettingHit = StartCoroutine(Hitted(enemy));
			}
		}
	}

	private void Die()
	{
		SpawnPlayer spawnPlayer = FindObjectOfType<SpawnPlayer>();
		spawnPlayer.ReSpawn();
		Destroy(gameObject);
	}
	IEnumerator Hitted(Enemy enemy)
	{
		yield return new WaitForEndOfFrame();
		if (enemy._dangerous)
		{
			//SoundManager.Instance.lowerMusicPitch(_animationTIme.GetTime("FrogHit"));
			SoundManager.Instance.PlaySoundEffect("PlayerDie");
			anim.Play("FrogHit");
			_freeze = true;
			StartCoroutine(HittedMovement());
			yield return new WaitForSeconds(_animationTIme.GetTime("FrogHit"));
			Die();
		}
		_gettingHit = null;
	}
	IEnumerator HittedMovement()
	{
		while (true)
		{
			if (transform.localScale==new Vector3 (1,1,1))
			{
				movementController.Move(Vector2.left*Time.deltaTime*_hitBounceBack);
			}
			if (transform.localScale==new Vector3(-1,1,1))
			{
				movementController.Move(Vector2.right * Time.deltaTime * _hitBounceBack);
			}
		yield return null;
		}
	}
	public void Freeze()
	{
		_freeze = true;
		anim.Play("FrogIdle");
	}
	public void UnFreeze()
	{
		_freeze = false;
	}

	public IEnumerator Shooting()
	{
		float _shootingAngle=0;

		while (true)
		{
			if (_lastShootTime + _shootingDelay <= Time.time)
			{
				Quaternion rotation = Quaternion.Euler(0, 0, _shootingAngle);
				Instantiate(_choosedProjectile, transform.position,rotation);
				_shootingAngle += 45;
				if (_shootingAngle==360)
				{
					_shootingAngle = 0;
				}

				_lastShootTime = Time.time;
			}
			yield return null;
		}
	}

}
