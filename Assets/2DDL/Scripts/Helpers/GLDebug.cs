
#if UNITY_EDITOR

using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using UnityEditor;



public class GLDebug : MonoBehaviour
{
	private struct Line
	{
		public Vector3 start;
		public Vector3 end;
		public Color color;
		public float startTime;
		public float duration;
		
		public Line (Vector3 start, Vector3 end, Color color, float startTime, float duration)
		{
			this.start = start;
			this.end = end;
			this.color = color;
			this.startTime = startTime;
			this.duration = duration;
		}
		
		public bool DurationElapsed (bool drawLine)
		{
			if (drawLine)
			{
				GL.Color (color);
				GL.Vertex (start);
				GL.Vertex (end);
			}
			return Time.time - startTime >= duration;
		}
	}
	
	private static GLDebug instance;
	private static Material matZOn;
	private static Material matZOff;
	
	public KeyCode toggleKey;
	public bool displayLines = true;
	#if UNITY_EDITOR
	public bool displayGizmos = true;
	#endif
	//public ScreenRect rect = new ScreenRect (0, 0, 150, 20);
	
	private List<Line> linesZOn;
	private List<Line> linesZOff;
	private List<Vector3> vertexes;
	int lStroke = 1;
	//private float milliseconds = 0f;

	
	void Awake ()
	{
		if (instance)
		{
			DestroyImmediate (this);
			return;
		}
		instance = this;
		SetMaterial ();
		linesZOn = new List<Line> ();
		linesZOff = new List<Line> ();
		vertexes = new List<Vector3>();
	}
	
	void SetMaterial ()
	{
		matZOn = AssetDatabase.LoadAssetAtPath ("Assets/2DDL/Materials/GLDebugLinesMaterial.mat", typeof(Material)) as Material;
		matZOn.hideFlags = HideFlags.HideAndDontSave;
		matZOn.shader.hideFlags = HideFlags.HideAndDontSave;

		matZOff = AssetDatabase.LoadAssetAtPath ("Assets/2DDL/Materials/GLDebugLinesMaterial.mat", typeof(Material)) as Material;
		matZOff.hideFlags = HideFlags.HideAndDontSave;
		matZOff.shader.hideFlags = HideFlags.HideAndDontSave;

			/*new Material (
			@"Shader ""GLlineZOn"" {
        SubShader {
                Pass {
                        Blend SrcAlpha OneMinusSrcAlpha
                        ZWrite Off
                        Cull Off
                        BindChannels {
                                Bind ""vertex"", vertex
                                Bind ""color"", color
                        }
                }
        }
}
");


		matZOn.hideFlags = HideFlags.HideAndDontSave;
		matZOn.shader.hideFlags = HideFlags.HideAndDontSave;
		matZOff = new Material (
			@"Shader ""GLlineZOff"" {
        SubShader {
                Pass {
                        Blend SrcAlpha OneMinusSrcAlpha
                        ZWrite Off
                        ZTest Always
                        Cull Off
                        BindChannels {
                                Bind ""vertex"", vertex
                                Bind ""color"", color
                        }
                }
        }
}
");

*/

	}
	
	void Update ()
	{
		if (Input.GetKeyDown (toggleKey))
			displayLines = !displayLines;
		
		if (!displayLines)
		{
			Stopwatch timer = Stopwatch.StartNew ();
			
			linesZOn = linesZOn.Where (l => !l.DurationElapsed (false)).ToList ();
			linesZOff = linesZOff.Where (l => !l.DurationElapsed (false)).ToList ();
			
			timer.Stop ();
			//milliseconds = timer.Elapsed.Ticks / 10000f;
		}
	}
	
	/*void OnGUI ()
        {
                GUI.Label (rect, "GLDebug : " + milliseconds.ToString ("f") + " ms");
        }*/
	
