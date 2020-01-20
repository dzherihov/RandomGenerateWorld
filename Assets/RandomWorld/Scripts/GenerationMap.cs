using UnityEngine;

namespace Project
{
    public class GenerationMap : MonoBehaviour
    {
        public static int[,] myArr;
        
        public static int maxRoom;
        public int maxRoomInspector;

        public static int maxLength;

        // public int maxLengthInspector;
        private string str;
        private int randomDoor, randomRoom;

        void Awake()
        {
            
            maxRoom = maxRoomInspector ;
            // максимальная длина массива строится от максимального кол-ва возможных комнат
            // макс комнат * на зазмерность комнаты * 2 стороны и + размерность начальной комнаты
            // начальная комнаты

            maxLength = maxRoom * 3 * 2 + 3;
            myArr = new int[maxLength, maxLength];
            // вызов простановки дверей в 4 стороны
            One(maxLength / 2 + 1, maxLength / 2, maxLength / 2, maxLength / 2, 1, 1);
            One(maxLength / 2 - 1, maxLength / 2, maxLength / 2, maxLength / 2, 1, 1);
            One(maxLength / 2, maxLength / 2 + 1, maxLength / 2, maxLength / 2, 1, 1);
            One(maxLength / 2, maxLength / 2 - 1, maxLength / 2, maxLength / 2, 1, 1);

            //сбор массивва в строку и передача строки
            for (int i = 0; i < maxLength; i++)
            {
                for (int j = 0; j < maxLength; j++)
                {
                    str = str + myArr[i, j];
                }

                str = str + "\n";
            }

            Debug.Log(str);
        }

        public void One(int xNew, int yNew, int xOld, int yOld, int door, int room)
        {
            //ставим 1, она означает дверь у новой координаты

            myArr[xNew, yNew] = 1;


            // если мы шли вверх или вниз то 

            if (Mathf.Abs(yNew - yOld) != 0)
            {
                // то сбоку от двери должна быть стена, ставим 0

                myArr[xNew - 1, yNew] = 0;
                myArr[xNew + 1, yNew] = 0;

                // если мы шли вниз то необходимо поменять значение старой 
                // и новой точки координаты и увеличить значение

                if (yNew > yOld)
                {
                    yOld = yNew;
                    yNew++;
                }

                // если мы шли вверх то необходимо поменять значение старой 
                // и новой точки координаты и  уменьшить координату

                else
                {
                    yOld = yNew;
                    yNew--;
                }
            }

            // если мы шли влево или право

            else
            {
                // если мы идем влево или вправо то 
                // то сверху и снизу от двери должна быть стена, ставим 0
                myArr[xNew, yNew - 1] = 0;
                myArr[xNew, yNew + 1] = 0;

                // если вправо необходимо поменять значение старой 
                // и новой точки координаты и увеличить значение
                if (xNew > xOld)
                {
                    xOld = xNew;
                    xNew++;
                }
                // если влево то необходимо поменять значение старой 
                // и новой точки координаты и  уменьшить координату
                else
                {
                    xOld = xNew;
                    xNew--;
                }
            }

            // для работы алгоритма необходимо комнату представлять как
            // массив размерностью 3*3, следовательно выход из комнаты 
            // должен ставить еще 1 дверь, а потом ставить 0

            if (door == 1)
            {
                door--;
                One(xNew, yNew, xOld, yOld, door, room);
            }
            else
            {
                Zero(xNew, yNew, xOld, yOld, room);
            }
        }

        public void Zero(int xNew, int yNew, int xOld, int yOld, int room)
        {
            //ставим 0, который означает центр комнаты
            myArr[xNew, yNew] = 0;

            // чем больше у нас максимальное колличество комнат
            // тем больше шанс на появление новой комнаты 
            // если мы дошли до максимального колличества комнат
            // то мы не запускаем распределение комнат дальше
            randomRoom = Random.Range(0, maxRoom);
            if (room < randomRoom)
            {
                // если мы шли вверх или вниз то 
                if (Mathf.Abs(yNew - yOld) != 0)
                {
                    // увеличиваем колличество комнат  и с веоятностью 50% запускаем 
                    // простройку дверей в разные стороны 
                    room++;

                    // если мы шли вниз то
                    if (yNew > yOld)
                    {
                        // с веоятностью 50% запускаем 
                        //стройку дверей в разные стороны
                        randomDoor = Random.Range(0, 2);

                        // простройка вниз

                        if (myArr[xNew, yNew + 1] != 1 && randomDoor == 1)
                            One(xNew, yNew + 1, xOld, yOld + 1, 1, room);
                        randomDoor = Random.Range(0, 2);

                        // прстройка вправо
                        if (myArr[xNew + 1, yNew] != 1 && randomDoor == 1)
                            One(xNew + 1, yNew, xOld, yOld + 1, 1, room);
                        randomDoor = Random.Range(0, 2);

                        // простройка влево
                        if (myArr[xNew - 1, yNew] != 1 && randomDoor == 1)
                            One(xNew - 1, yNew, xOld, yOld + 1, 1, room);
                    }

                    // если мы шли вверх то 
                    else
                    {
                        // увеличиваем колличество комнат  и с веоятностью 50% запускаем 
                        // простройку дверей в разные стороны 

                        randomDoor = Random.Range(0, 2);
                        // простройка вверх 
                        if (myArr[xNew, yNew - 1] != 1 && randomDoor == 1)
                            One(xNew, yNew - 1, xOld, yOld - 1, 1, room);
                        randomDoor = Random.Range(0, 2);

                        // прстройка вправо
                        if (myArr[xNew + 1, yNew] != 1 && randomDoor == 1)
                            One(xNew + 1, yNew, xOld, yOld - 1, 1, room);
                        randomDoor = Random.Range(0, 2);

                        // простройка влево
                        if (myArr[xNew - 1, yNew] != 1 && randomDoor == 1)
                            One(xNew - 1, yNew, xOld, yOld - 1, 1, room);
                    }
                }
                else
                {
                    room++;
                    // если шли вправо то
                    if (xNew > xOld)
                    {
                        // простройка вправо
                        randomDoor = Random.Range(0, 2);
                        if (myArr[xNew + 1, yNew] != 1 && randomDoor == 1)
                            One(xNew + 1, yNew, xOld + 1, yOld, 1, room);
                        randomDoor = Random.Range(0, 2);
                        // простройка вниз
                        if (myArr[xNew, yNew + 1] != 1 && randomDoor == 1)
                            One(xNew, yNew + 1, xOld + 1, yOld, 1, room);
                        randomDoor = Random.Range(0, 2);
                        // простройка вверх
                        if (myArr[xNew, yNew - 1] != 1 && randomDoor == 1)
                            One(xNew, yNew - 1, xOld + 1, yOld, 1, room);
                    }
                    // если шли влево 
                    else
                    {
                        //простройка влево
                        randomDoor = Random.Range(0, 2);
                        if (myArr[xNew - 1, yNew] != 1 && randomDoor == 1)
                            One(xNew - 1, yNew, xOld - 1, yOld, 1, room);
                        randomDoor = Random.Range(0, 2);
                        // простройка вниз
                        if (myArr[xNew, yNew + 1] != 1 && randomDoor == 1)
                            One(xNew, yNew + 1, xOld - 1, yOld, 1, room);
                        randomDoor = Random.Range(0, 2);
                        // простройка вверх 
                        if (myArr[xNew, yNew - 1] != 1 && randomDoor == 1)
                            One(xNew, yNew - 1, xOld - 1, yOld, 1, room);
                    }
                }
            }
        }
    }
}