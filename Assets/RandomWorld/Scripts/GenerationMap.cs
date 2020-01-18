using UnityEngine;

namespace Project
{
    public class GenerationMap : MonoBehaviour
{
    public static int[,] myArr;
    
    public static int maxRoom = 1;
    public int maxRoomInspector;
    public static int maxLength ;
   // public int maxLengthInspector;
    private string str;
    private int randomDoor, randomRoom;

    void Awake()
    {
        maxRoom = maxRoomInspector;
        maxLength =( maxRoom*3+1)*2;
        myArr = new int[maxLength, maxLength];
        One(maxLength/2+1, maxLength/2, maxLength/2, maxLength/2, 1, 1);
        One(maxLength/2-1, maxLength/2, maxLength/2, maxLength/2, 1, 1);
        One(maxLength/2, maxLength/2+1, maxLength/2, maxLength/2, 1, 1);
        One(maxLength/2, maxLength/2-1, maxLength, maxLength, 1, 1);
        for (int i = 0; i < maxLength; i++)
        {
            for (int j = 0; j < maxLength; j++)
            {
                str = str + (myArr[i, j].ToString());
            }
            str = str + "\n";
        }
        Debug.Log(str);
    }

    public void One(int xNew, int yNew, int xOld, int yOld, int door, int room)
    {
        myArr[xNew, yNew] = 1;
        if (Mathf.Abs(yNew - yOld) != 0)
        {
            myArr[xNew - 1, yNew] = 0;
            myArr[xNew + 1, yNew] = 0;
        }
        else
        {
            myArr[xNew, yNew - 1] = 0;
            myArr[xNew, yNew + 1] = 0;
        }

        if (door == 1)
        {
            if (Mathf.Abs(yNew - yOld) != 0)
            {
                if (yNew > yOld)
                {
                    yOld = yNew;
                    yNew++;
                }
                else
                {
                    yOld = yNew;
                    yNew--;
                }
            }
            else
            {
                if (xNew > xOld)
                {
                    xOld = xNew;
                    xNew++;
                }
                else
                {
                    xOld = xNew;
                    xNew--;
                }
            }
            door--;
            One(xNew, yNew, xOld, yOld, door, room);
            return;
        }
        else
        {
            if (Mathf.Abs(yNew - yOld) != 0)
            {
                if (yNew > yOld)
                {
                    yOld = yNew;
                    yNew++;
                }
                else
                {
                    yOld = yNew;
                    yNew--;
                }
            }
            else
            {
                if (xNew > xOld)
                {
                    xOld = xNew;
                    xNew++;
                }
                else
                {
                    xOld = xNew;
                    xNew--;
                }
            }
            Zero(xNew, yNew, xOld, yOld, room);
            return;
        }
    }

    public void Zero(int xNew, int yNew, int xOld, int yOld, int room)
    {
        randomRoom = Random.Range(1, maxRoom);
        if (room < randomRoom)
        {
            if (Mathf.Abs(yNew - yOld) != 0)
            {
                if (yNew > yOld)
                {
                    room++;
                    randomDoor = Random.Range(0, 2);
                    if (myArr[xNew, yNew + 1] != 1 && randomDoor == 1)
                        One(xNew, yNew + 1, xOld, yOld + 1, 1, room);
                    randomDoor = Random.Range(0, 2);
                    if (myArr[xNew + 1, yNew] != 1 && randomDoor == 1)
                        One(xNew + 1, yNew, xOld, yOld + 1, 1, room);
                    randomDoor = Random.Range(0, 2);
                    if (myArr[xNew - 1, yNew] != 1 && randomDoor==1)
                        One(xNew - 1, yNew, xOld, yOld + 1, 1, room);
                    return;
                }
                else
                {
                    room++;
                    randomDoor = Random.Range(0, 2);
                    if (myArr[xNew, yNew - 1] != 1 && randomDoor == 1)
                        One(xNew, yNew - 1, xOld, yOld - 1, 1, room);
                    randomDoor = Random.Range(0, 2);
                    if (myArr[xNew + 1, yNew] != 1 && randomDoor == 1)
                        One(xNew + 1, yNew, xOld, yOld - 1, 1, room);
                    randomDoor = Random.Range(0, 2);
                    if (myArr[xNew - 1, yNew] != 1 && randomDoor==1)
                        One(xNew - 1, yNew, xOld, yOld - 1, 1, room);
                    return;
                }
            }
            else
            {
                if (xNew > xOld)
                {
                    room++;
                    randomDoor = Random.Range(0, 2);
                    if (myArr[xNew + 1, yNew] != 1 && randomDoor ==1)
                        One(xNew + 1, yNew, xOld + 1, yOld, 1, room);
                    randomDoor = Random.Range(0, 2);
                    if (myArr[xNew, yNew + 1] != 1 && randomDoor==1)
                        One(xNew, yNew + 1, xOld + 1, yOld, 1, room);
                    randomDoor = Random.Range(0, 2);
                    if (myArr[xNew, yNew - 1] != 1 && randomDoor ==1)
                        One(xNew, yNew - 1, xOld + 1, yOld, 1, room);
                    return;
                }
                else
                {
                    room++;
                    randomDoor = Random.Range(0, 2);
                    if (myArr[xNew - 1, yNew] != 1 && randomDoor==1)
                        One(xNew - 1, yNew, xOld - 1, yOld, 1, room);
                    randomDoor = Random.Range(0, 2);
                    if (myArr[xNew, yNew + 1] != 1 && randomDoor ==1)
                        One(xNew, yNew + 1, xOld - 1, yOld, 1, room);
                    randomDoor = Random.Range(0, 2);
                    if (myArr[xNew, yNew - 1] != 1 && randomDoor ==1)
                        One(xNew, yNew - 1, xOld - 1, yOld, 1, room);
                    return;
                }
            }
        }
    }
}}