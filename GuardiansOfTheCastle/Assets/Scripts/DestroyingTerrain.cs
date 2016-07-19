using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DestroyingTerrain : MonoBehaviour {


	private float destroyed_time;
	private Mesh original_mesh;
	private MeshCollider original_meshCollider;
	private bool isdestroyed = false;
	public GameObject DestroyingTerrainEffect;
	private Vector3 effectTempPosition;

	



	void OnTriggerEnter(Collider other)
	{ 
		if (Time.time >= destroyed_time + 1 || destroyed_time == 99999) {
						if (this.tag == "terrain" && other.tag == "bullet_bomb2") {
				DestroyOnContact.Explosion_sound = true;
				Destroy (other.gameObject);

						}
				}



		if (Time.time >= destroyed_time + 10 || destroyed_time == 99999) {
			if (this.tag == "terrain" && other.tag == "bullet_bomb2") {

				if(DestroyingTerrainEffect != null)
				{
				effectTempPosition = other.transform.position;
				effectTempPosition.y = effectTempPosition.y - 10;
				Quaternion rot = Quaternion.identity;			
				Instantiate(DestroyingTerrainEffect,effectTempPosition, rot);				
				}


				destroyed_time = Time.time;
				isdestroyed = true;

		
				var newVerts = new List<Vector3> ();
				Mesh mesh;
				MeshCollider meshCollider;
			
			
				mesh = this.GetComponent<MeshFilter> ().mesh;				
				meshCollider = this.GetComponent<MeshCollider> ();
			
				var vertices = new Vector3[mesh.vertices.Length];	
				vertices = mesh.vertices;
			
				for (var i=0; i<102; i++) {
						vertices [i].y = vertices [i].y - 100;					
				}
		
			
				mesh.vertices = vertices;
				meshCollider.sharedMesh = mesh;

			
			
				mesh.RecalculateNormals ();
				mesh.RecalculateBounds ();
			

			}
		}


	}
	// Use this for initialization
	void Start () {	

		original_mesh = this.GetComponent<MeshFilter> ().mesh;	
		original_meshCollider = this.GetComponent<MeshCollider> ();
		destroyed_time = 99999;
		/*
		Vector3 newScale1 = new Vector3(1, 1, 1);
		Vector3 newScale2 = new Vector3(1, 1, 1);
		Vector3 newScale3 = new Vector3(1, 1, 1);
		
		var newVerts = new List<Vector3>();
		Mesh mesh;
		MeshCollider meshCollider;
	

		mesh = this.GetComponent<MeshFilter>().mesh;
		meshCollider = this.GetComponent<MeshCollider> ();

		var vertices = new Vector3[mesh.vertices.Length];	
		vertices = mesh.vertices;
		
	
		

		for (var i=19; i<40; i++) {
			vertices[i].y = vertices[i].y - (i-20);	
			//vertices[i] = newScale1;
		}
		
		for (var i=41; i<60; i++) {
			vertices[i].y = vertices[i].y - (60-i);	
			//vertices[i] = newScale1;
		}		
		mesh.vertices = vertices;
		meshCollider.sharedMesh = mesh; 
	
		
		for (var i=0; i<mesh.vertices.Length; i++) {
			//print("Vnumber : " + i + "X-axis" + mesh.vertices[i].y);
			
		}
		*/
	}
	
	// Update is called once per frame
	void Update () {

	
		if (Time.time >= destroyed_time + 3 && Time.time <= destroyed_time + 7  && destroyed_time != 99999 && isdestroyed == true) {
			isdestroyed = false;

			var newVerts = new List<Vector3> ();
			Mesh mesh;
			MeshCollider meshCollider;
			
			
			mesh = this.GetComponent<MeshFilter> ().mesh;				
			meshCollider = this.GetComponent<MeshCollider> ();

			var vertices = new Vector3[mesh.vertices.Length];	
			vertices = mesh.vertices;

			for (var i=0; i<102; i++) {
				vertices [i].y = vertices [i].y +100;	
			}
		
			mesh.vertices = vertices;
			meshCollider.sharedMesh = mesh;
		
			meshCollider.smoothSphereCollisions = false;
			meshCollider.smoothSphereCollisions = true;

			mesh.RecalculateNormals ();
			mesh.RecalculateBounds ();

			
		}
	}
}
