using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightRotation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        RightYRotation();
    }

    void RightYRotation()
    {
        Quaternion rightYRotation = Quaternion.Euler(0, 180, 0);
        transform.rotation = rightYRotation;
    }

    void Update()
    {
        transform.Translate(10 * Time.deltaTime, 0, 0);
    }
}
