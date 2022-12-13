using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GestureRecognizer;
using System.Linq;
using Unity.VisualScripting;
using Random = UnityEngine.Random;

public class GestureHandler : MonoBehaviour
{

	public Text textResult;
	public Transform parentHolder;
	public Transform popupLoseHolder;
	
	public GameObject popupLose;
	public GameObject drawScreen;
	public static bool isGuessed = false;
	private bool textFade = true;

	public void OnRecognize(RecognitionResult result)
	{
		StopAllCoroutines();
		if (result != RecognitionResult.Empty)
		{
			if (result.gesture.id == RandomGesture.instance.currentGesture.id && !RandomElement.noCurrentElement)
			{
				if (RandomElement.instance.currentIndex3 == 4)
				{
					RandomElement.instance.TntBomb();
					popupLose = Instantiate<GameObject>(this.popupLose,
						Vector2.zero, Quaternion.identity, popupLoseHolder);
					popupLose.transform.localPosition = Vector2.zero;
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
					textResult.color = Color.white;
					textResult.text = "Good job!";
					StartCoroutine("ShowText");
				}
			}
			else
			{
				textResult.text = "Wrong!";
				textResult.color = Color.red;
				StartCoroutine("ShowText");
			}
		}
		else
		{
			textResult.text = "Wrong!";
			textResult.color = Color.red;
			StartCoroutine("ShowText");
		}
	}

	IEnumerator ShowText()
	{
		float progress = 0.0f;
		float lerpduration = 12f;
		while (progress < lerpduration)
		{
			textResult.color = new Color(textResult.color.r, textResult.color.g, textResult.color.b, Mathf.Lerp(textResult.color.a,0f,progress / lerpduration));
			progress += Time.deltaTime;
			yield return null;
		}
	}
}

