using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientsManager : MonoBehaviour
{
    [SerializeField]
    private List<Client> _clients = new();

	private void Start()
	{
		StartCoroutine(StartClients());
	}

	public void ClientLeft(Client client, bool timeOut = false)
	{
		if (timeOut)
			OverlayCanvas.Instance.Strike();

		StartCoroutine(NewClient(client));
	}

	private IEnumerator StartClients()
	{
		yield return new WaitForSeconds(2f);
		foreach (var client in _clients)
		{
			client.SetRandomOrder();
			yield return new WaitForSeconds(30f);
		}
	}
	private IEnumerator NewClient(Client client)
	{
		yield return new WaitForSeconds(30f);
		client.SetRandomOrder();
	}
}
