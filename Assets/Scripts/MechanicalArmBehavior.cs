using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechanicalArmBehavior : MonoBehaviour
{
    public LineRenderer laser;

    public void ActivateLaser()
    {
        laser.enabled = true;
    }
}
