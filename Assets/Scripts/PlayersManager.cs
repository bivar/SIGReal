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

    public InputField nameInput;
    public Text playersListText;

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
                playerList.Add(new PlayersInfo(nameInput.text));
                playersListText.text += nameInput.text + ";\n";
                playersCount++;
                nameInput.text = "";
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

    public void NextScene()
    {
        if(playersCount > 0)
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

    //Cor do jogador, ainda nao é importante
    //private Color color;

    public PlayersInfo(string name)
    {
        this.name = name;
    }

    /*public PlayersInfo(string name, Color color)
    {
        this.name = name;
        this.color = color;
    }*/
}