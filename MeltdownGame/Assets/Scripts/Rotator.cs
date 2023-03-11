using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] float _initialVelocity;
    [SerializeField] float _maxVelocity;
    [SerializeField] float _minVelocity;
    [SerializeField] float _velocityChangeDelta;
    private Vector3 _rotateVelocity;
    public List<Transform> EdgeObjects;

    public void StartRotating()
    {
        _rotateVelocity.y = _initialVelocity;
        StartCoroutine(Coroutine_VelocityChange());
    }

    void Update()
    {
        transform.Rotate(_rotateVelocity*Time.deltaTime);
    }

    public float GetVelocity()
    {
        return _rotateVelocity.y;
    }

    IEnumerator Coroutine_VelocityChange()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(3, 5));
            int sign = Random.Range(0, 2) == 0 ? -1 : 1;
            _rotateVelocity.y = Mathf.Clamp(_rotateVelocity.y + sign * _velocityChangeDelta, _minVelocity, _maxVelocity);
        }
    }
}
