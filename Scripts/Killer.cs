using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killer : MonoBehaviour
{
    [SerializeField] private GameObject _destroyParticle;

    private Rigidbody2D _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

        _rb.velocity = new Vector2(0, -7);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.tag == "Bambuza")
            DestroyKiller();
        if (collision.transform.tag == "Killer")
            transform.position = new Vector2(transform.position.x, transform.position.y + 5);
    }

    private void DestroyKiller()
    {
        GameObject particle = Instantiate(_destroyParticle, transform.position, Quaternion.identity);
        Destroy(gameObject);
        Destroy(particle, 2f);
    }
}
