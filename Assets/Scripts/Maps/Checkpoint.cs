using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private Vector2 checkpointPosition;
    private SpriteRenderer spriteRenderer;
    private bool triggered = false;
    // Start is called before the first frame update
    void Start()
    {
        DisableSpriteRenderer();
        SetCheckpointPosition();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetCheckpointPosition() {
        checkpointPosition = new Vector2(transform.position.x, transform.position.y);
    }

    void DisableSpriteRenderer() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
    }

    public Vector2 Position() {
        return checkpointPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(!triggered && collision.tag == "Player") {
            TriggerCheckpoint();
        }
    }

    private void TriggerCheckpoint() {
        PlayerController.InstanciaPlayerController.SetCheckpoint(this);
        triggered = true;
    }
}
