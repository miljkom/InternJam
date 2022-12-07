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

	public Transform referenceRoot;
	public Transform parentInstantiate;
	public Transform parentHolder;

	GesturePatternDraw[] references;

	void Start()
	{
		references = referenceRoot.GetComponentsInChildren<GesturePatternDraw>();
	}

	void ShowAll()
	{
		for (int i = 0; i < references.Length; i++)
		{
			references[i].gameObject.SetActive(true);
		}
	}

	public void OnRecognize(RecognitionResult result)
	{
		StopAllCoroutines();
		ShowAll();
		if (result != RecognitionResult.Empty)
		{
			if (result.gesture.id == RandomGesture.instance.currentGesture.id)
			{
				if (RandomElement.instance.currentIndex2 == 4)
				{
					RandomElement.instance.tntBomb();
					textResult.text = "Boom u lost progress";
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

