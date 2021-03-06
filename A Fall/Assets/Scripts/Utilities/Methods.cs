using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Methods
{

    public static Vector3 ZeroZMousePos()
    {
        return Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
    }

    public static Vector3 ZDistanceMousePos(float distanceFromCamera)
    {
        return Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distanceFromCamera));
    }

    public static Vector3 ChangeX(this Vector3 vec, float xValue)
    {
        return new Vector3(xValue, vec.y, vec.z);
    }
    public static Vector3 ChangeY(this Vector3 vec, float yValue)
    {
        return new Vector3(vec.x, yValue, vec.z);
    }
    public static Vector3 ChangeZ(this Vector3 vec, float zValue)
    {
        return new Vector3(vec.x, vec.y, zValue);
    }

    public static Vector3 OffsetX(this Vector3 vec, float xValue)
    {
        return new Vector3(vec.x + xValue, vec.y, vec.z);
    }
    public static Vector3 OffsetY(this Vector3 vec, float yValue)
    {
        return new Vector3(vec.x, vec.y + yValue, vec.z);
    }
    public static Vector3 OffsetZ(this Vector3 vec, float zValue)
    {
        return new Vector3(vec.x, vec.y, vec.z + zValue);
    }

    public static Vector3 ClampX(this Vector3 vec, float min, float max)
    {
        return new Vector3(Mathf.Clamp(vec.x, min, max), vec.y, vec.z);
    }
    public static Vector3 ClampY(this Vector3 vec, float min, float max)
    {
        return new Vector3(vec.x, Mathf.Clamp(vec.y, min, max), vec.z);
    }
    public static Vector3 ClampZ(this Vector3 vec, float min, float max)
    {
        return new Vector3(vec.x, vec.y, Mathf.Clamp(vec.z, min, max));
    }

    public static Vector3 MultiplyVector(Vector3 vec1, Vector3 vec2)
    {
        return new Vector3(vec1.x * vec2.x, vec1.y * vec2.y, vec1.z * vec2.z);
    }
    public static Vector3 MultiplyVector(Vector3 vec1, Vector3 vec2, Vector3 vec3)
    {
        return new Vector3(vec1.x * vec2.x * vec3.x, vec1.y * vec2.y * vec3.y, vec1.z * vec2.z * vec3.z);
    }
    public static Vector3 MultiplyVector(Vector3 vec1, Vector3 vec2, float value)
    {
        return new Vector3(vec1.x * vec2.x * value, vec1.y * vec2.y * value, vec1.z * vec2.z * value);
    }

    public static void SetMaterialOpacity(GameObject go, float value)
    {
        MaterialPropertyBlock matProp = new MaterialPropertyBlock();
        matProp.SetFloat("_Treshold", value); //A save et Load en opaque ou carr?ment changer le shader
        go.GetComponent<MeshRenderer>().SetPropertyBlock(matProp);
    }

    public static void SetMaterialColor(GameObject go, Color value)
    {
        MaterialPropertyBlock matProp = new MaterialPropertyBlock();
        matProp.SetColor("_Color", value); //NON HDRP
        //matProp.SetColor("_BaseColor", value); //HDRP
        go?.GetComponent<MeshRenderer>()?.SetPropertyBlock(matProp);
    }

    public static float GetMaterialValue(GameObject go, string value)
    {
        return go.GetComponent<MeshRenderer>().material.GetFloat(value);
    }

    public static float TriangleFonction(float time, float period = 2, float amplitude = 1, float offset = 0)
    {
        return Mathf.Abs((((time / (period / 2)) + (offset + 1)) % (amplitude * 2)) - amplitude);
    }

    public static float NegativeInclusionTriangleFonction(float time, float period = 2, float amplitude = 1, float offset = 0)
    {
        return (((time / (period / 2)) + (offset + 1)) % (amplitude * 2)) - amplitude;
    }

    public static Vector2 xz(this Vector3 vv)
    {
        return new Vector2(vv.x, vv.z);
    }

    public static float FlatDistanceTo(this Vector3 from, Vector3 to)
    {
        Vector2 a = from.xz();
        Vector2 b = to.xz();

        return Vector2.Distance(a, b);
    }

    public static Vector3 Round(this Vector3 vector3, int decimalPlaces = 2)
    {
        float multiplier = 1;
        for (int i = 0; i < decimalPlaces; i++)
        {
            multiplier *= 10f;
        }
        return new Vector3(
            Mathf.Round(vector3.x * multiplier) / multiplier,
            Mathf.Round(vector3.y * multiplier) / multiplier,
            Mathf.Round(vector3.z * multiplier) / multiplier);
    }

    public static Vector3Int ToIntVector(this Vector3 vec)
    {
        return new Vector3Int((int)vec.x, (int)vec.y, (int)vec.z);
    }
}
