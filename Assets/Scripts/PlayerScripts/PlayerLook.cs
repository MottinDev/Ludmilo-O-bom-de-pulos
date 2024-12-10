using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Camera cam;
    public Transform gun; 

    private float xRotation = 0f;

    public float xSensitivity = 15f;
    public float ySensitivity = 15f;

    private bool isAlive = true;
    
    public void ProcessLook(Vector2 input)
    {
        if (!isAlive) return;
        float mouseX = input.x;
        float mouseY = input.y;

        xRotation -= (mouseY * Time.deltaTime) * ySensitivity;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        cam.transform.localRotation = Quaternion.Euler(xRotation,0 ,0);
         
        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSensitivity);
    }
   
    public void SetAlive(bool alive)
    {
        isAlive = alive;
        
        if (!isAlive)
        {
            GetComponent<PlayerLook>().enabled = false;
        }
    }
}
