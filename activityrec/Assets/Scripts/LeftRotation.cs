using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftRotation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LeftYRotation();
    }

    void LeftYRotation()
    {
        Quaternion leftYRotation = Quaternion.Euler(0, 0, 0);
        transform.rotation = leftYRotation;
    }

    void Update()
    {
        transform.Translate(10 * Time.deltaTime, 0, 0);
    }
}
