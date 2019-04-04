using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneraciónDeNivel : MonoBehaviour
{
	int[,] nivel;
	int filas=6;
	int columnas=4;
	//Distancia horizontal entre los centros de las plataformas
	float a=1.5f;
	//Distancia vertical entre los centros de plataformas
	float b=1f;
	public GameObject plat_1;
	public GameObject plat_2;
	public GameObject plat_3;
	public GameObject plat_4;
	public GameObject plat_5;

	void Awake()
	{
		int i;
		int j;
		int cont = 0;

		nivel = new int[filas, columnas];

		for (i=0; i < filas; i++) {
			for (j = 0; j < columnas; j++) {
				nivel [i, j] = Random.Range (0, 5);
			}
		}

		for (i=0; i < filas; i++) {
			for (j = 0; j < columnas; j++) {
				if (nivel [i, j] == 0) {
					cont++;
				}
			}
			if (cont == 0) {
				nivel [i, Random.Range (0, columnas)] = 0;
			}
			if (cont == columnas) {
				nivel [i, Random.Range (0, columnas)] = Random.Range (1, 5);
			}
			cont = 0;
		}

		for (i=1; i < filas; i++) {
			for (j = 0; j < columnas; j++) {
				if (nivel [i, j] == 0 && nivel [i - 1, j] == 0) {
					nivel [i, j] = Random.Range (1, 5);
					int temp = Random.Range (0, columnas);
					while (temp == j) {
						temp = Random.Range (0, columnas);
					}
					nivel [i, temp] = 0;
				}
			}
		}


	}

	void Start()
	{
		float k = 0f;
		float l = 0f;

		for (int i=0; i < filas; i++) {
			for (int j = 0; j < columnas; j++) {
				switch (nivel [i, j]) {

				case 1:
					Instantiate (plat_1, new Vector3 (k, l, 0), Quaternion.identity);
					break;
				case 2:
					Instantiate (plat_2, new Vector3 (k, l, 0), Quaternion.identity);
					break;
				case 3:
					Instantiate (plat_3, new Vector3 (k, l, 0), Quaternion.identity);
					break;
				case 4:
					Instantiate (plat_4, new Vector3 (k, l, 0), Quaternion.identity);
					break;
				case 5:
					Instantiate (plat_5, new Vector3 (k, l, 0), Quaternion.identity);
					break;
				}
				k = k + a;
			}
			k = 0;
			l = l + b;
		}
	}

}
