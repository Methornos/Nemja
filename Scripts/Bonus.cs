using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    public string BonusType;

    private PlayerController _pc;

    private void Start()
    {
        _pc = GameObject.FindGameObjectWithTag("PlayerController").GetComponent<PlayerController>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.tag == "Bonus")
            transform.position = new Vector2(transform.position.x, transform.position.y + 5);
    }

    public void UseBonus()
    {
        switch (BonusType)
        {
            case "ReloadJump":
                _pc.AddJump();
                Destroy(gameObject);
                break;
        }
    }
}
