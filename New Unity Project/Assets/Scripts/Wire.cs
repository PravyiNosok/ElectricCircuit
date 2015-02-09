 using UnityEngine;
using System.Collections;

public class Wire : Element {
    [SerializeField] private float width = 60;
    [SerializeField] private Mesh mesh;
    [SerializeField] private MeshFilter meshFilter;
    [SerializeField] private Material mat;
	private Vector3[] points = new Vector3[4];
    private float currentOffset = 0;
    public Color color1 = Color.red;
    public Color color2 = Color.grey;
    private int lengthAccuracy = 25;
	public void Init(Node nodos) {
        mesh = new Mesh();
        meshFilter = gameObject.GetComponent<MeshFilter>();
        meshFilter.mesh = mesh;
        mat = (Material) Instantiate(mat);
        //mat.color = color1;
        if (ViewManager.Instance.CurrentStyle == VisualStyle.schematic)
        {
            mat.color = color2;
        }
        else
        {
            mat.color = color1;
        }
        gameObject.GetComponent<MeshRenderer>().material = mat;
        contacts = new Node[2];
        contacts[0] = nodos;
        nodos.AddWire(this);
        Vector3[] vert = new Vector3[lengthAccuracy * 2 + 2];
        Vector3[] normals = new Vector3[vert.Length];
        Vector2[] uv = new Vector2[vert.Length];
        for (int i = 0; i < lengthAccuracy + 1; i++)
        {
            uv[2 * i] = Vector2.up * (i);
            normals[2 * i] = Vector3.forward;
            normals[2 * i + 1] = Vector3.forward;
            uv[2 * i + 1] = new Vector2(1, (i));
        }
        int[] tri = new int[lengthAccuracy * 6];
        for (int i = 0; i < lengthAccuracy; i++)
        {
            tri[6 * i] = 2 * i;
            tri[6 * i + 1] = 2 * i + 1;
            tri[6 * i + 2] = 2 * i + 3;
            tri[6 * i + 3] = 2 * i;
            tri[6 * i + 4] = 2 * i + 3;
            tri[6 * i + 5] = 2 * i + 2;
        }
        width = width/1920;
        mesh.vertices = vert;
        mesh.uv = uv;
        mesh.triangles = tri;
        mesh.normals = normals;
    }
    public override void ConnectNode(Node no)
    {
        if (no == null || no == contacts[0] || no.owner == contacts[0].owner) { Destroy(gameObject); return; }
        contacts[1] = no;
        no.AddWire(this);
        Move(Vector3.zero);
    }
    public void OnDestroy() {
        if (contacts[0] != null)
            contacts[0].Remove(this);
        if (contacts[1] != null)
            contacts[1].Remove(this);
    }
    public override void Show()
    {
        currentOffset+=current * Time.deltaTime;
        mat.mainTextureOffset = Vector2.up * currentOffset;
    }
    public override void ChangeIcon(string atlasName)
    {

        if (ViewManager.Instance.CurrentStyle == VisualStyle.schematic)
        {
            mat.color = color2;
        }
        else
        {
            mat.color = color1;
        }
		Move (Vector3.zero);
    }
    //--------------------------------//--------------------------------//--------------------------------
 
    public void CalculatePoints (Vector3 position) {
        points[0] = contacts[0].transform.position;
        //--------------------------------
        points[1] = contacts[0].transform.TransformPoint(Vector3.up * ElementValue);
            if (contacts[1] == null)
            {
            points[2] = position - 0.1f * (position - points[0]);
            points[3] = position;
            }else {
            points[2] = contacts[1].transform.TransformPoint(Vector3.up * ElementValue);
            points[3] = contacts[1].transform.position;
        }
        //-------------------------------
        	
 
        if (ViewManager.Instance.CurrentStyle == VisualStyle.schematic){
            Vector3 difference = points [3] - points [0];
            if (Vector3.Angle(difference.x * Vector3.right, contacts[0].transform.up) > 100 || (contacts[1] != null && Vector3.Angle(-difference.y * Vector3.up, contacts[1].transform.up) > 100))
            {
                points[1] = (points[0] + difference.y*(1 + 0.2f*Mathf.Abs(difference.x)) * Vector3.up);
                points[2] = (points[3] - difference.x*(1 + 0.2f * Mathf.Abs(difference.y)) * Vector3.right);
            }
            else {
                points[1] = (points[0] + difference.x*(1 + 0.2f * Mathf.Abs(difference.y)) * Vector3.right);
                points[2] = (points[3] - difference.y*(1 + 0.2f * Mathf.Abs(difference.x)) * Vector3.up);
            }
        }
    }


	public override void Move (Vector3 position){
        if (contacts == null || contacts.Length<2|| contacts[0] == null)
			return;
       CalculatePoints(position);
        Vector3[] vert = mesh.vertices;
        Quaternion rot = Quaternion.Euler(0, 0, 90);
        points[0] += rot*((points[0] - points[1]).normalized * 0.5f * width);
        points[3] += rot*((points[2] - points[3]).normalized * 0.5f * width);
        int i = 0;
        for (i = 0; i < lengthAccuracy+1; i++) {
            float am = 1.0f * i / lengthAccuracy;
            vert[2 * i] = EvaluatePoint(am);
        }
         for (i = 0; i < lengthAccuracy; i++)
        {
            vert[2 * i + 1] = vert[2 * i] + rot*(vert[2*i+2]-vert[2 * i]).normalized*width;

        }
        vert[2 * i + 1] = vert[2 * i] + rot * (vert[2 * i] - vert[2 * i - 2]).normalized * width;
        mesh.vertices = vert;
        mesh.RecalculateBounds();
	}
    public void Update() {
        Show();
    }
	private Vector3 EvaluatePoint(float t){
		float t1 = 1 - t;
		return t1 * t1 * t1 * points[0] + 3 * t * t1 * t1 * points[1] +	3 * t * t * t1 * points[2] + t * t * t * points[3];
	}
	
}
	