	#if UNITY_EDITOR
	void OnDrawGizmos ()
	{
		if (!displayGizmos || !Application.isPlaying)
			return;
		for (int i = 0; i < linesZOn.Count; i++)
		{
			Gizmos.color = linesZOn[i].color;
			Gizmos.DrawLine (linesZOn[i].start, linesZOn[i].end);
		}
		for (int i = 0; i < linesZOff.Count; i++)
		{
			Gizmos.color = linesZOff[i].color;
			Gizmos.DrawLine (linesZOff[i].start, linesZOff[i].end);
		}
	}
	#endif
	
	void OnPostRender ()
	{
		if (!displayLines) return;
		
		Stopwatch timer = Stopwatch.StartNew ();
		
		matZOn.SetPass (0);
		GL.Begin (GL.LINES);
		linesZOn = linesZOn.Where (l => !l.DurationElapsed (true)).ToList ();
		GL.End ();
		
		matZOff.SetPass (0);
		GL.Begin (GL.LINES);
		linesZOff = linesZOff.Where (l => !l.DurationElapsed (true)).ToList ();
		GL.End ();

		if(lStroke > 1){
			matZOff.SetPass (0);
			GL.Begin(GL.QUADS);
			for(int i=0 ; i< vertexes.Count; i++){
				GL.Vertex(vertexes[i]);
			}
			lStroke = 1;
			GL.End();//

		}
		
		timer.Stop ();
		//milliseconds = timer.Elapsed.Ticks / 10000f;
	}
	
	private static void DrawLine (Vector3 start, Vector3 end, Color color, float duration = 0, bool depthTest = false)
	{
		if (duration == 0 && !instance.displayLines)
			return;
		if (start == end)
			return;
		if (depthTest)
			instance.linesZOn.Add (new Line (start, end, color, Time.time, duration));
		else
			instance.linesZOff.Add (new Line (start, end, color, Time.time, duration));
	}
	
	/// <summary>
	/// Draw a line from start to end with color for a duration of time and with or without depth testing.
	/// If duration is 0 then the line is rendered 1 frame.
	/// </summary>
	/// <param name="start">Point in world space where the line should start.</param>
	/// <param name="end">Point in world space where the line should end.</param>
	/// <param name="stroke">Width of line</param>
	/// <param name="color">Color of the line.</param>
	/// <param name="duration">How long the line should be visible for.</param>
	/// <param name="depthTest">Should the line be obscured by objects closer to the camera ?</param>
	public static void DrawLine (Vector3 start, Vector3 end, int stroke, Color? color = null, float duration = 0, bool depthTest = false)
	{
		float nearClip = Camera.main.nearClipPlane + 0.00001f;
		float thisWidth = 1f/Screen.width * stroke * 0.5f;

		if (stroke == 1)
		{
			DrawLine (start, end, color ?? Color.white, duration, depthTest);
			instance.lStroke = 1;
		}
		else
		{

			Vector3 perpendicular = (new Vector3(end.y, start.x, nearClip) -
			                         new Vector3(start.y, end.x, nearClip)).normalized * thisWidth;
			Vector3 v1 = new Vector3(start.x, start.y, nearClip);
			Vector3 v2 = new Vector3(end.x, end.y, nearClip);

			instance.vertexes.Add((v1 - perpendicular));
			instance.vertexes.Add((v1 + perpendicular));
			instance.vertexes.Add((v2 + perpendicular));
			instance.vertexes.Add((v1 - perpendicular));
			instance.lStroke = stroke;
		}

	}

