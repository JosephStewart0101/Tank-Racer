using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BoostVariables : ScriptableObject
{
    public float boostTime = 5f;
    public float boostCurrent = 5f;

    public float cooldown = 6f;

    public bool isBoosting = false;
    public bool isCooldown = false;

    public float speed = 0f;
    public float speedCurrent = 0f;
    public float boostSpeed = 0f;
}
