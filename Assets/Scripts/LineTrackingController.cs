using UnityEngine;

public class LineTrackingController : MonoBehaviour
{
    public enum InfraRed
    {
        Left,
        Middle,
        Right
    }

    public float luminosityBias = 0.5f;

    [Header("References")] public Transform leftInfraRed;
    public Transform middleInfraRed;
    public Transform rightInfraRed;

    public bool GetInfraRed(InfraRed infraRed)
    {
        Transform source;
        switch (infraRed)
        {
            case InfraRed.Left:
                source = leftInfraRed;
                break;
            case InfraRed.Middle:
                source = middleInfraRed;
                break;
            case InfraRed.Right:
                source = rightInfraRed;
                break;
            default:
                source = null;
                break;
        }

        if (source == null ||
            !Physics.Raycast(source.position, source.forward, out var hitInfo, 0.05f))
            return false;

        var meshRenderer = hitInfo.transform.GetComponent<Renderer>();
        var meshCollider = hitInfo.collider as MeshCollider;

        if (meshRenderer == null ||
            meshRenderer.sharedMaterial == null ||
            meshRenderer.sharedMaterial.mainTexture == null ||
            meshCollider == null)
            return false;

        var texture = meshRenderer.material.mainTexture as Texture2D;

        if (texture == null)
            return false;

        var textureCoord = hitInfo.textureCoord;
        textureCoord.x *= texture.width;
        textureCoord.y *= texture.height;
        var luminosity = texture.GetPixel((int) textureCoord.x, (int) textureCoord.y).grayscale;

        return luminosity < luminosityBias;
    }
}