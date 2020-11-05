using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeAnimation : MonoBehaviour
{
    private bool _isOpen = true;

    void Update()
    {
        if(_isOpen)
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(1, 0), Time.deltaTime * 5);
    }
}
