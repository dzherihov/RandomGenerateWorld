using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateLevel : MonoBehaviour {
    private string str;
    public GameObject room, noRoom;
    public Transform roomsParent;
    // Use this for initialization
    void Start () {
        BuildRooms();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void BuildRooms() {
        int a = 0;
        Vector3 worldVec = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0));
        for (int i = 0; i < 7; i++) {
            for (int j = 0; j < 7; j++)
            {
                str = ArrayRooms.finalArr[a];
                CreatCell(str, worldVec, i, j);
                a++;
            }
        }
    }

    void CreatCell(string str, Vector3 wV, int x, int y)
    {
        GameObject tempCell;
        Debug.Log(str);
        if (str == "000000000")
        {
            tempCell = Instantiate(noRoom);
        }
        else
        {
            tempCell = Instantiate(room);
        }
        tempCell.transform.SetParent(roomsParent, false);
        float sprSizeX = tempCell.GetComponent<SpriteRenderer>().bounds.size.x;
        float sprSizeY = tempCell.GetComponent<SpriteRenderer>().bounds.size.y;
        tempCell.transform.position = new Vector3(wV.x - (sprSizeX * x), wV.y + (sprSizeY * y));
    }
}
