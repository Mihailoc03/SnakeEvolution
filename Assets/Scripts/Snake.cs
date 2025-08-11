using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private Vector2 _direction = Vector2.right;

    private List<Transform> _segments = new List<Transform>();

    public GameOverManager gameOverManager;

    public Transform segmentPrefab;

    public ScoreManager scoreManager;

    public int initialSize = 4;

    public float moveDelay = 0.15f;
    public float initialMoveDelay = 0.15f;
    public float speedIncreaseStep = 0.02f;

    private float moveTimer = 0f;

    private void Start()
    {
        ResetState();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            _direction = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            _direction = Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            _direction = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            _direction = Vector2.right;
        }

        moveTimer += Time.deltaTime;
        if (moveTimer >= moveDelay)
        {
            moveSnake();
            moveTimer = 0f;
        }
    }
    private void moveSnake()
    {
        for (int i = _segments.Count - 1; i > 0; i--)
        {
            if (_segments[i] != null && _segments[i - 1] != null)
                _segments[i].position = _segments[i - 1].position;
        }

        this.transform.position = new Vector3(
            Mathf.Round(this.transform.position.x) + _direction.x,
            Mathf.Round(this.transform.position.y) + _direction.y,
            0.0f
        );

        for (int i = 1; i < _segments.Count; i++)
        {
            if (this.transform.position == _segments[i].position)
            {
                gameOverManager.GameOver();
                return;
            }
        }
    }

    private void Grow()
    {
        Transform segment = Instantiate(segmentPrefab);
        segment.position = _segments[_segments.Count - 1].position;
        _segments.Add(segment);
    }

    public void ResetState()
    {
        for (int i = 1; i < _segments.Count; i++)
        {
            Destroy(_segments[i].gameObject);
        }
        _segments.Clear();
        _segments.Add(this.transform);

        for (int i = 1; i < initialSize; i++)
        {
            Transform segment = Instantiate(segmentPrefab);
            segment.position = new Vector3(-i, 0, 0);
            segment.tag = "Player";
            _segments.Add(segment);
        }

        this.transform.position = Vector3.zero;
        moveDelay = initialMoveDelay;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "NormalFood")
        {
            Grow();
            scoreManager.AddScore(1);
            Destroy(other.gameObject);
        }
        else if (other.tag == "BonusFood")
        {
            Grow();
            Grow();
            scoreManager.AddScore(3);
            StartCoroutine(SpeedUp());
            Destroy(other.gameObject);
        }
        else if (other.tag == "SlowFood")
        {
            StartCoroutine(SpeedDown());
            scoreManager.AddScore(-1);
            Destroy(other.gameObject);
        }
        else if (other.tag == "Wall" || other.tag == "Player")
        {
            gameOverManager.GameOver();
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Wall") || other.CompareTag("Player"))
        {
            gameOverManager.GameOver();
        }
    }

    private IEnumerator SpeedUp()
    {
        float originalDelay = moveDelay;
        moveDelay *= 0.5f;
        yield return new WaitForSeconds(3f);
        moveDelay = originalDelay;
    }

    private IEnumerator SpeedDown()
    {
        float originalDelay = moveDelay;
        moveDelay *= 2f;
        yield return new WaitForSeconds(3f);
        moveDelay = originalDelay;
    }
}