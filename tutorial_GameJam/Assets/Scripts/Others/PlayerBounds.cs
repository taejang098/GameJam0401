using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerBounds : MonoBehaviour
{
    private void Update()
    {
        Vector3 worldpos = Camera.main.WorldToViewportPoint(this.transform.position);
        if (worldpos.x < 0.01f) worldpos.x = 0.01f;
        if (worldpos.y < 0.03f) worldpos.y = 0.03f;
        if (worldpos.x > 0.99f) worldpos.x = 0.99f;
        if (worldpos.y > 0.99f) worldpos.y = 0.99f;

        Vector2 camPos = Camera.main.ViewportToWorldPoint(worldpos);
        this.transform.position = camPos;
    }
}
