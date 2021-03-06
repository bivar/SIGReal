﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayersManager : MonoBehaviour
{
    //Lista para salvar todos os players cadastrados na partida
    private List<PlayersInfo> playerList = new List<PlayersInfo>();
    //Contador de players, máximo 10 (a referência de no máximo 10 está na função AddPlayer);
    private int playersCount = 0;
    
    public int PlayersCount
    {
        get { return playersCount; }
    }

    public GameObject playerPrefab;
    public Transform playerListTransform;
    public InputField nameInput;
    public RectTransform movableComponents;

    private void Start()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    public void AddPlayer()
    {
        if (!string.IsNullOrWhiteSpace(nameInput.text))
        {
            if (playersCount < 10)
            {
                playerList.Add(new PlayersInfo(nameInput.text, true));
                GameObject player = Instantiate(playerPrefab, playerListTransform);
                player.GetComponentInChildren<Text>().text = nameInput.text;
                if(PlayersCount != 0)
                {
                    player.transform.localPosition = new Vector3(playerList[PlayersCount-1].x, playerList[PlayersCount-1].y - 200);
                }
                playerList[playersCount].x = player.transform.localPosition.x;
                playerList[playersCount].y = player.transform.localPosition.y;

                playersCount++;
                nameInput.text = "";

                if(PlayersCount > 5)
                {
                    movableComponents.sizeDelta = new Vector2(1080, 1920 + ((PlayersCount - 5) * 200));
                }
            }
            else
            {
                //Colocar um aviso de que o número máximo de Players é de 10
                Debug.Log("full bitch");
            }
        }
    }

    public PlayersInfo GetPlayer(int number)
    {
        PlayersInfo player = playerList[number];
        playerList.RemoveAt(number);
        playersCount--;
        return player;
    }

    //Método para obter apenas players em uma única condição de saúde
    public List<PlayersInfo> GetSpecificPlayers(bool healthStatus)
    {
        List<PlayersInfo> specificPlayers = new List<PlayersInfo>();

        foreach (PlayersInfo player in playerList)
        {
            if (player.healthStatus == healthStatus)
            {
                specificPlayers.Add(player);
            }
        }

        return specificPlayers;
    }

    public void NextScene()
    {
        if(playersCount > 1)
        {
            SceneManager.LoadScene("EndlessMode", LoadSceneMode.Single);
        }
        else
        {
            //Colocar mensagem de que tem adicionar players para jogar
        }
        
    }
}

public class PlayersInfo
{
    public string name;
    public float x;
    public float y;
    public GameObject player;
    //Saúde do jogador: true indica saudável; false indica infectado com covid
    public bool healthStatus = true;
    

    //Cor do jogador, ainda nao é importante
    //private Color color;

    public PlayersInfo(string name)
    {
        this.name = name;
    }

    public PlayersInfo(string name, bool healthStatus)
    {
        this.healthStatus = healthStatus;
        this.name = name;
    }
    /*public PlayersInfo(string name, Color color)
    {
        this.name = name;
        this.color = color;
    }*/
}
