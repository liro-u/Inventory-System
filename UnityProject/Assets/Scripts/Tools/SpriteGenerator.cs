using UnityEngine;
using System.IO;
using UnityEditor;

public class SpriteGenerator : MonoBehaviour
{
    public Camera captureCamera;
    public Transform models;
    public bool overwrite;
    private string outputFolder = "Assets/Resources/Items/Sprites2D/";

    private void Start()
    {
        foreach(Transform transform in models)
        {
            transform.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        RenderSprite();
    }

    void RenderSprite()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            for (int i = 0; i < models.childCount; i++)
            {
                GameObject model = models.GetChild(i).gameObject;

                model.SetActive(true);

                RenderTexture rt = new RenderTexture(512, 512, 24);
                rt.graphicsFormat = UnityEngine.Experimental.Rendering.GraphicsFormat.R8G8B8A8_UNorm;
                captureCamera.targetTexture = rt;

                Texture2D screenshot = new Texture2D(512, 512, TextureFormat.RGBA32, false);

                captureCamera.Render();
                RenderTexture.active = rt;
                screenshot.ReadPixels(new Rect(0, 0, 512, 512), 0, 0);
                screenshot.Apply(); // Applique les changements à la texture

                RenderTexture.active = null;

                string filePath = outputFolder + model.name + ".png";
                if (!overwrite && File.Exists(filePath))
                {
                    Debug.Log("File already exists, skipping save: " + filePath);
                }
                else
                {
                    File.WriteAllBytes(filePath, screenshot.EncodeToPNG());
                    Debug.Log(model.name + " has been saved as a PNG into: " + outputFolder);
                }

                captureCamera.targetTexture = null;
                Destroy(rt);
                model.SetActive(false);
            }
            AssetDatabase.Refresh();
        }
    }
}
