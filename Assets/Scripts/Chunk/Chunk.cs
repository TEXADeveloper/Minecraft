using System.Collections.Generic;
using UnityEngine;

public class Chunk
{
    private GameObject chunkObj;
    private ChunkCoords coord;
    private MeshRenderer mr;
    private MeshFilter mf;

    #region lists
    private int vertexIndex = 0;
    private List<Vector3> vertexes = new List<Vector3>();
    private List<int> triangles = new List<int>();
    private List<Vector2> uvs = new List<Vector2>();
    private byte[,,] voxelMap = new byte[ChunkData.chunkSize.x, ChunkData.chunkSize.y, ChunkData.chunkSize.x];
    #endregion

    private World world;

    public bool isActive
    {
        get { return chunkObj.activeSelf; }
        set { chunkObj.SetActive(value); }
    }

    public Vector3 position
    {
        get { return chunkObj.transform.position; }
    }

    public Chunk(World worldRef, ChunkCoords coords)
    {
        world = worldRef;
        chunkObj = new GameObject("(" + coords.x + ", " + coords.z + ")");
        chunkObj.transform.parent = world.transform;
        chunkObj.transform.position = new Vector3(ChunkData.chunkSize.x * coords.x, 0, ChunkData.chunkSize.x * coords.z);
        coord = coords;
        startChunk();
    }

    private void startChunk()
    {
        mr = chunkObj.AddComponent<MeshRenderer>();
        mf = chunkObj.AddComponent<MeshFilter>();
        mr.material = world.mat;

        startVoxelMap();
        createMeshData();
        createMesh();
    }

    private void startVoxelMap()
    {
        for (int y = 0; y < ChunkData.chunkSize.y; y++)
            for (int x = 0; x < ChunkData.chunkSize.x; x++)
                for (int z = 0; z < ChunkData.chunkSize.x; z++)
                    voxelMap[x, y, z] = world.GetVoxelType(new Vector3(x, y, z) + position);
    }

    private void createMeshData()
    {
        for (int y = 0; y < ChunkData.chunkSize.y; y++)
            for (int x = 0; x < ChunkData.chunkSize.x; x++)
                for (int z = 0; z < ChunkData.chunkSize.x; z++)
                    voxelDataToChunk(new Vector3Int(x, y, z));
    }

    private void voxelDataToChunk(Vector3Int pos)
    {
        for (int i = 0; i < 6; i++)
            if (!checkVoxel(pos + VoxelData.faceChecks[i]))
            {
                //! FIXME: Error acá dentro
                byte blockID = voxelMap[pos.x, pos.y, pos.z]; //!Había un error acá
                for (int j = 0; j < 4; j++)
                    vertexes.Add(VoxelData.vertexes[VoxelData.triangles[i, j]] + pos);
                addTexture(world.blockTypes[blockID].GetTextureId(i));
                triangles.Add(vertexIndex);
                triangles.Add(vertexIndex + 1);
                triangles.Add(vertexIndex + 2);
                triangles.Add(vertexIndex + 2);
                triangles.Add(vertexIndex + 1);
                triangles.Add(vertexIndex + 3);
                vertexIndex += 4;
            }
    }

    bool checkVoxel(Vector3 pos)
    {
        int x = Mathf.FloorToInt(pos.x);
        int y = Mathf.FloorToInt(pos.y);
        int z = Mathf.FloorToInt(pos.z);

        if (!isVoxelInChunk(x, y, z))
            return world.blockTypes[world.GetVoxelType(pos + position)].isSolid;
        return world.blockTypes[voxelMap[x, y, z]].isSolid;
    }

    bool isVoxelInChunk(int x, int y, int z)
    {
        return !(x < 0 || x >= ChunkData.chunkSize.x || y < 0 || y >= ChunkData.chunkSize.y || z < 0 || z >= ChunkData.chunkSize.x);
    }

    private void createMesh()
    {
        Mesh mesh = new Mesh();
        mesh.vertices = vertexes.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.uv = uvs.ToArray();
        mesh.RecalculateNormals();
        mf.mesh = mesh;
    }

    private void addTexture(int textureID)
    {
        float y = textureID / VoxelData.TextureSizeInBlocks;
        float x = textureID - (y * VoxelData.TextureSizeInBlocks);

        x *= VoxelData.NormalizedBlockTextureSize;
        y *= VoxelData.NormalizedBlockTextureSize;
        y = 1f - y - VoxelData.NormalizedBlockTextureSize;

        uvs.Add(new Vector2(x, y));
        uvs.Add(new Vector2(x, y + VoxelData.NormalizedBlockTextureSize));
        uvs.Add(new Vector2(x + VoxelData.NormalizedBlockTextureSize, y));
        uvs.Add(new Vector2(x + VoxelData.NormalizedBlockTextureSize, y + VoxelData.NormalizedBlockTextureSize));
    }
}

public class ChunkCoords
{
    public int x;
    public int z;

    public ChunkCoords(int x, int z)
    {
        this.x = x;
        this.z = z;
    }
}
