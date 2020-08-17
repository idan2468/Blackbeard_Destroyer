using UnityEngine;

public class Block : MonoBehaviour
{
    // config params
    LevelLogic levelLogic;
    GameSession gameSession;
    private const float sparkleDuration = 1f;
    [SerializeField] private int maxBlockHealth;
    private const int BROKEN_TWO_HEALTH = 2;
    private const int BROKEN_ONE_HEALTH = 1;
    private int currBlockHealth; // for debugging

    // cache components
    [SerializeField] AudioClip destgorySound = null;
    [SerializeField] GameObject blockSparklesVFX = null;
    [SerializeField] Sprite[] damageLevelSprites = null;


    private void Start()
    {
        cacheComponents();
        if (tag == "Breakable")
        {
            levelLogic.addBlock();
            maxBlockHealth = damageLevelSprites.Length + 1;
            currBlockHealth = maxBlockHealth;
        }
    }

    private void cacheComponents()
    {
        levelLogic = FindObjectOfType<LevelLogic>();
        gameSession = FindObjectOfType<GameSession>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (tag == "Breakable")
        {
            handleHit();
        }
    }

    private void handleHit()
    {
        currBlockHealth--;
        if (currBlockHealth == 0)
        {
            DestroyBlock();
        }
        else
        {
            showNextDamageSprite();
        }
    }

    private void showNextDamageSprite()
    {
        int spriteIndex = currBlockHealth - 1;

        GetComponent<SpriteRenderer>().sprite = damageLevelSprites[spriteIndex];

    }

    private void DestroyBlock()
    {
        AudioSource.PlayClipAtPoint(destgorySound, Camera.main.transform.position);
        gameSession.addScore();
        levelLogic.removeBlock();
        triggerSparklesVFX();
        Destroy(gameObject);
    }

    private void triggerSparklesVFX()
    {
        GameObject sparkles = Instantiate(blockSparklesVFX, gameObject.transform.position, gameObject.transform.rotation);
        Destroy(sparkles, sparkleDuration);
    }
}