	/// <summary>
	/// Draw a line from start to end with color for a duration of time and with or without depth testing.
	/// If duration is 0 then the line is rendered 1 frame.
	/// </summary>
	/// <param name="start">Point in world space where the line should start.</param>
	/// <param name="end">Point in world space where the line should end.</param>
	/// <param name="color">Color of the line.</param>
	/// <param name="duration">How long the line should be visible for.</param>
	/// <param name="depthTest">Should the line be obscured by objects closer to the camera ?</param>
	public static void DrawLine (Vector3 start, Vector3 end, Color? color = null, float duration = 0, bool depthTest = false)
	{
		DrawLine (start, end, color ?? Color.white, duration, depthTest);
	}

	
	/// <summary>
	/// Draw a line from start to start + dir with color for a duration of time and with or without depth testing.
	/// If duration is 0 then the ray is rendered 1 frame.
	/// </summary>
	/// <param name="start">Point in world space where the ray should start.</param>
	/// <param name="dir">Direction and length of the ray.</param>
	/// <param name="color">Color of the ray.</param>
	/// <param name="duration">How long the ray should be visible for.</param>
	/// <param name="depthTest">Should the ray be obscured by objects closer to the camera ?</param>
	public static void DrawRay (Vector3 start, Vector3 dir, Color? color = null, float duration = 0, bool depthTest = false)
	{
		if (dir == Vector3.zero)
			return;
		DrawLine (start, start + dir, color, duration, depthTest);
	}


	public static void DrawCircle (Vector3 start, float radius, int segments, Color? color = null, float duration = 0, bool depthTest = false){
		if (segments < 3)
			return;

		//--step is the angle of forward --//
		float step = (2 *Mathf.PI) / segments;

		
		float TangencialFactor = Mathf.Tan (step);
		float radialFactor = Mathf.Cos (step);
		
		float x = radius ;
		float y = 0;

		float lastX = x, lastY = y;

		

		//--stable cicle --//
		for(int i=1; i<(segments+1); i++){

			lastX = x;
			lastY = y;

			float tx = -y;
			float ty = x;
			
			x += tx * TangencialFactor;
			y += ty * TangencialFactor;
			
			
			x *= radialFactor;
			y *= radialFactor;
				
			DrawLine (new Vector2(lastX +start.x, lastY+start.y), new Vector2(x+start.x,y+start.y), color ?? Color.white, duration, depthTest);

		}

		
		
	}
	
	/// <summary>
	/// Draw an arrow from start to end with color for a duration of time and with or without depth testing.
	/// If duration is 0 then the arrow is rendered 1 frame.
	/// </summary>
	/// <param name="start">Point in world space where the arrow should start.</param>
	/// <param name="end">Point in world space where the arrow should end.</param>
	/// <param name="arrowHeadLength">Length of the 2 lines of the head.</param>
	/// <param name="arrowHeadAngle">Angle between the main line and each of the 2 smaller lines of the head.</param>
	/// <param name="color">Color of the arrow.</param>
	/// <param name="duration">How long the arrow should be visible for.</param>
	/// <param name="depthTest">Should the arrow be obscured by objects closer to the camera ?</param>
	public static void DrawLineArrow (Vector3 start, Vector3 end, float arrowHeadLength = 0.25f, float arrowHeadAngle = 20, Color? color = null, float duration = 0, bool depthTest = false)
	{
		DrawArrow (start, end - start, arrowHeadLength, arrowHeadAngle, color, duration, depthTest);
	}
	
	/// <summary>
	/// Draw an arrow from start to start + dir with color for a duration of time and with or without depth testing.
	/// If duration is 0 then the arrow is rendered 1 frame.
	/// </summary>
	/// <param name="start">Point in world space where the arrow should start.</param>
	/// <param name="dir">Direction and length of the arrow.</param>
	/// <param name="arrowHeadLength">Length of the 2 lines of the head.</param>
	/// <param name="arrowHeadAngle">Angle between the main line and each of the 2 smaller lines of the head.</param>
	/// <param name="color">Color of the arrow.</param>
	/// <param name="duration">How long the arrow should be visible for.</param>
	/// <param name="depthTest">Should the arrow be obscured by objects closer to the camera ?</param>
	public static void DrawArrow (Vector3 start, Vector3 dir, float arrowHeadLength = 0.25f, float arrowHeadAngle = 20, Color? color = null, float duration = 0, bool depthTest = false)
	{
		if (dir == Vector3.zero)
			return;
		DrawRay (start, dir, color, duration, depthTest);
		Vector3 right = Quaternion.LookRotation (dir) * Quaternion.Euler (0, 180 + arrowHeadAngle, 0) * Vector3.forward;
		Vector3 left  = Quaternion.LookRotation (dir) * Quaternion.Euler (0, 180 - arrowHeadAngle, 0) * Vector3.forward;
		DrawRay (start + dir, right * arrowHeadLength, color, duration, depthTest);
		DrawRay (start + dir, left  * arrowHeadLength, color, duration, depthTest);
	}
	
