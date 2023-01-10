using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeLogo : MonoBehaviour
{

public float shakeAmount = 0.1f;
public float shakeInterval = 1.0f;

private float shakeTimer = 0.0f;

void Update()
{
// Increment the shake timer
shakeTimer += Time.deltaTime;

// If the shake timer has reached the shake interval, shake the image
if (shakeTimer >= shakeInterval)
{
// Reset the shake timer
shakeTimer = 0.0f;

// Shake the image by a random amount within the specified range
transform.position += new Vector3(Random.Range(-shakeAmount, shakeAmount), Random.Range(-shakeAmount, shakeAmount), 0.0f);
}
}

}
