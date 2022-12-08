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
	
	public Transform parentInstantiate;
	public Transform parentHolder;
	
	public void OnRecognize(RecognitionResult result)
	{
		if (result != RecognitionResult.Empty)
		{
			if (result.gesture.id == RandomGesture.instance.currentGesture.id)
			{
				if (RandomElement.instance.currentIndex2 == 4)
				{
					RandomElement.instance.tntBomb();
					textResult.text = "Boom u lost progress";
					RandomElement.instance.StopAllCoroutines();
					RandomGesture.instance.StopAllCoroutines();
					
					RandomElement.instance.secondsToChangeImage = 4f;
					RandomGesture.instance.secondsToChangeGesture = 4f;
					RandomElement.instance.StartCoroutine("ChangeImage");
					RandomGesture.instance.StartCoroutine("ChangeGesture");
					RandomGesture.instance.guessCounter = 0;
				}
				else
				{
					int index = RandomElement.instance.currentIndex2;
					Elements.instance.elements[RandomElement.instance.currentIndex2]++;
					SceneGame1 sceneGame1 = GetComponent<SceneGame1>();
					GameObject elementForAnimation = Instantiate<GameObject>(RandomElement.instance.imageList[index],
						Vector2.zero, Quaternion.identity, parentInstantiate);
					elementForAnimation.transform.localPosition = Vector2.zero;
					elementForAnimation.transform.parent = parentHolder;
					elementForAnimation.AddComponent<ElementFly>();
					elementForAnimation.GetComponent<ElementFly>().positionToGo = sceneGame1.textPlaces[index].position;
					RandomGesture.instance.guessCounter++;
					Debug.Log(RandomGesture.instance.guessCounter);
					if (RandomGesture.instance.guessCounter == 3)
					{
						RandomGesture.instance.StopAllCoroutines();
						RandomElement.instance.StopAllCoroutines();
						
						RandomGesture.instance.secondsToChangeGesture -= 0.5f;
						RandomElement.instance.secondsToChangeImage -= 0.5f;
						
						RandomGesture.instance.StartCoroutine("ChangeGesture");
						RandomElement.instance.StartCoroutine("ChangeImage");
						RandomGesture.instance.guessCounter = 0;
					}
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

