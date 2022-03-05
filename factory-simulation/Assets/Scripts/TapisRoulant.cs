using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapisRoulant : MonoBehaviour
{
    private float _speed = 0;
    public GameObject begin, main, end;
    Renderer rendererBegin, rendererMain, rendererEnd;
    Rigidbody rigidbodyTapis;

    // Start is called before the first frame update
    void Start()
    {
        rigidbodyTapis = GetComponent<Rigidbody>();
        rendererBegin = begin.GetComponent<Renderer>();
        rendererMain = main.GetComponent<Renderer>();
        rendererEnd = end.GetComponent<Renderer>();

        _speed = GameManager.conveyor_speed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log("AVANCE");
        Vector3 position = rigidbodyTapis.position;
        rigidbodyTapis.position += Vector3.back * _speed * Time.fixedDeltaTime;
        rigidbodyTapis.MovePosition(position);

        rendererBegin.material.SetTextureOffset("_MainTex", new Vector2(-Time.time * _speed * 2, 0));
        rendererMain.material.SetTextureOffset("_MainTex", new Vector2(0, Time.time * _speed));
        rendererEnd.material.SetTextureOffset("_MainTex", new Vector2(-Time.time * _speed * 2, 0));
    }
}
