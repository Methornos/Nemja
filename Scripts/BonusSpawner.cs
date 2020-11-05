using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusSpawner : MonoBehaviour
{
    [SerializeField] private Transform _player;

    public List<GameObject> Bonuses;

    public void SpawnBonus()
    { 
        GameObject bonus = Instantiate(RandomBonus(), new Vector3(0, _player.position.y + Random.Range(30, 50), 0), Quaternion.identity);
    }

    private GameObject RandomBonus()
    {
        GameObject bonus = Bonuses[Random.Range(0, Bonuses.Count)];

        return bonus;
    }
}
