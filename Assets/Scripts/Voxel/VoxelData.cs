using UnityEngine;

public static class VoxelData
{
    public static Vector2Int worldSizeInBlocks
    {
        get
        {
            return new Vector2Int(ChunkData.worldSizeInChunks.x * ChunkData.chunkSize.x,
                                     ChunkData.worldSizeInChunks.y * ChunkData.chunkSize.x);
        }
    }

    public static readonly int TextureSizeInBlocks = 4;
    public static float NormalizedBlockTextureSize
    {
        get { return 1f / TextureSizeInBlocks; }
    }


    public static readonly Vector3Int[] vertexes = new Vector3Int[8]
    {
        new Vector3Int(0, 0, 0),
        new Vector3Int(1, 0, 0),
        new Vector3Int(1, 1, 0),
        new Vector3Int(0, 1, 0),
        new Vector3Int(0, 0, 1),
        new Vector3Int(1, 0, 1),
        new Vector3Int(1, 1, 1),
        new Vector3Int(0, 1, 1)
    };

    public static readonly int[,] triangles = new int[6, 4]
    {
        {0, 3, 1, 2}, //?Back
        {5, 6, 4, 7}, //?Front
        {3, 7, 2, 6}, //?Top
        {1, 5, 0, 4}, //?Bottom
        {4, 7, 0, 3}, //?Left
        {1, 2, 5, 6}  //?Right
    };

    public static readonly Vector3Int[] faceChecks = new Vector3Int[6]
    {
        new Vector3Int(0, 0, -1),
        new Vector3Int(0, 0, 1),
        new Vector3Int(0, 1, 0),
        new Vector3Int(0, -1, 0),
        new Vector3Int(-1, 0, 0),
        new Vector3Int(1, 0, 0)
    };

    public static readonly Vector2Int[] uvs = new Vector2Int[4]
    {
        new Vector2Int(0, 0),
        new Vector2Int(0, 1),
        new Vector2Int(1, 0),
        new Vector2Int(1, 1)
    };
}
