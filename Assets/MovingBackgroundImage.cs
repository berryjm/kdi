using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBackgroundImage : MonoBehaviour
{
    public float backgroundSpeed;
    public Renderer backgroundRenderer;

    // Update is called once per frame
    void Update()
    {
        backgroundRenderer.material.mainTextureOffset += new Vector2(backgroundSpeed * Time.deltaTime, 0f);
    }
}
