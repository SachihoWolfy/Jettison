using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField] float _launchForce = 500;
    [SerializeField] float _maxDragDistance = 5;
    Vector2 _startPosition;
    Rigidbody2D _rigidBody2D;
    bool launched = false;

    public bool IsDraging { get; private set; }

    void Awake()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        _startPosition = _rigidBody2D.position;
        _rigidBody2D.isKinematic = true;
    }

    private void OnMouseDown()
    {
        IsDraging = true;
    }
    private void OnMouseUp()
    {
        var currentPosition = _rigidBody2D.position;
        Vector2 direction = _startPosition - currentPosition;
        direction.Normalize();
        _rigidBody2D.isKinematic = false;
        _rigidBody2D.freezeRotation = false;
        _rigidBody2D.AddForce(direction * _launchForce);
        launched = true;
        IsDraging = false;
    }
    private void OnMouseDrag()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 desiredPosition = mousePosition;

        float distance = Vector2.Distance(desiredPosition, _startPosition);
        if(distance > _maxDragDistance)
        {
            Vector2 direction = desiredPosition - _startPosition;
            direction.Normalize();
            desiredPosition = _startPosition + (direction * _maxDragDistance);
        }
        if (desiredPosition.x > _startPosition.x)
        {
            desiredPosition.x = _startPosition.x;
        }
        _rigidBody2D.position = desiredPosition;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(launched)
            StartCoroutine(ResetAfterDelay());
    }
    IEnumerator ResetAfterDelay()
    {
        yield return new WaitForSeconds(3);
        _rigidBody2D.position = _startPosition;
        _rigidBody2D.isKinematic = true;
        _rigidBody2D.freezeRotation = true;
        _rigidBody2D.SetRotation(0);
        _rigidBody2D.velocity = Vector2.zero;
        launched = false;
    }



    // Update is called once per frame+
    void Update()
    {
        
    }
}
