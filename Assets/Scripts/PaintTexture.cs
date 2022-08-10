using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PaintTexture : MonoBehaviour
{
    [SerializeField] MeshRenderer meshRenderer;
    [SerializeField] Texture2D brush;
    [SerializeField] Vector2Int textureArea;
    Texture2D texture;

    [SerializeField] Slider paintPercent;
    [SerializeField] TextMeshProUGUI percentText;

    int red = 0;

    int maxNumber;

    float percent;

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        texture = new Texture2D(textureArea.x, textureArea.y, TextureFormat.ARGB32, false);
        meshRenderer.material.mainTexture = texture;

        maxNumber = texture.width * texture.height;

        paintPercent.minValue = 0f;
        paintPercent.maxValue = 100f;
        paintPercent.value = 0f;

        percent = 0;

        percentText.text = percent.ToString("0") + "%"; 

        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameControl.instance.isEnd)
        {
            Painting();
        }
        
    }

    void Painting()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Paintable"))
            {
                Paint(hit.textureCoord);
            }
        }
    }

    void Paint(Vector2 coordinate)
    {
        coordinate.x *= texture.width;
        coordinate.y *= texture.height;
        Color32[] textureC32 = texture.GetPixels32();
        Color32[] brushC32 = brush.GetPixels32();

        Vector2Int halfBrush = new Vector2Int(brush.width / 2, brush.height / 2);

        for (int x = 0; x < brush.width; x++)
        {
            int xPos = x - halfBrush.x + (int)coordinate.x;
            if (xPos < 0 || xPos >= texture.width) continue;

            for (int y = 0; y < brush.height; y++)
            {
                int yPos = y - halfBrush.y + (int)coordinate.y;
                if (yPos < 0 || yPos >= texture.height) continue;

                if (brushC32[x + (y * brush.width)].a > 0f)
                {
                    int tPos = xPos + (texture.width * yPos);

                    if (brushC32[x+ (y * brush.width)].r > textureC32[tPos].r)
                    {
                        textureC32[tPos] = brushC32[x + (y * brush.width)];
                    }
                }
            }
        }
        Percent();
        texture.SetPixels32(textureC32);
        texture.Apply();
    }

    void Percent()
    {
        Color32[] textureC32 = texture.GetPixels32();
        percent = Mathf.Clamp(percent, 0f, 100f);

        foreach (var pixel in textureC32)
        {
            if (pixel.a != 205)
            {
                red++;
            }
        }
        percent = (float)(red * 100) / maxNumber;
        paintPercent.value = percent;
        percentText.text = percent.ToString("0.0") + "%";

        WinControl(percent);

        red = 0;
    }

    void WinControl(float percent)
    {
        if(percent >= 99.9f)
        {
            GameControl.instance.paintWin = true;
        }
    }

}