	/// <summary>
	/// Draw a square with color for a duration of time and with or without depth testing.
	/// If duration is 0 then the square is renderer 1 frame.
	/// </summary>
	/// <param name="pos">Center of the square in world space.</param>
	/// <param name="rot">Rotation of the square in euler angles in world space.</param>
	/// <param name="scale">Size of the square.</param>
	/// <param name="color">Color of the square.</param>
	/// <param name="duration">How long the square should be visible for.</param>
	/// <param name="depthTest">Should the square be obscured by objects closer to the camera ?</param>
	public static void DrawSquare (Vector3 pos, Vector3? rot = null, Vector3? scale = null, Color? color = null, float duration = 0, bool depthTest = false)
	{
		DrawSquare (Matrix4x4.TRS (pos, Quaternion.Euler (rot ?? Vector3.zero), scale ?? Vector3.one), color, duration, depthTest);
	}
	/// <summary>
	/// Draw a square with color for a duration of time and with or without depth testing.
	/// If duration is 0 then the square is renderer 1 frame.
	/// </summary>
	/// <param name="pos">Center of the square in world space.</param>
	/// <param name="rot">Rotation of the square in world space.</param>
	/// <param name="scale">Size of the square.</param>
	/// <param name="color">Color of the square.</param>
	/// <param name="duration">How long the square should be visible for.</param>
	/// <param name="depthTest">Should the square be obscured by objects closer to the camera ?</param>
	public static void DrawSquare (Vector3 pos, Quaternion? rot = null, Vector3? scale = null, Color? color = null, float duration = 0, bool depthTest = false)
	{
		DrawSquare (Matrix4x4.TRS (pos, rot ?? Quaternion.identity, scale ?? Vector3.one), color, duration, depthTest);
	}
	/// <summary>
	/// Draw a square with color for a duration of time and with or without depth testing.
	/// If duration is 0 then the square is renderer 1 frame.
	/// </summary>
	/// <param name="matrix">Transformation matrix which represent the square transform.</param>
	/// <param name="color">Color of the square.</param>
	/// <param name="duration">How long the square should be visible for.</param>
	/// <param name="depthTest">Should the square be obscured by objects closer to the camera ?</param>
	public static void DrawSquare (Matrix4x4 matrix, Color? color = null, float duration = 0, bool depthTest = false)
	{
		Vector3
			p_1     = matrix.MultiplyPoint3x4 (new Vector3 ( .5f, 0,  .5f)),
			p_2     = matrix.MultiplyPoint3x4 (new Vector3 ( .5f, 0, -.5f)),
			p_3     = matrix.MultiplyPoint3x4 (new Vector3 (-.5f, 0, -.5f)),
			p_4     = matrix.MultiplyPoint3x4 (new Vector3 (-.5f, 0,  .5f));
		
		DrawLine (p_1, p_2, color, duration, depthTest);
		DrawLine (p_2, p_3, color, duration, depthTest);
		DrawLine (p_3, p_4, color, duration, depthTest);
		DrawLine (p_4, p_1, color, duration, depthTest);
	}
	
