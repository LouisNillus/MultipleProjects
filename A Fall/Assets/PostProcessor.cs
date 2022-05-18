using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.Rendering.PostProcessing;
using DG.Tweening;

[RequireComponent(typeof(Camera))]
public class PostProcessor : MonoBehaviour
{
    Camera cam;
    Bloom bloomLayer = null;
    AmbientOcclusion ambientOcclusionLayer = null;
    ColorGrading colorGradingLayer = null;
    Vignette vignette = null;
    ChromaticAberration chromaticAberration = null;

    PostProcessVolume volume;

    PixelPerfectCamera pixelPerfectCamera;

    public Transform trans;
    private void Awake()
    {
        cam = this.GetComponent<Camera>();
        volume = this.GetComponent<PostProcessVolume>();
        pixelPerfectCamera = this.GetComponent<PixelPerfectCamera>();

        volume.profile.TryGetSettings(out bloomLayer);
        volume.profile.TryGetSettings(out ambientOcclusionLayer);
        volume.profile.TryGetSettings(out colorGradingLayer);
        volume.profile.TryGetSettings(out chromaticAberration);
        volume.profile.TryGetSettings(out vignette);

    }
    // Start is called before the first frame update
    void Start()
    {
        //TweenBloom();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) FocusCamera(trans, 2f, true);
    }

    public void TweenBloom(float _intensity, float _duration, Ease _ease = Ease.Linear)
    {
        float f;
        DOTween.To(() => bloomLayer.intensity.value, x => bloomLayer.intensity.value = x, _intensity, _duration).SetEase(_ease);

    }

    public void TweenChromaticAbberation(float _intensity, float _duration, Ease _ease = Ease.Linear)
    {
        DOTween.To(() => chromaticAberration.intensity.value, x => chromaticAberration.intensity.value = x, _intensity, _duration).SetEase(_ease);

    }

    public int vignettePulseCount = 0;
    public void TweenVignette(float _intensity, float _duration, Color color = default, bool instantColor = false, int pulseCount = 0, Ease _ease = Ease.Linear)
    {
        float lastIntensity = vignette.intensity.value;
        Color lastColor = vignette.color.value;


        if(vignettePulseCount >= (pulseCount*2))
        {
            vignettePulseCount = 0;
            return;
        }

        if (instantColor) vignette.color.value = color;
        else
        {
            DOTween.To(() => vignette.color.value, x => vignette.color.value = x, color, _duration);
        }

        DOTween.To(() => vignette.intensity.value, x => vignette.intensity.value = x, _intensity, _duration).OnComplete(() => TweenVignette(lastIntensity,  _duration, lastColor, instantColor, pulseCount, _ease));

        vignettePulseCount++;
    }

    public void FieldOfView(float _value, float _duration, Ease _ease = Ease.InOutCubic)
    {
        DOTween.To(() => cam.fieldOfView, x => cam.fieldOfView = x, _value, _duration).SetEase(_ease);
    }

    public void LockCamera()
    {
        cam.transform.parent = null;
    }

    public void FocusCamera(Transform t, float duration, bool setToParent)
    {
        cam.transform.DOMoveX(t.position.x, duration);
        cam.transform.DOMoveY(t.position.y, duration);

        cam.transform.parent = setToParent ? t : null;
    }

}
