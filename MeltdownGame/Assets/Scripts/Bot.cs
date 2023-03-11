using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : CharacterController
{
    Rotator _rotator;
    [SerializeField] float _distanceThreshold;
    private int _previousAction;
    [SerializeField] Transform _middleObject;
    [SerializeField] float _timeToClearJump;
    [SerializeField] float _timeToClearDuck;
    public float ErrorDelta;
    private float _predictionError;
    protected override void Start()
    {
        base.Start();
        _rotator = FindObjectOfType<Rotator>();
        ErrorDelta = Random.Range(0.01f, 0.03f);
    }

    protected override void Update()
    {
        if (ActionInProgress || Defeated)
        {
            return;
        }
        int action = 0;
        for (int i = 0; i < _rotator.EdgeObjects.Count; i++)
        {
            Vector3 vector= (_rotator.EdgeObjects[i].position - _middleObject.position);
            float dotUp = Vector3.Dot(Vector3.up, vector);
            float obstacleVelocity = _rotator.GetVelocity();
            if (dotUp > 0)
            {
                float delta = obstacleVelocity * (_timeToClearDuck- _predictionError);
                Vector3 predictedObstaclePos = _rotator.transform.position +
                    Quaternion.Euler(0, delta, 0) * (_rotator.EdgeObjects[i].position - _rotator.transform.position);
                Vector3 predictedVector = (predictedObstaclePos - _middleObject.position);
                float dotCurrent = Vector3.Dot(vector, transform.right);
                if (predictedVector.magnitude < _distanceThreshold || (dotCurrent>0 && _previousAction==1) || vector.magnitude<_distanceThreshold)
                {
                    Duck();
                    action = 1;
                }
            }
            else
            {
                float delta = obstacleVelocity * (_timeToClearJump - _predictionError);
                Vector3 predictedObstaclePos = _rotator.transform.position +
                    Quaternion.Euler(0, delta, 0) * (_rotator.EdgeObjects[i].position - _rotator.transform.position);
                Vector3 predictedVector = (predictedObstaclePos - _middleObject.position);
                float dotSide = Vector3.Dot(transform.right, vector);
                if (dotSide>0 && (predictedVector.magnitude < _distanceThreshold || vector.magnitude<_distanceThreshold))
                {
                    Jump();
                    action = 2;
                }
            }
        }
        if(_previousAction==1 && action == 0)
        {
            DuckFinish();
        }
        _previousAction = action;
        _predictionError += Random.Range(0f, ErrorDelta) * Time.deltaTime;
    }
}
