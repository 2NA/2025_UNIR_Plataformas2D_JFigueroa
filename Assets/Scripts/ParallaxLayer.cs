using UnityEngine;

public class ParallaxLayer : MonoBehaviour
{
    [SerializeField] Camera cam;          // Cinemachine brain camera
    [SerializeField] float parallaxFactor = 0.5f; // 0 = fijo, 1 = se mueve como la cámara

    float spriteWidth;
    Vector3 startPos;

    void Start()
    {
        if (cam == null) cam = Camera.main;

        startPos = transform.position;
        var sr = GetComponent<SpriteRenderer>();
        spriteWidth = sr.bounds.size.x;   // Debe coincidir con la distancia entre A y B
    }

    void LateUpdate()
    {
        float deltaX = cam.transform.position.x * parallaxFactor;

        // Posición parallax
        float newX = startPos.x + deltaX;
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);

        // Repetición infinita (capa completa: A y B como hijos)
        float camX = cam.transform.position.x;
        float diff = camX - transform.position.x;

        if (diff >= spriteWidth)
        {
            startPos.x += spriteWidth;
        }
        else if (diff <= -spriteWidth)
        {
            startPos.x -= spriteWidth;
        }
    }
}
