using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personagem : MonoBehaviour
{
    public float velocidadeFrente = 20;
    public float velocidadeRotacao = 80;

    private bool chao = false;
    private Rigidbody body;

    // Use this for initialization
    void Start()
    {
        body = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            this.transform.Translate(0, 0, (velocidadeFrente * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.S))
        {
            this.transform.Translate(0, 0, -(velocidadeFrente * Time.deltaTime));
        }

        if (Input.GetKey(KeyCode.A))
        {
            this.transform.Rotate(0, -(velocidadeRotacao * Time.deltaTime), 0);
        }

        if (Input.GetKey(KeyCode.D))
        {
            this.transform.Rotate(0, (velocidadeRotacao * Time.deltaTime), 0);
        }

        chao = Physics.Raycast(this.transform.position, -Vector3.up, 1f);//ultimo parametro é o raio para verificar a distancia do eprsonagem do chao

        if (chao && Input.GetKeyDown(KeyCode.Space))
        {
            body.velocity = new Vector3(0, Input.GetAxis("Jump") + 3, 0);
        }
    }
}
