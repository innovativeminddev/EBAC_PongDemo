using UnityEngine;

public class BallController : MonoBehaviour
{
    [Header("Configurações de Velocidade")]
    public float initialSpeed = 8f;
    public float speedIncreasePerHit = 0.5f;

    private Rigidbody2D rb;
    private float currentSpeed;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void LaunchBall()
    {
        transform.position = Vector2.zero;
        currentSpeed = initialSpeed;

        // Direção X aleatória (Esquerda ou Direita)
        float xDir = Random.Range(0, 2) == 0 ? -1f : 1f;
        // Direção Y aleatória em ângulo moderado
        float yDir = Random.Range(-0.6f, 0.6f);

        Vector2 direction = new Vector2(xDir, yDir).normalized;
        rb.linearVelocity = direction * currentSpeed; // Se estiver em versão mais antiga do Unity, use rb.velocity
    }

    public void ResetBall()
    {
        rb.linearVelocity = Vector2.zero;
        transform.position = Vector2.zero;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Se rebater num Paddle, aumenta ligeiramente a velocidade para acelerar a partida
        if (collision.gameObject.GetComponent<PaddleController>() != null)
        {
            currentSpeed += speedIncreasePerHit;
            rb.linearVelocity = rb.linearVelocity.normalized * currentSpeed;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("GoalLeft"))
        {
            GameManager.Instance.OnGoalScored(2); // Ponto do Jogador 2
        }
        else if (other.CompareTag("GoalRight"))
        {
            GameManager.Instance.OnGoalScored(1); // Ponto do Jogador 1
        }
    }
}