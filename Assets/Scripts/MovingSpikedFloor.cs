using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FloorMovement { RightToLeft,LeftToRight,TopToBottom,BottomToTop}
public class MovingSpikedFloor : MonoBehaviour
{
	public GameObject[] _movingSpikes;
	public GameObject _wallToCreate,_resetTrapSwitch,_TrapSwitchToReset;
	public int _heightInBlocs;
	public float _timeBetweenBlockCreation;
	public FloorMovement _floorMovement;
	public bool _isResetSwitch;
	[HideInInspector]
	public List<GameObject> _createdTiles;
	Vector2 _floorMovementVector;
	bool _isTimerEnded;
	Vector3[] _initalPosition;
	private void Start()
	{
		switch (_floorMovement)
		{
			case FloorMovement.RightToLeft:
				_floorMovementVector = Vector2.left;
				break;
			case FloorMovement.LeftToRight:
				_floorMovementVector = Vector2.right;
				break;
			case FloorMovement.TopToBottom:
				_floorMovementVector = Vector2.down;
				break;
			case FloorMovement.BottomToTop:
				_floorMovementVector = Vector2.up;
				break;
			default:
				break;
		}
		_initalPosition = new Vector3[_movingSpikes.Length];
		for (int i = 0; i < _movingSpikes.Length; i++)
		{
			_initalPosition[i] = _movingSpikes[i].transform.position;
		}
	}
	Coroutine _spikeMoveCoroutine;
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (!_isResetSwitch&& collision.gameObject.tag=="Player")
		{
			_createdTiles = new List<GameObject>();
			_resetTrapSwitch.GetComponent<BoxCollider2D>().enabled=true;

			_spikeMoveCoroutine=StartCoroutine(SpikeMoveCoroutine());
		}
		if (_isResetSwitch && collision.gameObject.tag == "Player")
		{
			_TrapSwitchToReset.GetComponent<MovingSpikedFloor>().Reset();
		}
	}
	public void Reset()
	{
		StopCoroutine(SpikeMoveCoroutine());
		GetComponent<BoxCollider2D>().enabled = true;
		GetComponent<ButtonActions>()._hasBeenTriggered = false;
		for (int i = 0; i < _movingSpikes.Length; i++)
		{
			_movingSpikes[i].transform.position = _initalPosition[i];
		}
		foreach (GameObject go in _createdTiles)
		{
			Destroy(go);
		}
	}
	void SpikeMove()
	{
		for (int j = 0; j < _movingSpikes.Length; j++)
		{
			_createdTiles.Add(Instantiate(_wallToCreate, _movingSpikes[j].transform.position, Quaternion.identity));
			_movingSpikes[j].transform.position+=(Vector3)_floorMovementVector;
		}
	}

	public IEnumerator SpikeMoveCoroutine()
	{
		int iteration = 0;
		while (iteration<_heightInBlocs)
		{
			SpikeMove();
			iteration++;
			yield return new WaitForSeconds(_timeBetweenBlockCreation);
		}
	}

}
