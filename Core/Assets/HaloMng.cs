using System;
using UnityEditor;
using UnityEngine;

public class HaloMng : MonoBehaviour
{
    public float MaxSize = 1;
    public float MinSize = 0.1f;

    public float BlinkScale = 2;

    public float Speed = 0.01f;
    private SerializedObject Halo;
    private SerializedProperty Size;

    private float CurSize;

    private bool Ok;

    private bool Blink;

    void Start()
    {
        Halo = new SerializedObject(GetComponent("Halo"));
        Ok = Halo != null;
        if (Ok)
            Size = Halo.FindProperty("m_Size");
        else
            Debug.LogError(this.name + "cant find Halo !");
    }

    void Update()
    {
        CurSize += UnityEngine.Random.Range(-Speed, Speed);
        if(Blink)
            CurSize = Math.Clamp(CurSize, MinSize* BlinkScale, MaxSize* BlinkScale);
        else
            CurSize = Math.Clamp(CurSize, MinSize, MaxSize);
        Size.floatValue = CurSize;
        Halo.ApplyModifiedProperties();
    }

    public void SetBlink(bool blink)
    {
        Blink = blink;
    }
}
