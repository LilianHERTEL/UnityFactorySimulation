using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapisRoulant : MonoBehaviour
{
    Rigidbody _rigidbody;
    Renderer _renderer;

    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log("AVANCE");
        Vector3 position = _rigidbody.position;
        _rigidbody.position += Vector3.back * speed * Time.fixedDeltaTime;
        _rigidbody.MovePosition(position);

        _renderer.material.SetTextureOffset("_MainTex", new Vector2(0, Time.time * speed));
    }
}
