using UnityEngine;

public class LevelLogic : MonoBehaviour
{
    [SerializeField] int totalActiveBlocks;
    SceneLoader sceneLoader;
    // Start is called before the first frame update
    void Start()
    {
        //totalActiveBlocks = 0;
        sceneLoader = FindObjectOfType<SceneLoader>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void addBlock()
    {
        this.totalActiveBlocks++;
    }
    public void removeBlock()
    {
        this.totalActiveBlocks--;
        if (totalActiveBlocks == 0)
        {
            totalActiveBlocks = 0;
            sceneLoader.loadNextScene();
        }
    }
}
