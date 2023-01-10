using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollScript : MonoBehaviour
{
public float scrollSpeed = 0.5f;

void Update()
{
transform.position += new Vector3(-scrollSpeed, 0, 0) * Time.deltaTime;

if (transform.position.x < -GetComponent<SpriteRenderer>().bounds.size.x)
{
    Debug.Log(transform.position.x);
    transform.position += new Vector3(GetComponent<SpriteRenderer>().bounds.size.x, 0, 0);
}

}
}
