using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MouseInteraction : MonoBehaviour
{
    private Camera mainCamera;
    private Renderer renderer;
    private Ray ray;
    private RaycastHit hit;
    private LayerMask layerMask = 6;
    public TextMeshProUGUI speed;
    public TextMeshProUGUI distance;
    public TextMeshProUGUI lifetime;

    private void Start()
    {
        mainCamera = Camera.main;
        renderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ray = new Ray(mainCamera.ScreenToWorldPoint(Input.mousePosition), mainCamera.transform.forward);

            if (Physics.Raycast(ray, out hit, 1000f, layerMask))
            {
                if (hit.transform == transform)
                {
                    speed.text = "Speed: " + transform.GetComponent<AICarMove>().speed;
                    distance.text = "Distance: " + transform.GetComponent<AICarMove>().distance;
                    lifetime.text = "Lifetime: " + AICarMove.lifetime;
                }
            }
        }
    }
}
