using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureOffsetScrollDescending : MonoBehaviour
{
    // Scroll main texture based on time

    [SerializeField] float textureOffsetScrollSpeed = 0.5f;
    [SerializeField] float minTextureOffset;
    [SerializeField] float maxTextureOffset;

    [SerializeField] float lineWidthChangeSpeed;
    [SerializeField] float minLineWidth;
    [SerializeField] float maxLineWidth;
    Renderer rend;
    LineRenderer lineRend;
    float lineRendWidth;
    float initialLineRendWidth;
    float offset;
    bool textureOffsetDescending = true;
    bool lineWidthDescending = true;

    private void Awake()
    {
        rend = GetComponent<Renderer>();
        lineRend = GetComponent<LineRenderer>();
    }
    void Start()
    {
        initialLineRendWidth = lineRend.startWidth;
    }

    private void OnEnable()
    {
        offset = 1;
        lineRendWidth = initialLineRendWidth;
    }

    void Update()
    {
        if (textureOffsetDescending)
        {
            offset -= Time.deltaTime * textureOffsetScrollSpeed;            
        }
        else
        {
            offset += Time.deltaTime * textureOffsetScrollSpeed; 
        }

        if (lineWidthDescending)
        {
            lineRendWidth -= Time.deltaTime * textureOffsetScrollSpeed;
        }
        else
        {
            lineRendWidth += Time.deltaTime * textureOffsetScrollSpeed;
        }

        if (offset < minTextureOffset)
        {
            textureOffsetDescending = false;
        }

        if (lineRendWidth < minLineWidth)
        {
            lineWidthDescending = false;
        }

        if (offset > maxTextureOffset)
        {
            textureOffsetDescending = true;
        }

        if (lineRendWidth > maxLineWidth)
        {
            lineWidthDescending = true;
        }

        rend.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
        lineRend.widthCurve = AnimationCurve.Constant(0, 1, lineRendWidth);

    }
}
