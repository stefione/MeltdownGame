using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterController : MonoBehaviour
{
    [SerializeField] Animator _anim;
    [SerializeField] AnimationCurve _jumpCurve;
    [SerializeField] float _jumpStrength;
    [SerializeField] float _jumpSpeed;
    [SerializeField] float _jumpDelay;
    private float _startHeight;
    [SerializeField] List<Rigidbody> _radgollParts;

    public bool ActionInProgress;
    public bool Defeated { get; private set; }

    protected virtual void Start()
    {
        _startHeight = transform.position.y;
    }

    protected virtual void Update()
    {
    }

    public void Duck()
    {
        _anim.SetBool("Duck", true);
    }

    public void DuckFinish()
    {
        _anim.SetBool("Duck", false);
    }

    public void Jump()
    {
        _anim.SetTrigger("Jump");
        StopAllCoroutines();
        StartCoroutine(Coroutine_Jump());
    }

    public virtual void ActivateRagdoll()
    {
        _anim.enabled = false;
        for(int i=0;i< _radgollParts.Count; i++)
        {
            _radgollParts[i].isKinematic = false;
        }
        Defeated = true;
        GameManager.Instance.EliminateCharacter(this);
    }
    IEnumerator Coroutine_Jump()
    {
        ActionInProgress = true;
        yield return new WaitForSeconds(_jumpDelay);
        float lerp = 0;
        while (lerp < 1)
        {
            Vector3 pos = transform.position;
            pos.y = _startHeight + _jumpStrength * _jumpCurve.Evaluate(lerp);
            transform.position = pos;
            lerp += Time.deltaTime* _jumpSpeed;
            yield return null;
        }
        Vector3 finalPos = transform.position;
        finalPos.y = _startHeight;
        transform.position = finalPos;
        ActionInProgress = false;
    }

}
