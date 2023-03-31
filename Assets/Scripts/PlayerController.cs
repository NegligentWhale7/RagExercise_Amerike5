using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float speed;
    [SerializeField] float turnSoothTime = 0.1f;

    float _turnSmoothVelocity;
    private float _hInput, _vInput;
    private Vector3 _direction;

    [Header("Punch")]
    [SerializeField] Transform enemyTransform, rightRestPosition, leftRestPosition;
    [SerializeField] Fabrik rightArm, leftArm;
    [SerializeField] float punchSpeed, punchAccuracy;
    [SerializeField] float punchTime;

    private float _rightDistanceFromPlayer, _rightDisFromRest;
    private float _leftDistanceFromPlayer, _leftDisFromRest;
    bool rightArrive = false, leftArrive = false, righArmCor = false, leftArmCor = false;


    private void Start()
    {
        rightArm.target.position = rightRestPosition.position;
        leftArm.target.position = leftRestPosition.position;
    }


    private void Update()
    {

        GetDistanceRight(rightArm);
        GetRestDistanceRight(rightArm);

        GetDistanceLeft(leftArm);
        GetRestDistanceLeft(leftArm);

        
    }
    private void FixedUpdate()
    {
        ThrowPunch();
    }
    private void ThrowPunch()
    {
        if (Input.GetMouseButton(1))
        {
            righArmCor = true;
            StartCoroutine(PunchRight(punchTime));
        }

        if (Input.GetMouseButton(0))
        {
            leftArmCor = true;
            StartCoroutine(PunchLeft(punchTime));
        }
    }

    private IEnumerator PunchRight(float time)
    {
        while (righArmCor)
        {
            if (_rightDistanceFromPlayer > punchAccuracy && !rightArrive)
            {
                rightArm.target.position = Vector3.Lerp(rightArm.target.position, enemyTransform.position, Time.deltaTime * punchSpeed);

            }
            if (_rightDistanceFromPlayer <= punchAccuracy) rightArrive = true;

            yield return new WaitForSeconds(time);

            if (_rightDisFromRest > punchAccuracy && rightArrive)
            {
                rightArm.target.position = Vector3.Lerp(rightArm.target.position, rightRestPosition.position, Time.deltaTime * punchSpeed);
            }
            if (_rightDisFromRest <= punchAccuracy && rightArrive)
            {
                rightArrive = false;
                righArmCor = false;
            }
        }        
    }


    private IEnumerator PunchLeft(float time)
    {
        while (leftArmCor)
        {
            if (_leftDistanceFromPlayer > punchAccuracy && !leftArrive)
            {
                leftArm.target.position = Vector3.Lerp(leftArm.target.position, enemyTransform.position, Time.deltaTime * punchSpeed);

            }
            if (_leftDistanceFromPlayer <= punchAccuracy) leftArrive = true;

            yield return new WaitForSeconds(time);

            if (_leftDisFromRest > punchAccuracy && leftArrive)
            {
                leftArm.target.position = Vector3.Lerp(leftArm.target.position, leftRestPosition.position, Time.deltaTime * punchSpeed);
            }
            if (_leftDisFromRest <= punchAccuracy && leftArrive)
            {
                leftArrive = false;
                leftArmCor = false;
            }
        }
    }


    private void GetDistanceRight(Fabrik arm)
    {
        _rightDistanceFromPlayer = Vector3.Distance(arm.target.position, enemyTransform.position);
    }


    private void GetDistanceLeft(Fabrik arm)
    {
        _leftDistanceFromPlayer = Vector3.Distance(arm.target.position, enemyTransform.position);
    }


    private void GetRestDistanceRight(Fabrik arm)
    {
        _rightDisFromRest = Vector3.Distance(arm.target.position, rightRestPosition.position);
    }


    private void GetRestDistanceLeft(Fabrik arm)
    {
        _leftDisFromRest = Vector3.Distance(arm.target.position, leftRestPosition.position);
    }
}
