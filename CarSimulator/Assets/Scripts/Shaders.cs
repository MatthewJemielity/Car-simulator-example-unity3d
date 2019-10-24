using UnityEngine;
using System.Collections;

public class Shaders : MonoBehaviour
{

    public enum ShaderMode
    {
        Normal = 1,
        Outline = 2,
        RedOutline = 3,
        Transparent = 4,
        Invisible = 5,
    }

    public static Shader _transparent;
    public static Shader transparent {
        get {
            if (_transparent == null) {
                _transparent = Shader.Find("Transparent/Diffuse");
            }
            return _transparent;
        }
    }

    public static Shader _outline;
    public static Shader outline {
        get {
            if (_outline == null) {
                _outline = Shader.Find("Outlined/Silhouetted Diffuse");
            }
            return _outline;
        }
    }
    public static Shader _silhouetteOnly;
    public static Shader silhouetteOnly {
        get {
            if (_silhouetteOnly == null) {
                _silhouetteOnly = Shader.Find("Outlined/Silhouette Only");
            }
            return _silhouetteOnly;
        }
    }

    public static Shader _standardOutlined;
    public static Shader standardOutlined {
        get {
            if (_standardOutlined == null) {
                _standardOutlined = Shader.Find("Standard Outlined");
            }
            return _standardOutlined;
        }
    }

    public static Color Red = new Color(1f, 0f, 0f);
    public static Color Orange = new Color(1f, 0.557f, 0f);
    public static Color Green = new Color(0.2f, 1f, 0f);

    // from http://answers.unity3d.com/questions/812240/convert-hex-int-to-colorcolor32.html
    public static Color hexToColor(string hex)
    {
        hex = hex.Replace("0x", ""); //in case the string is formatted 0xFFFFFF
        hex = hex.Replace("#", ""); //in case the string is formatted #FFFFFF
        byte a = 255; //assume fully visible unless specified in hex
        byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);

        //Only use alpha if the string has enough characters
        if (hex.Length == 8) {
            a = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
        }
        return new Color32(r, g, b, a);
    }

    public static void setShader(ShaderMode mode, Material mat, Color color, bool show = true)
    {


        switch (mode)
        {
            case ShaderMode.Normal:
                mat.color = new Color(color.r, color.g, color.b, 120);
                //mat.shader = Shaders.transparent;
                break;
            case ShaderMode.RedOutline:
                mat.shader = Shaders.transparent;
                mat.color = new Color(color.r, color.g, color.b, 120);
                break;

        }
    }

    public static void setTransparent(Material mat, Color color, bool _transp = true)
    {
        if (_transp)
        {
            mat.color = new Color(color.r, color.g, color.b, 0.35f);
        }
        else
        {
            mat.color = new Color(color.r, color.g, color.b, 1f);
        }

    }
}