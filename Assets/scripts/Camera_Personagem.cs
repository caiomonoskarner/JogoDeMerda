using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Camera_Personagem : MonoBehaviour
{
    public Transform MeuJogador;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        seguir();
    }

    void seguir()
    {
        Vector3 desiredPosition = new Vector3(MeuJogador.position.x + offset.x, MeuJogador.position.y + offset.y, transform.position.z);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
