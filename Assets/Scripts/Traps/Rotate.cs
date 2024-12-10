using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] private float speed = 30f;

    private void Update()
    {
        transform.Rotate(speed * Time.deltaTime, 0, 0);
    }
}
