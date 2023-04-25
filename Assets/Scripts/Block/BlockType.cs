using UnityEngine;

[System.Serializable]
public class BlockType
{
    public string blockName;
    public bool isSolid;
    [Tooltip("Back, Front, Top, Bottom, Left, Right")]
    public int[] textureIds;

    public int GetTextureId(int faceIndex)
    {
        if (faceIndex >= 0 && faceIndex < textureIds.Length)
            return textureIds[faceIndex];
        return 0;
    }
}
