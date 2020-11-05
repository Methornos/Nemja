using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerChecker : MonoBehaviour
{
    [SerializeField] private PlayerController _pc;
    [SerializeField] private GameObject _deathParticle;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "RightWall")
        {
            _pc.IsReadyLeft = true;
            _pc.IsReadyRight = false;

            _pc._playerSprite.sprite = _pc._playerStay;
            _pc._eyeFlyRight.SetActive(false);
            _pc._eyeStay.SetActive(true);

            _pc.IsFly = false;
        }

        if (collision.transform.tag == "LeftWall")
        {
            _pc.IsReadyLeft = false;
            _pc.IsReadyRight = true;

            _pc._playerSprite.sprite = _pc._playerStay;
            _pc._eyeFlyLeft.SetActive(false);
            _pc._eyeStay.SetActive(true);

            _pc.IsFly = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.transform.tag)
        {
            case "Bambuza":
                GameOver();
                break;
            case "Killer":
                GameOver();
                break;
            case "Bonus":
                collision.gameObject.GetComponent<Bonus>().UseBonus();
                break;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.tag == "RightWall")
        {
            _pc.IsReadyLeft = true;
            _pc.IsReadyRight = false;
        }

        if (collision.transform.tag == "LeftWall")
        {
            _pc.IsReadyLeft = false;
            _pc.IsReadyRight = true;
        }
    }

    private void GameOver()
    {
        _pc.IsAlive = false;
        GameObject particle = Instantiate(_deathParticle, transform.position, Quaternion.identity);
        Destroy(gameObject);
        Destroy(particle, 2f);
    }
}
