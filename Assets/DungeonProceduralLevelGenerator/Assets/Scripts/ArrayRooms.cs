using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProceduralLevelGenerator
{
    public class ArrayRooms : MonoBehaviour
    {
        private int[,] myArr = new int[3, 3];
        private string str;
        public static string[] finalArr ;
        private int q = 0;
        private int maxLength;

        public GameObject[] room;
        public Transform roomsParent;
        public GameObject spawnPoint;
        public GameObject finishPoint;

        [SerializeField]
        private int scaleMulty = 5;

        [Header("if false - server")]
        public bool _client;

        [Header("Send to client")]
        public int getMaxBranching;
        [TextArea(10, 10)]
        public string getMatrix;

        private void Start()
        {
            if (_client)
            {
                RunClient(getMatrix);
            }

            else
            {
                RunServer();
            }
        }

        void RunServer()
        {
            GetComponent<GenerationMap>().GenerationMatrix();

            maxLength = GenerationMap.maxLength;

            finalArr = new string[maxLength * maxLength];
            int j = 0;
            int x = 0;
            int y = 0;
            int c = 0;
            int p = 0;
            int a = 0;
            for (a = 0; a < maxLength / 3; a++)
            {
                p = 0;
                for (p = 0; p < maxLength / 3; p++)
                {
                    c = 0;
                    int i = 0;

                    for (i = a * 3; i < maxLength + 1; i++)
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
                        for (j = c; j < maxLength + 1; j++)
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


        void RunClient(string _getMatrix)
        {

            maxLength = getMaxBranching * 3 * 2 + 3;

            finalArr = new string[maxLength * maxLength];

            char[] arr = new char[maxLength * maxLength];

            arr = _getMatrix.ToCharArray();

            int j1 = 0;
            int x1 = 0;
            int y1 = 0;
            int c1 = 0;
            int p1 = 0;
            int a1 = 0;
            for (a1 = 0; a1 < maxLength / 3; a1++)
            {
                p1 = 0;
                for (p1 = 0; p1 < maxLength / 3; p1++)
                {
                    c1 = 0;
                    int i1 = 0;

                    for (i1 = a1 * 3; i1 < maxLength + 1; i1++)
                    {
                        if ((i1 != a1 * 3) && (i1 % 3 == 0))
                        {
                            x1 = 0;
                            Read();
                            j1 = 0;
                            y1 = 0;
                            break;
                        }

                        c1 = j1 + p1 * 3;
                        for (j1 = c1; j1 < maxLength + 1; j1++)
                        {
                            if ((j1 != c1) && (j1 % 3 == 0))
                            {
                                break;
                            }

                            myArr[x1, y1] = int.Parse(arr[maxLength * i1 + j1].ToString());

                            x1++;
                        }

                        j1 = 0;
                        y1++;
                        x1 = 0;
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
            bool spawn = false, finish = false;

            for (int i = 0; i < maxLength / 3; i++)
            {
                for (int j = 0; j < maxLength / 3; j++)
                {
                    if (a == SpawnPointCheck())
                        spawn = true;

                    if (a == FinishPointCheck())
                        finish = true;


                    str = finalArr[a];
                    CreatCell(str, j, i, spawn, finish);
                    a++;

                    spawn = false;
                    finish = false;
                }
            }
            roomsParent.localScale = new Vector3(scaleMulty, scaleMulty, scaleMulty);
        }

        int SpawnPointCheck()
        {

            int myIndex = 0;

            int[] Mas = new int[3] { Array.LastIndexOf(finalArr, "010000000"), Array.LastIndexOf(finalArr, "000100000"), Array.LastIndexOf(finalArr, "010001000") };

            for (int i = 0; i < Mas.Length; i++)
            {
                if (Mas[i] > myIndex)
                    myIndex = Mas[i];
            }

            if (myIndex > Array.LastIndexOf(finalArr, "010101010"))
                return myIndex;
            else return Array.LastIndexOf(finalArr, "010101010");
        }

        int FinishPointCheck()
        {

            int myIndex = 9999999;

            int[] Mas = new int[3] { Array.IndexOf(finalArr, "000000010"), Array.IndexOf(finalArr, "000001010"), Array.IndexOf(finalArr, "000001000") };

            for (int i = 0; i < Mas.Length; i++)
            {
                if (Mas[i] < myIndex && Mas[i] != 0 && Mas[i] != -1)
                    myIndex = Mas[i];
            }

            if (myIndex < Array.IndexOf(finalArr, "010101010"))
                return myIndex;
            else return Array.IndexOf(finalArr, "010101010");
        }

        void CreatCell(string str, int x, int y, bool spawn = false, bool finish = false)
        {
            Vector3 wV = Vector3.zero;
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
            tempCell.transform.position = new Vector3(wV.x + (10 * x), wV.y - (10 * y));

            if (spawn)
            {
                GameObject spawnP = Instantiate(spawnPoint, tempCell.transform.position, Quaternion.identity);
                spawnP.transform.SetParent(tempCell.transform, false);
                spawnP.transform.position = tempCell.transform.position;
            }
            if (finish)
            {
                GameObject finishP = Instantiate(finishPoint, tempCell.transform.position, Quaternion.identity);
                finishP.transform.SetParent(tempCell.transform, false);
                finishP.transform.position = tempCell.transform.position;
            }

        }

        void Update()
        {
            if (Input.GetKey(KeyCode.R))
            {
                SceneManager.LoadScene(0);
            }
        }
    }
}
