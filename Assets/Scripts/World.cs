using UnityEngine;

public class World : MonoBehaviour
{
    [SerializeField] public Material mat;
    [SerializeField] public BlockType[] blockTypes;
    private Chunk[,] chunkMap = new Chunk[ChunkData.worldSizeInChunks.x, ChunkData.worldSizeInChunks.y];

    void Start()
    {
        generateWorld();
    }

    private void generateWorld()
    {
        for (int x = 0; x < ChunkData.worldSizeInChunks.x; x++)
            for (int z = 0; z < ChunkData.worldSizeInChunks.y; z++)
                createChunk(x, z);
    }

    private void createChunk(int x, int z)
    {
        chunkMap[x, z] = new Chunk(this, new ChunkCoords(x, z));
    }

    public byte GetVoxelType(Vector3 pos) //! recibe 16, y no carga la textura de arriba
    {
        if (!isVoxelInWorld(new Vector3Int(Mathf.FloorToInt(pos.x), Mathf.FloorToInt(pos.y), Mathf.FloorToInt(pos.z))))
            return 0;
        if (pos.y == ChunkData.chunkSize.y - 1)
            return 2;
        if (pos.y > ChunkData.chunkSize.y - 5)
            return 1;
        if (pos.y > 0)
            if (Random.Range(0, 15) == 0)
                return 6;
            else
                return 4;
        return 7;
    }

    private bool isChunkInWorld(ChunkCoords coords)
    {
        return (coords.x > 0 && coords.x < ChunkData.worldSizeInChunks.x - 1 &&
            coords.z > 0 && coords.z < ChunkData.worldSizeInChunks.y - 1);
    }

    private bool isVoxelInWorld(Vector3Int coords)
    {
        Debug.Log(coords);
        return (coords.x >= 0 && coords.x < VoxelData.worldSizeInBlocks.x &&
            coords.y >= 0 && coords.x < ChunkData.chunkSize.y &&
            coords.z >= 0 && coords.z < VoxelData.worldSizeInBlocks.y);
    }
}
