using UnityEngine;
using System.Collections;

public class AnimeateExample : MonoBehaviour
{
	public float animrate = 6f;
	public int tilesX = 3;
	public int tilesY = 3;
	
	void Update()
	{
		int totalTiles = tilesX*tilesY;
		int idx = (int)(Time.time*5)%(totalTiles);
		int x = idx%tilesX;
		int y = ((int)(((float)idx)/tilesX));
		y = tilesY - y - 1;
		
		renderer.material.SetTextureScale("_MainTex", new Vector2(1f/tilesX, 1f/tilesY));
		renderer.material.SetTextureOffset("_MainTex", new Vector2(((float)x)/tilesX, ((float)y)/tilesY));
		// Okay so it'll 
		//It's already in the editor -- let me show you
		
		//how do you mean?
		// where is the image coming from, there is no input for that in the method
	}
}