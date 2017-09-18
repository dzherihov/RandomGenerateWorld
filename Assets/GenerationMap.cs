using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerationMap : MonoBehaviour
{
    public static int[,] myArr = new int[21, 21];
    private string str;
    private int randomDoor, randomRoom;

    void Awake()
    {
        One(11, 10, 10, 10, 1, 1);
        One(9, 10, 10, 10, 1, 1);
        One(10, 11, 10, 10, 1, 1);
        One(10, 9, 10, 10, 1, 1);
        for (int i = 0; i < 21; i++)
        {
            for (int j = 0; j < 21; j++)
            {
                str = str + (myArr[i, j].ToString());
            }
            str = str + "\n";
        }
        //Debug.Log(str);
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
        randomRoom = Random.Range(1, 4);
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
}