using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GestureRecognizer;
using System.Linq;
using Random = UnityEngine.Random;

public class GestureHandler : MonoBehaviour
{

	public Text textResult;
	public Transform parentHolder;
	public GameObject drawScreen;
	public static bool isGuessed = false;

	public void OnRecognize(RecognitionResult result)
	{
		if (result != RecognitionResult.Empty)
		{
			if (result.gesture.id == RandomGesture.instance.currentGesture.id && !RandomElement.noCurrentElement)
			{
				if (RandomElement.instance.currentIndex3 == 4)
				{
					RandomElement.instance.TntBomb();
					textResult.text = "Boom u lost progress(will add popup)!";
					RandomElement.instance.secondsToChangeImage = 4f;
					RandomGesture.instance.guessCounter = 0;
				}
				else
				{
					isGuessed = true;
					RandomElement.dontCreate = true;
					int index = RandomElement.instance.currentIndex3;
					Elements.instance.elements[index]++;
					SceneGame1 sceneGame1 = GetComponent<SceneGame1>();
					GameObject elementForAnimation = Instantiate(RandomElement.instance.imageList[index], Vector2.zero, Quaternion.identity, parentHolder);
					
					elementForAnimation.transform.localPosition = Vector2.zero;
					elementForAnimation.transform.parent = parentHolder;
					elementForAnimation.AddComponent<ElementFly>();
					elementForAnimation.GetComponent<ElementFly>().positionToGo = sceneGame1.textPlaces[index].position;

					if(Elements.instance.elements[index] <= 3)
						RandomGesture.instance.guessCounter++;
					if(RandomGesture.instance.guessCounter == 3)
					{
						RandomElement.instance.secondsToChangeImage -= 0.5f;
						Debug.Log(RandomElement.instance.secondsToChangeImage + " sekunde");
						RandomGesture.instance.guessCounter = 0;
					}
					Debug.Log(RandomGesture.instance.guessCounter);
					RandomElement.noCurrentElement = true;
					textResult.text = "Good job!";
				}
			}
			else
			{
				textResult.text = "Try again?";
			}
		}
		else
		{
			textResult.text = "Try again?";
		}
	}
}

