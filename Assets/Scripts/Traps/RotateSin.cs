using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSin : MonoBehaviour
{
    [SerializeField] private float speed = 30f; // Velocidade de rotação
    [SerializeField] private float amplitude = 15f; // Amplitude do movimento

    private void Update()
    {
        // Calcula a rotação usando a função Sin para criar um movimento pendular
        float angle = amplitude * Mathf.Sin(Time.time * speed);

        // Aplica a rotação ao objeto, mantendo os valores y e z e ajustando o x
        transform.rotation = Quaternion.Euler(angle, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
    }
}
