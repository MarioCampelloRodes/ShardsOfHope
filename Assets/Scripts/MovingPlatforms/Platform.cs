using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Platform : MonoBehaviour
{
    public ObjectPool<Platform> platformPool;

    [SerializeField] private float speed = 8f;
    //limite en Z que limita cuándo la plataforma ya no es visible
    [SerializeField] private float despawn = -10f;

    private void Update()
    {
        //mover el segmento hacia atrás
        transform.Translate(Vector3.back * speed * Time.deltaTime);

        //comprobar si ha pasado el límite de visión
        if (transform.position.z < despawn)
        {
            platformPool.Release(this);
        }
    }
    
    public void ResetPlatform()
    {
       
    }
}
