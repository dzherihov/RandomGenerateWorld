using UnityEngine;
using UnityEngine.SceneManagement;

namespace Project
{


    public class ArrayRooms : MonoBehaviour
    {
        private int[,] myArr = new int[3, 3];
        private string str;
        public static string[] finalArr ;
        private int q = 0;
        public GameObject[] room;
        public Transform roomsParent;

        void Start()
        {
            finalArr = new string[GenerationMap.maxLength * GenerationMap.maxLength];
            int j = 0;
            int x = 0;
            int y = 0;
            int c = 0;
            int p = 0;
            int a = 0;
            for (a = 0; a < GenerationMap.maxLength/3; a++)
            {
                p = 0;
                for (p = 0; p < GenerationMap.maxLength/3; p++)
                {
                    c = 0;
                    int i = 0;

                    for (i = a * 3; i < GenerationMap.maxLength+1; i++)
                    {
                        if ((i != a * 3) && (i % 3 == 0))
                        {
                            x = 0;
                            Read();
                            j = 0;
                            y = 0;
                            break;
                        }

                        c = j + p * 3;
                        for (j = c; j < GenerationMap.maxLength+1; j++)
                        {
                            if ((j != c) && (j % 3 == 0))
                            {
                                break;
                            }

                            myArr[x, y] = GenerationMap.myArr[i, j];
                            x++;
                        }

                        j = 0;
                        y++;
                        x = 0;
                    }
                }
            }

            BuildRooms();
        }

        void Read()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    str = str + (myArr[j, i].ToString());
                }
            }

            q++;
            finalArr[q] = str;
            str = "";
        }

        void ArrRead()
        {
            for (int i = 0; i < 49; i++)
            {
                print(finalArr[i]);
            }
        }

        void BuildRooms()
        {
            int a = 1;

            for (int i = 0; i < GenerationMap.maxLength/3; i++)
            {
                for (int j = 0; j < GenerationMap.maxLength/3; j++)
                {
                    str = finalArr[a];
                    CreatCell(str, j, i);
                    a++;
                }
            }
        }

        void CreatCell(string str, int x, int y)
        {
            Vector3 wV = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0));
            GameObject tempCell;

            switch (str)
            {
                case "000000000":
                    tempCell = Instantiate(room[0]);

                    break;
                case "010101010":
                    tempCell = Instantiate(room[1]);

                    break;
                case "000100000":
                    tempCell = Instantiate(room[2]);

                    break;
                case "010000000":
                    tempCell = Instantiate(room[3]);

                    break;
                case "000001000":
                    tempCell = Instantiate(room[4]);

                    break;
                case "000000010":
                    tempCell = Instantiate(room[5]);

                    break;
                case "000100010":
                    tempCell = Instantiate(room[6]);

                    break;
                case "000101000":
                    tempCell = Instantiate(room[7]);

                    break;
                case "000001010":
                    tempCell = Instantiate(room[8]);

                    break;
                case "010000010":
                    tempCell = Instantiate(room[9]);

                    break;
                case "010001000":
                    tempCell = Instantiate(room[10]);

                    break;
                case "010100000":
                    tempCell = Instantiate(room[11]);

                    break;
                case "010100010":
                    tempCell = Instantiate(room[12]);

                    break;
                case "010001010":
                    tempCell = Instantiate(room[13]);

                    break;
                case "010101000":
                    tempCell = Instantiate(room[14]);

                    break;
                case "000101010":
                    tempCell = Instantiate(room[15]);
                    break;
                default:
                    tempCell = Instantiate(room[0]);
                    tempCell.transform.SetParent(roomsParent, false);
                    break;
            }

            tempCell.transform.SetParent(roomsParent, false);
            float sprSizeX = tempCell.GetComponent<SpriteRenderer>().bounds.size.x;
            float sprSizeY = tempCell.GetComponent<SpriteRenderer>().bounds.size.y;
            tempCell.transform.position = new Vector3(wV.x + (sprSizeX * x), wV.y - (sprSizeY * y));
        }

        void Update()
        {
            if (Input.GetKey(KeyCode.R))
            {
                //Application.LoadLevel(0);
                SceneManager.LoadScene(0);
            }
        }

    }
}
