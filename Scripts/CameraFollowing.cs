using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowing : MonoBehaviour
{
    [SerializeField] private PlayerController _pc;
    [SerializeField] private float _offsetY;

    private Transform _player;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        if(_pc.IsAlive)
        {
            transform.position = Vector3.Lerp(new Vector3(0, transform.position.y, -10), new Vector3(0, _player.position.y + _offsetY, -10), Time.deltaTime);
        }
    }
}
