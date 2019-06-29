using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Generates blocks, fuel and coins
/// </summary>
public class ObstacleController : MonoBehaviour
{
    public Color[] colors;
    public GameObject[] effects;
    public Block block;
    public Fuel fuel;
    public Coin coin;
    Field[,] fields;

    public EndLevel endLevel;
    public Level level;
    public Queue<Block> blocks = new Queue<Block>();
    public CarController carController;

    void Awake()
    {
        for (int i = 0; i < 100; i++)
        {
            Block block = Instantiate(this.block);
            block.gameObject.SetActive(false);
            block.Init(this);
            blocks.Enqueue(block);
        }

        GenerateLevel(level);
    }

    /// <summary>
    /// Generates blocks, fuel and coins base od the level. Set's their color and values
    /// </summary>
    /// <param name="level">Current level of the game</param>
    public void GenerateLevel(Level level)
    {
        Vector2 startPosition = level.startPositon;

        fields = new Field[level.length, 5];

        for (int i = 0; i < level.length; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                fields[i, j] = new Field(new Vector2(level.spawnPositions[j].x, startPosition.y), null);
            }
            startPosition.y += 1;
        }

        int blocksGenerated = 0;

        for (int i = 0; i < level.length; i += level.spawnRate)
        {
            for (int j = 0; j < 5; j++)
            {
                Field field = fields[i, j];
                if (Random.value < level.blockSpawnChance / 100f)
                {
                    Block block = blocks.Dequeue();
                    block.gameObject.SetActive(true);
                    int min = level.minValue;
                    int max = Mathf.Min(level.minMaxValue + GameManager.instance.data.level + 1, level.maxValue);
                    block.health = Random.Range(min, max);
                    block.transform.position = field.positon;
                    field.spawnableObject = block;
                    blocksGenerated++;
                    int x = Mathf.RoundToInt((block.health - min) / (float)(max - min) * (colors.Length - 1));
                    block.color = colors[x];
                    block.hitEffect = effects[x];
                }
            }
        }

        for (int i = 0; i < (blocksGenerated / 100f) * level.coinRatio; i++)
        {
            Field field = null;
            do
            {
                field = fields[Random.Range(0, level.length), Random.Range(0, 5)];
            }
            while (field.spawnableObject != null);
            Coin coin = Instantiate(this.coin);
            field.spawnableObject = coin;
            coin.transform.position = field.positon;
        }

        for (int i = 0; i < (blocksGenerated / 100f) * level.fuelRation; i++)
        {
            Field field = null;
            do
            {
                field = fields[Random.Range(0, level.length), Random.Range(0, 5)];
            }
            while (field.spawnableObject != null);
            Fuel fuel = Instantiate(this.fuel);
            field.spawnableObject = fuel;
            fuel.transform.position = field.positon;
            float speed = carController.maxSpeed + carController.speedLevel;
            fuel.fuel = Random.Range(level.minFuel, ((int)speed) / 3);
        }

        endLevel.transform.position = startPosition;
    }

}

/// <summary>
/// Level parameters for blocks, fuel and coins
/// </summary>
[System.Serializable]
public class Level
{
    [Header("Obstacle")]
    [Tooltip("Length of a level in a unity units")]
    public int length = 30;
    [Tooltip("Spawn rate for every n number of unity units")]
    public int spawnRate = 5;
    [Range(0, 100)]
    [Tooltip("Chance that a block will spawn")]
    public float blockSpawnChance = 50;
    [Tooltip("Minimum damage value of a block")]
    public int minValue = 5;
    [Tooltip("Minimum damage value of a block")]
    public int minMaxValue = 30;
    [Tooltip("Maximum damage value of a block")]
    public int maxValue = 150;
    [Header("Fuel")]
    [Range(0, 100)]
    [Tooltip("Fuel spawn chance")]
    public int fuelRation = 30;
    [Tooltip("Minimum flue value")]
    public int minFuel = 3;
    [Header("Coin")]
    [Range(0, 100)]
    [Tooltip("Coin spawn chance")]
    public int coinRatio = 18;

    [Header("")]
    [Header("")]
    [Header("")]

    public Vector2 startPositon;
    public Vector2[] spawnPositions;
}

/// <summary>
/// Spot where if a spawnable object is placed or not
/// </summary>
public class Field
{
    public Vector2 positon;
    public ISpawnable spawnableObject;

    public Field(Vector2 positon, ISpawnable spawnableObject)
    {
        this.positon = positon;
        this.spawnableObject = spawnableObject;
    }
}
