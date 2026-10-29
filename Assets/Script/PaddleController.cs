using UnityEngine;

public class PaddleController : MonoBehaviour
{
    [Header("Configurações do Paddle")]
    public float speed = 12f;
    public bool isPlayer2 = false;

    private float boundY = 4.0f; // Limite vertical da tela

    void Update()
    {
        float input = 0f;

        if (!isPlayer2)
        {
            // Jogador 1 (W / S)
            if (Input.GetKey(KeyCode.W)) input = 1f;
            else if (Input.GetKey(KeyCode.S)) input = -1f;
        }
        else
        {
            // Jogador 2 (Setas para Cima / Baixo)
            if (Input.GetKey(KeyCode.UpArrow)) input = 1f;
            else if (Input.GetKey(KeyCode.DownArrow)) input = -1f;
        }

        // Movimentação limitada no eixo Y
        Vector3 pos = transform.position;
        pos.y += input * speed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, -boundY, boundY);
        transform.position = pos;
    }
}