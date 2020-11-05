using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillerSpawner : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private PlayerController _pc;

    public List<GameObject> LeftKillers;
    public List<GameObject> RightKillers;

    [HideInInspector] public float LeftKillerX;
    [HideInInspector] public float RightKillerX;

    private void Start()
    {
        StartCoroutine("RandomGeneration");
    }

    public void Generate()
    {
        switch(Random.Range(0, 2))
        {
            case 0:
                GenerateLeft();
                break;
            case 1:
                GenerateRight();
                break;
            default:
                Generate();
                break;
        }
    }

    private void GenerateLeft()
    {
        if(_pc.IsAlive)
        {
            GameObject leftKiller = Instantiate(RandomLeftKiller(), new Vector3(-4.3f, _player.position.y + Random.Range(20, 51), 0), Quaternion.identity);
        }
    }

    private void GenerateRight()
    {
        if (_pc.IsAlive)
        {
            GameObject rightKiller = Instantiate(RandomRightKiller(), new Vector3(4.3f, _player.position.y + Random.Range(40, 71), 0), Quaternion.identity);
        }
    }

    private GameObject RandomLeftKiller()
    {
        GameObject killer = LeftKillers[Random.Range(0, LeftKillers.Count)];

        return killer;
    }

    private GameObject RandomRightKiller()
    {
        GameObject killer = RightKillers[Random.Range(0, RightKillers.Count)];

        return killer;
    }

    private IEnumerator RandomGeneration()
    {
        yield return new WaitForSeconds((int)Random.Range(10f, 20f));
        GenerateLeft();
        GenerateRight();
        StartCoroutine("RandomGeneration");
    }
}
