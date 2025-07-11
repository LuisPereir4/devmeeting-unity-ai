// 11/07/2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class JogoDaVelha : MonoBehaviour
{
    public Button[] botoes; // Referência aos 9 botões
    public TextMeshProUGUI textoVencedor; // Referência ao texto do vencedor
    public Button botaoReiniciar; // Referência ao botão de reinício

    private string jogadorAtual = "X"; // Jogador atual (X ou O)
    private string[] tabuleiro = new string[9]; // Estado do tabuleiro

    void Start()
    {
        ReiniciarTabuleiro();

        // Configurar o botão de reinício
        botaoReiniciar.onClick.AddListener(ReiniciarTabuleiro);
        botaoReiniciar.gameObject.SetActive(false); // Esconde o botão inicialmente
    }

    public void AoClicarBotao(int indice)
    {
        if (tabuleiro[indice] == "")
        {
            tabuleiro[indice] = jogadorAtual;

            // Atualiza o texto do botão usando TextMeshProUGUI
            TextMeshProUGUI textoBotao = botoes[indice].GetComponentInChildren<TextMeshProUGUI>();
            if (textoBotao != null)
            {
                textoBotao.text = jogadorAtual;
            }

            if (VerificarVitoria())
            {
                MostrarVencedor($"Jogador {jogadorAtual} venceu!");
            }
            else if (VerificarEmpate())
            {
                MostrarVencedor("Empate!");
            }
            else
            {
                jogadorAtual = jogadorAtual == "X" ? "O" : "X";
            }
        }
    }

    void ReiniciarTabuleiro()
    {
        for (int i = 0; i < tabuleiro.Length; i++)
        {
            tabuleiro[i] = "";

            // Limpa o texto do botão usando TextMeshProUGUI
            TextMeshProUGUI textoBotao = botoes[i].GetComponentInChildren<TextMeshProUGUI>();
            if (textoBotao != null)
            {
                textoBotao.text = "";
            }
        }

        jogadorAtual = "X";
        textoVencedor.text = ""; // Limpa o texto do vencedor
        botaoReiniciar.gameObject.SetActive(false); // Esconde o botão de reinício
        AtivarBotoes(true); // Reativa os botões do tabuleiro
    }

    void MostrarVencedor(string mensagem)
    {
        textoVencedor.text = mensagem; // Exibe a mensagem do vencedor
        botaoReiniciar.gameObject.SetActive(true); // Mostra o botão de reinício
        AtivarBotoes(false); // Desativa os botões do tabuleiro
    }

    void AtivarBotoes(bool estado)
    {
        foreach (var botao in botoes)
        {
            botao.interactable = estado;
        }
    }

    bool VerificarVitoria()
    {
        // Verificar linhas
        for (int i = 0; i < 3; i++)
        {
            if (tabuleiro[i * 3] == jogadorAtual && tabuleiro[i * 3 + 1] == jogadorAtual && tabuleiro[i * 3 + 2] == jogadorAtual)
                return true;
        }

        // Verificar colunas
        for (int i = 0; i < 3; i++)
        {
            if (tabuleiro[i] == jogadorAtual && tabuleiro[i + 3] == jogadorAtual && tabuleiro[i + 6] == jogadorAtual)
                return true;
        }

        // Verificar diagonais
        if (tabuleiro[0] == jogadorAtual && tabuleiro[4] == jogadorAtual && tabuleiro[8] == jogadorAtual)
            return true;
        if (tabuleiro[2] == jogadorAtual && tabuleiro[4] == jogadorAtual && tabuleiro[6] == jogadorAtual)
            return true;

        return false;
    }

    bool VerificarEmpate()
    {
        foreach (var celula in tabuleiro)
        {
            if (celula == "")
                return false;
        }
        return true;
    }
}
