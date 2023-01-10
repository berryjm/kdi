using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
public float scrollSpeed = 0.5f;
public SpriteRenderer renderer;

void Update()
{
transform.position += new Vector3(-scrollSpeed, 0, 0) * Time.deltaTime;

 Debug.Log("monito" + transform.position.x.ToString() );
if (transform.position.x < -renderer.bounds.size.x)
{
    //Debug.Log("THIS IS IT: " + transform.position.x.ToString() + "Yep:" + renderer.bounds.size.x.ToString());
transform.position += new Vector3(renderer.bounds.size.x * 2, 0, 0);
}
}
}