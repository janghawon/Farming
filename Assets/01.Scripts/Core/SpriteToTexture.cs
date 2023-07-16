using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteToTexture : MonoBehaviour
{
    public static Texture ConvertSpriteToTex(Sprite sprite)
    {
        try
        {
            if(sprite.rect.width != sprite.texture.width)
            {
                int x = Mathf.FloorToInt(sprite.textureRect.x);
                int y = Mathf.FloorToInt(sprite.textureRect.y);
                int width = Mathf.FloorToInt(sprite.textureRect.width);
                int height = Mathf.FloorToInt(sprite.textureRect.height);

                Texture2D newTexture = new Texture2D(width, height);
                Color[] newColors = sprite.texture.GetPixels(x, y, width, height);

                newTexture.SetPixels(newColors);
                newTexture.Apply();
                return newTexture;
            }
            else
            {
                return sprite.texture;
            }
        }
        catch
        {
            return sprite.texture;
        }
    }
}
