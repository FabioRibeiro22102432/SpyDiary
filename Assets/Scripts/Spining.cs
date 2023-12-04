using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spining : MonoBehaviour
{
    [SerializeField] private float fanSpeed;

    // Update is called once per frame
    private void Update()
    {
        transform.Rotate(0f, 0f, fanSpeed * Time.deltaTime, Space.Self);
    }
}
