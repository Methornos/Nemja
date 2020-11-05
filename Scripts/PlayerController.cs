using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler
{
    [Header("Задаваемые переменные")]
    [SerializeField]
    private float _force;
    [SerializeField]
    private float _jumpForce;

    [Header("Закинуть")]
    [SerializeField]
    private Rigidbody2D _playerRigidbody;
    [SerializeField]
    private Transform _player;
    [SerializeField]
    private KillerSpawner _killerSpawner;
    [SerializeField]
    private BonusSpawner _bonusSpawner;
    [SerializeField]
    private GameObject _startPlatform;
    [SerializeField]
    private ScoreSystem _scoreSystem;
    [SerializeField]
    private GameObject _jumpParticle;
    [SerializeField]
    private Transform _jumpPanel;
    [SerializeField]
    private GameObject _jumpIndicator;
    [SerializeField]
    private Sounds _sounds;

    [Space]
    public bool IsReadyRight = true;
    public bool IsReadyLeft = true;
    public bool IsAlive = true;

    [Space(10)]
    [Header("Фазы")]
    [Space]
    public SpriteRenderer _playerSprite;

    [Space]
    public Sprite _playerStay;

    public GameObject _eyeStay;

    [Space] [SerializeField]
    private Sprite _playerFlyLeft;

    public GameObject _eyeFlyLeft;

    [Space] [SerializeField]
    private Sprite _playerFlyRight;

    public GameObject _eyeFlyRight;

    [Space] [SerializeField]
    private Sprite _playerJump;

    public GameObject _eyeJump;

    public bool IsFly = false;

    private Vector2 _firstPress;
    private Vector2 _secondPress;
    private Vector2 _currentSwipe;

    /*doubleclick*/
    private float _clicked = 0;
    private float _clicktime = 0;
    private float _clickdelay = 0.4f;

    public Stack<int> Jumps = new Stack<int>();

    private int _jumpsCount = 3;

    private void Start()
    {
        for (int i = 0; i < _jumpsCount; i++)
        {
            Jumps.Push(Jumps.Count + 1);
        }
    }

    public void OnPointerDown(PointerEventData data)
    {
        
        _clicked++;
        if (_clicked == 1) _clicktime = Time.time;

        if (_clicked > 1 && Time.time - _clicktime < _clickdelay)
        {
            _clicked = 0;
            _clicktime = 0;
            Jump();
        }
        else if (_clicked > 2 || Time.time - _clicktime > 1) _clicked = 0;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _firstPress = new Vector2(eventData.position.x, eventData.position.y);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (_startPlatform.activeSelf)
            _startPlatform.SetActive(false);

        _secondPress = new Vector2(eventData.position.x, eventData.position.y);
        _currentSwipe = new Vector2(Mathf.Abs(_secondPress.x) - Mathf.Abs(_firstPress.x), Mathf.Abs(_secondPress.y) - Mathf.Abs(_firstPress.y));
        _currentSwipe.Normalize();

        if (IsAlive)
        {
            if (_currentSwipe.x != 0)
            {
                if (_currentSwipe.x < 0 && IsReadyLeft == true)
                {
                    SwipeLeft();
                }
                else if (_currentSwipe.x > 0 && IsReadyRight == true)
                {
                    SwipeRight();
                    
                }

                IsReadyRight = false;
                IsReadyLeft = false;
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
    }

    private void SwipeLeft()
    {
        ChangeSprite("ToLeft");

        _killerSpawner.Generate();

        if (Random.Range(1, 101) <= 10)
            _bonusSpawner.SpawnBonus();

        _scoreSystem.Score();

        _playerRigidbody.AddForce(-_player.right * _force);
        _playerRigidbody.AddForce(_player.up * _jumpForce);

        _sounds.PlayFlySound();
    }

    private void SwipeRight()
    {
        ChangeSprite("ToRight");

        _killerSpawner.Generate();

        if (Random.Range(1, 101) <= 10)
            _bonusSpawner.SpawnBonus();

        _scoreSystem.Score();

        _playerRigidbody.AddForce(_player.right * _force);
        _playerRigidbody.AddForce(_player.up * _jumpForce);

        _sounds.PlayFlySound();
    }

    public void Jump()
    {
        if (Jumps.Count > 0 && IsAlive)
        {
            IsFly = false;

            ChangeSprite("Jump");

            _playerRigidbody.AddForce(_player.up * _jumpForce);
            _playerRigidbody.velocity = new Vector2(0, 0);

            IsReadyRight = true;
            IsReadyLeft = true;

            Jumps.Pop();
            Destroy(_jumpPanel.GetChild(0).gameObject);
            GameObject jumpParticle = Instantiate(_jumpParticle, new Vector3(_player.transform.position.x, _player.transform.position.y - 1, 0), Quaternion.identity);
            Destroy(jumpParticle, 0.5f);

            StartCoroutine("JumpCD");
        }
    }

    private IEnumerator JumpCD()
    {
        yield return new WaitForSeconds(1f);
        if (IsFly == false) ChangeSprite("JumpOver");

        yield return new WaitForSeconds(3f);
        if (Jumps.Count <= _jumpsCount && Jumps.Count <= 5) AddJump();
    }

    private void ChangeSprite(string action)
    {
        if (IsAlive)
        {
            switch (action)
            {
                case "ToLeft":
                    IsFly = true;

                    _playerSprite.sprite = _playerFlyLeft;
                    _eyeFlyLeft.SetActive(true);
                    _eyeStay.SetActive(false);
                    _eyeJump.SetActive(false);
                    break;
                case "ToRight":
                    IsFly = true;

                    _playerSprite.sprite = _playerFlyRight;
                    _eyeFlyRight.SetActive(true);
                    _eyeStay.SetActive(false);
                    _eyeJump.SetActive(false);
                    break;
                case "Jump":
                    _playerSprite.sprite = _playerJump;
                    _eyeFlyRight.SetActive(false);
                    _eyeFlyLeft.SetActive(false);
                    _eyeStay.SetActive(false);
                    _eyeJump.SetActive(true);
                    break;
                case "JumpOver":
                    _playerSprite.sprite = _playerStay;
                    _eyeJump.SetActive(false);
                    _eyeStay.SetActive(true);
                    break;
            }
        }
    }

    public void AddJump()
    {
        Jumps.Push(Jumps.Count + 1);
        Instantiate(_jumpIndicator, _jumpPanel);
    }
}
