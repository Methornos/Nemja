using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallPlacer : MonoBehaviour
{
    [SerializeField] private PlayerController _pc;

    [HideInInspector] public float LeftWallX;
    [HideInInspector] public float RightWallX;

    public List<GameObject> Walls = new List<GameObject>();

    private Transform _player;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        //Walls[0].transform.position = new Vector2(LeftWallX, _player.position.y);
        //Walls[1].transform.position = new Vector2(RightWallX, _player.position.y);
    }

    private void Update()
    {
        if(_pc.IsAlive)
        {
            Walls[0].transform.position = new Vector2(Walls[0].transform.position.x, _player.position.y);
            Walls[1].transform.position = new Vector2(Walls[1].transform.position.x, _player.position.y);
        }
    }
}
