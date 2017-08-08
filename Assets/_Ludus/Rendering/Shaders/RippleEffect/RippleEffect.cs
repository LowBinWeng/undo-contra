using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public enum DimensionSpace {World, Screen}

public class RippleEffect : MonoBehaviour
{
	private static RippleEffect _instance;
	public static RippleEffect Instance { get{return _instance;}}

    public AnimationCurve waveform = new AnimationCurve(
        new Keyframe(0.00f, 0.50f, 0, 0),
        new Keyframe(0.05f, 1.00f, 0, 0),
        new Keyframe(0.15f, 0.10f, 0, 0),
        new Keyframe(0.25f, 0.80f, 0, 0),
        new Keyframe(0.35f, 0.30f, 0, 0),
        new Keyframe(0.45f, 0.60f, 0, 0),
        new Keyframe(0.55f, 0.40f, 0, 0),
        new Keyframe(0.65f, 0.55f, 0, 0),
        new Keyframe(0.75f, 0.46f, 0, 0),
        new Keyframe(0.85f, 0.52f, 0, 0),
        new Keyframe(0.99f, 0.50f, 0, 0)
    );

	[Range(0.01f, 1.0f)]
    public float refractionStrength = 0.5f;

    public Color reflectionColor = Color.gray;

    [Range(0.01f, 1.0f)]
    public float reflectionStrength = 0.7f;

    [Range(1.0f, 3.0f)]
    public float waveSpeed = 1.25f;

	[Range(1, 3)]
	public int dropCount;
	int dropletIndex=0;
	public bool debugMode = false;


    [SerializeField, HideInInspector]
    Shader shader;

    class Droplet
    {
        Vector2 position;
        float time;

        public Droplet()
        {
            time = 1000;
        }

		public void Emit( Vector2 viewPortSpace) {
			position = viewPortSpace;
			time = 0;
		}

        public void Update()
        {
			if ( time < 3f ) time += Time.deltaTime;
        }

        public Vector4 MakeShaderParameter(float aspect)
        {
            return new Vector4(position.x * aspect, position.y, time, 0);
        }
    }


	List<Droplet> droplets;
    Texture2D gradTexture;
    Material material;

    void Awake()
    {
		if 		(_instance == null ) _instance = this;
		else if (_instance != this ) Destroy(gameObject);

		droplets = new List<Droplet>();	
		for ( int i = 0; i < dropCount; i++ ) droplets.Add( new Droplet() );

        gradTexture = new Texture2D(2048, 1, TextureFormat.Alpha8, false);
        gradTexture.wrapMode = TextureWrapMode.Clamp;
        gradTexture.filterMode = FilterMode.Bilinear;
        for (var i = 0; i < gradTexture.width; i++)
        {
            var x = 1.0f / gradTexture.width * i;
            var a = waveform.Evaluate(x);
            gradTexture.SetPixel(i, 0, new Color(a, a, a, a));
        }
        gradTexture.Apply();

        material = new Material(shader);
        material.hideFlags = HideFlags.DontSave;
        material.SetTexture("_GradTex", gradTexture);

        UpdateShaderParameters();
    }

	void UpdateShaderParameters()
	{
		var c = GetComponent<Camera>();

		for ( int i = 0; i < droplets.Count; i++ ) {
			material.SetVector("_Drop" + i.ToString(), droplets[i].MakeShaderParameter(c.aspect));
		}

		material.SetColor("_Reflection", reflectionColor);
		material.SetVector("_Params1", new Vector4(c.aspect, 1, 1 / waveSpeed, 0));
		material.SetVector("_Params2", new Vector4(1, 1 / c.aspect, refractionStrength, reflectionStrength));
	}

    void Update()
    {
		for ( int i = 0; i < droplets.Count; i++ ) droplets[i].Update();
        UpdateShaderParameters();

		if ( debugMode == true ) {
			if ( Input.GetMouseButtonDown(0)) {
				EmitRipple( Input.mousePosition, DimensionSpace.Screen );
//				if ( obj ) EmitRipple(obj.position, DimensionSpace.World);
			}
		}
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, material);
    }

//	public void EmitRipple( Vector2 screenPos) {
//		droplets[dropletIndex].Emit(Camera.main.ScreenToViewportPoint(screenPos));
//		dropletIndex = (dropletIndex + 1) % dropCount;
//	}

	public void EmitRipple( Vector3 pos, DimensionSpace dimensionSpace) {

		Vector2 viewPortSpace = Vector2.zero;

		switch ( dimensionSpace ) {
		case DimensionSpace.World	: viewPortSpace = Camera.main.WorldToViewportPoint(pos); break;
		case DimensionSpace.Screen	: viewPortSpace = Camera.main.ScreenToViewportPoint(pos); break;
		}

		droplets[dropletIndex].Emit( viewPortSpace );
		dropletIndex = (dropletIndex + 1) % dropCount;
	}
}