	/// <summary>
	/// Draw a cube with color for a duration of time and with or without depth testing.
	/// If duration is 0 then the square is renderer 1 frame.
	/// </summary>
	/// <param name="pos">Center of the cube in world space.</param>
	/// <param name="rot">Rotation of the cube in euler angles in world space.</param>
	/// <param name="scale">Size of the cube.</param>
	/// <param name="color">Color of the cube.</param>
	/// <param name="duration">How long the cube should be visible for.</param>
	/// <param name="depthTest">Should the cube be obscured by objects closer to the camera ?</param>
	public static void DrawCube (Vector3 pos, Vector3? rot = null, Vector3? scale = null, Color? color = null, float duration = 0, bool depthTest = false)
	{
		DrawCube (Matrix4x4.TRS (pos, Quaternion.Euler (rot ?? Vector3.zero), scale ?? Vector3.one), color, duration, depthTest);
	}
	/// <summary>
	/// Draw a cube with color for a duration of time and with or without depth testing.
	/// If duration is 0 then the square is renderer 1 frame.
	/// </summary>
	/// <param name="pos">Center of the cube in world space.</param>
	/// <param name="rot">Rotation of the cube in world space.</param>
	/// <param name="scale">Size of the cube.</param>
	/// <param name="color">Color of the cube.</param>
	/// <param name="duration">How long the cube should be visible for.</param>
	/// <param name="depthTest">Should the cube be obscured by objects closer to the camera ?</param>
	public static void DrawCube (Vector3 pos, Quaternion? rot = null, Vector3? scale = null, Color? color = null, float duration = 0, bool depthTest = false)
	{
		DrawCube (Matrix4x4.TRS (pos, rot ?? Quaternion.identity, scale ?? Vector3.one), color, duration, depthTest);
	}
	/// <summary>
	/// Draw a cube with color for a duration of time and with or without depth testing.
	/// If duration is 0 then the square is renderer 1 frame.
	/// </summary>
	/// <param name="matrix">Transformation matrix which represent the cube transform.</param>
	/// <param name="color">Color of the cube.</param>
	/// <param name="duration">How long the cube should be visible for.</param>
	/// <param name="depthTest">Should the cube be obscured by objects closer to the camera ?</param>
	public static void DrawCube (Matrix4x4 matrix, Color? color = null, float duration = 0, bool depthTest = false)
	{
		Vector3
			down_1  = matrix.MultiplyPoint3x4 (new Vector3 ( .5f, -.5f,  .5f)),
			down_2  = matrix.MultiplyPoint3x4 (new Vector3 ( .5f, -.5f, -.5f)),
			down_3  = matrix.MultiplyPoint3x4 (new Vector3 (-.5f, -.5f, -.5f)),
			down_4  = matrix.MultiplyPoint3x4 (new Vector3 (-.5f, -.5f,  .5f)),
			up_1    = matrix.MultiplyPoint3x4 (new Vector3 ( .5f,  .5f,  .5f)),
			up_2    = matrix.MultiplyPoint3x4 (new Vector3 ( .5f,  .5f, -.5f)),
			up_3    = matrix.MultiplyPoint3x4 (new Vector3 (-.5f,  .5f, -.5f)),
			up_4    = matrix.MultiplyPoint3x4 (new Vector3 (-.5f,  .5f,  .5f));
		
		DrawLine (down_1, down_2, color, duration, depthTest);
		DrawLine (down_2, down_3, color, duration, depthTest);
		DrawLine (down_3, down_4, color, duration, depthTest);
		DrawLine (down_4, down_1, color, duration, depthTest);
		
		DrawLine (down_1, up_1, color, duration, depthTest);
		DrawLine (down_2, up_2, color, duration, depthTest);
		DrawLine (down_3, up_3, color, duration, depthTest);
		DrawLine (down_4, up_4, color, duration, depthTest);
		
		DrawLine (up_1, up_2, color, duration, depthTest);
		DrawLine (up_2, up_3, color, duration, depthTest);
		DrawLine (up_3, up_4, color, duration, depthTest);
		DrawLine (up_4, up_1, color, duration, depthTest);
	}
}

#endif