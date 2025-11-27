using UnityEngine;

public class BGloop : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float loopSpeed;
    public Renderer bgRenderer;

    // Update is called once per frame
    void Update()
    {
        bgRenderer.material.mainTextureOffset += new Vector2(loopSpeed * Time.deltaTime,0f);
    }
}
