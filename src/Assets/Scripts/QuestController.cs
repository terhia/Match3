using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
        public class QuestController : MonoBehaviour
        {
                [SerializeField] private Canvas gameCanvas;

                [SerializeField] private List<CreationElement> creationElements;

                private GameElement[,] gameElements;


                private GameElement firstElement = null;
                private GameElement secondElement = null;
                
                private System.Random random = new System.Random();

                public void StartQuest(int m, int n)
                {
                        GenerateElements(m, n);
                }

                private void GenerateElements(int m, int n)
                {
                        gameElements = new GameElement[m, n];
                        for (int i = 0; i < gameElements.GetLength(0); i++)
                        {
                                for (int j = 0; j < gameElements.GetLength(1); j++)
                                {
                                        CreateElement(i, j);
                                }
                        }
                }

                private void UpdateElements()
                {
                        for (int i = 0; i < gameElements.GetLength(0); i++)
                        {
                                for (int j = 0; j < gameElements.GetLength(1); j++)
                                {
                                        if (gameElements[i, j] == null)
                                        {
                                                CreateElement(i, j);
                                        }
                                }
                        }
                }

                private void CreateElement(int x, int y)
                {
                        var creationElement = GetRandomCreationElement(new Position(x, y));

                        gameElements[x, y] = new GameElement();
                        gameElements[x, y].ID = creationElement.ID;

                        var obj = GameObject.Instantiate(creationElement.Image,
                                new Vector3(x*50, y*50, 0),
                                Quaternion.identity);
                        obj.transform.SetParent(gameCanvas.transform);
                        var inputHandler = obj.GetComponent<InputHandler>();
                        inputHandler.OnPointerDownUnityEvent.AddListener(GameElementClicked);
                        
                        gameElements[x, y].Obj = obj.gameObject;
                }

                private bool IsMatch()
                {
                        for (int i = 1; i < gameElements.GetLength(0) - 1; i++)
                        {
                                for (int j = 1; j < gameElements.GetLength(1) - 1; j++)
                                {
                                        if (gameElements[i - 1, j] != null && gameElements[i, j] != null &&
                                            gameElements[i + 1, j] != null)
                                        {
                                                if (
                                                        IsElementsMatch(new[]
                                                        {
                                                                gameElements[i - 1, j], gameElements[i, j],
                                                                gameElements[i + 1, j]
                                                        }))
                                                {
                                                        DeleteElements(new[]
                                                        {
                                                                gameElements[i - 1, j], gameElements[i, j],
                                                                gameElements[i + 1, j]
                                                        });
                                                        return true;
                                                }
                                        }
                                        if (gameElements[i, j - 1] != null && gameElements[i, j] != null &&
                                            gameElements[i, j + 1] != null)
                                        {
                                                if (
                                                        IsElementsMatch(new[]
                                                        {
                                                                gameElements[i, j - 1], gameElements[i, j],
                                                                gameElements[i, j + 1]
                                                        }))
                                                {
                                                        DeleteElements(new[]
                                                      {
                                                               gameElements[i, j - 1], gameElements[i, j],
                                                                gameElements[i, j + 1]
                                                        });
                                                        return true;
                                                }
                                        }
                                }
                        }
                        Debug.Log("False");
                        return false;
                }

                private bool IsElementsMatch(GameElement[] checkingElements)
                {
                        return checkingElements.All(checkingElement => checkingElement.ID == checkingElements[0].ID);
                }

                public void StopQuest()
                {
                        for (int i = 0; i < gameElements.GetLength(0); i++)
                        {
                                for (int j = 0; j < gameElements.GetLength(1); j++)
                                {
                                        if (gameElements[i, j] != null && gameElements[i, j].Obj != null)
                                        {
                                                Destroy(gameElements[i, j].Obj);
                                                gameElements[i, j] = null;
                                        }
                                }
                        }
                }

                private bool IsMatch(Position position, int id)
                {
                        if (position.X - 2 >= 0 && position.X - 1 >= 0)
                        {
                                if (IsElementsMatch(new[]
                                {
                                        gameElements[position.X - 2, position.Y], gameElements[position.X-1, position.Y],
                                        new GameElement() {ID = id}, 
                                }))
                                {
                                        return true;
                                }
                        }

                        if (position.Y - 2 >= 0 && position.Y - 1 >= 0)
                        {
                                if (IsElementsMatch(new[]
                                {
                                        gameElements[position.X, position.Y-2], gameElements[position.X, position.Y-1],
                                        new GameElement() {ID = id}
                                }))
                                {
                                        return true;
                                }
                        }
                        return false;
                }
                /// <summary>
                /// Достать элемент
                /// </summary>
                /// <returns></returns>
                private CreationElement GetRandomCreationElement(Position position)
                {
                        List<CreationElement> list = new List<CreationElement>();
                        foreach (var creationElement in creationElements)
                        {
                                if (!IsMatch(position, creationElement.ID))
                                {
                                        list.Add(creationElement);
                                }
                        }
                        Debug.Log(list.Count);
                        return list[random.Next(0, list.Count)];
                }

                private Position GetElementPosition(GameObject checkingGameObject)
                {
                        for (int i = 0; i < gameElements.GetLength(0); i++)
                        {
                                for (int j = 0; j < gameElements.GetLength(1); j++)
                                {
                                        if (gameElements[i, j].Obj.gameObject == checkingGameObject)
                                        {
                                                return new Position(i, j);
                                        }
                                }
                        }
                        return null;
                }

                private Position GetElementPosition(GameElement checkingGameElement)
                {
                        for (int i = 0; i < gameElements.GetLength(0); i++)
                        {
                                for (int j = 0; j < gameElements.GetLength(1); j++)
                                {
                                        if (gameElements[i, j] == checkingGameElement)
                                        {
                                                return new Position(i, j);
                                        }
                                }
                        }
                        return null;
                }

                private void DeleteElements(GameElement[] deletingElements)
                {
                        foreach (var deletingElement in deletingElements)
                        {
                                Destroy(deletingElement.Obj);
                        }
                }

                /// <summary>
                /// Смена местами двух элементов
                /// </summary>
                /// <param name="firstPosition">Позиция первого элемента</param>
                /// <param name="secondPosition">Позиция второго элемента</param>
                private void Swap(Position firstPosition, Position secondPosition)
                {
                        var temporaryElement1 = gameElements[firstPosition.X, firstPosition.Y].Obj.transform.position;
                        gameElements[firstPosition.X, firstPosition.Y].Obj.transform.position =
                                gameElements[secondPosition.X, secondPosition.Y].Obj.transform.position;
                        gameElements[secondPosition.X, secondPosition.Y].Obj.transform.position = temporaryElement1;

                        var temporaryElement = gameElements[firstPosition.X, firstPosition.Y];
                        gameElements[firstPosition.X, firstPosition.Y] =
                                gameElements[secondPosition.X, secondPosition.Y];
                        gameElements[secondPosition.X, secondPosition.Y] = temporaryElement;
                }

                private bool IsElementNear(GameElement element1, GameElement element2)
                {
                        return ((Mathf.Abs(GetElementPosition(element1).X - GetElementPosition(element2).X) == 1) &&
                                (Mathf.Abs(GetElementPosition(element1).Y - GetElementPosition(element2).Y) == 0)) ||
                               ((Mathf.Abs(GetElementPosition(element1).X - GetElementPosition(element2).X) == 0) &&
                                (Mathf.Abs(GetElementPosition(element1).Y - GetElementPosition(element2).Y) == 1));
                }

                private void CheckClickedElement(GameElement clickedGameElement)
                {
                        if (firstElement == null)
                        {
                                firstElement = clickedGameElement;
                                secondElement = null;
                        }
                        else
                        {
                                secondElement = clickedGameElement;

                                if (IsElementNear(firstElement, secondElement))
                                {
                                        Swap(GetElementPosition(firstElement), GetElementPosition(secondElement));

                                        if (IsMatch())
                                        {
                                                UpdateElements();
                                        }
                                        else
                                        {
                                                Swap(GetElementPosition(firstElement), GetElementPosition(secondElement));
                                        }

                                        firstElement = null;
                                        secondElement = null;
                                }
                                else
                                {
                                        firstElement = null;
                                        secondElement = null;
                                }
                        }
                }

                private void GameElementClicked(GameObject clickedGameObject)
                {
                        var pos = GetElementPosition(clickedGameObject);
                        CheckClickedElement(gameElements[pos.X, pos.Y]);
                }
        }
}