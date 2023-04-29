using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    Vector2 _startPosition;
    Rigidbody2D _rigidBody2D;

    void Awake()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        _startPosition = _rigidBody2D.position;
        _rigidBody2D.isKinematic = true;
    }

    private void OnMouseUp()
    {
        var currentPosition = _rigidBody2D.position;
        Vector2 direction = _startPosition - currentPosition;
        direction.Normalize();
        _rigidBody2D.isKinematic = false;
        _rigidBody2D.AddForce(direction * 1000);
    }
    private void OnMouseDrag()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePosition.x, mousePosition.y, transform.position.z);
    }
    // Update is called once per frame+
    void Update()
    {
        
    }
}
