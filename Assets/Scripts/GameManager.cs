using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Clase Pregunta
    public class Pregunta
    {
        public string pregunta;
        public string respuesta;
        public string pista;
    }

    // Variables 
    List<Pregunta> preguntas = new List<Pregunta>(); // lista de preguntas
    Pregunta preguntaActual; // pregunta actual
    string respuestaUsuario;
    public Text TextoPregunta, RespuestaPregunta, TextoPista;
    public GameObject PanelInicio, PanelFinal;
    bool juegoIniciado = false; // indica si el juego ha comenzado
    bool juegoTerminado = false; // indica si el juego ha terminado
    System.Random rnd = new System.Random(); // para seleccionar preguntas aleatorias

    // Start is called before the first frame update
    void Start()
    {
        // crear preguntas y añadirlas a la lista
        preguntas.Add(new Pregunta { pregunta = "Adivinanza 1", respuesta = "Respuesta 1", pista = "Pista 1" });
        preguntas.Add(new Pregunta { pregunta = "Adivinanza 2", respuesta = "Respuesta 2", pista = "Pista 2" });
        preguntas.Add(new Pregunta { pregunta = "Adivinanza 3", respuesta = "Respuesta 3", pista = "Pista 3" });

        PanelInicio.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (juegoIniciado && !juegoTerminado) // si el juego ha comenzado y no ha terminado
        {
            MostrarPregunta();
            GuardaRespuestaUsuario();
        }
    }

    void MostrarPregunta()
    {
        TextoPregunta.text = preguntaActual.pregunta;
        TextoPista.text = preguntaActual.pista;
    }

    void GuardaRespuestaUsuario()
    {
        if (RespuestaPregunta.text != "")
        {
            respuestaUsuario = RespuestaPregunta.text;
        }
    }

    public void ValidacionRespuesta()
    {
        string respuestaCorrecta = preguntaActual.respuesta.ToUpper();
        string respuestaActualUsuario = respuestaUsuario.ToUpper();

        if (respuestaCorrecta == respuestaActualUsuario)
        {
            Debug.Log("Correcto");
            SiguienteAdivinanza();
        }
        else
        {
            Debug.Log("Incorrecto");
        }
    }

    public void SiguienteAdivinanza()
    {
        if (preguntas.Count > 0) // si quedan preguntas en la lista
        {
            int index = rnd.Next(preguntas.Count); // seleccionar pregunta aleatoria
            preguntaActual = preguntas[index]; // asignarla como pregunta actual
            preguntas.RemoveAt(index); // eliminarla de la lista
            Invoke("MostrarPregunta", 5); // mostrar la siguiente pregunta después de 5 segundos
        }
        else // si no quedan preguntas
        {
            juegoTerminado = true; // indicar que el juego ha terminado
            PanelFinal.SetActive(true); // mostrar el panel de final de juego
        }
    }

    public void IniciarJuego()
    {
        if (PanelInicio.activeInHierarchy == true)
        {
            PanelInicio.SetActive(false);
        }

        if (PanelFinal.activeInHierarchy == true)
        {
            PanelFinal.SetActive(false);
        }

        juegoIniciado = true; // indicar que el juego ha comenzado
        SiguienteAdivinanza(); // mostrar la primera pregunta
    }

    public void SalirJuego()
    {
        Application.Quit();
    }

    public void JugarNuevo()
    {
        // resetear variables
        preguntas.Clear();
        preguntaActual = null;
        respuestaUsuario = "";
        juegoIniciado = false;
        juegoTerminado = false;

        // crear preguntas y añadirlas a la lista
        Start();

        // mostrar panel de inicio
        PanelFinal.SetActive(false);
    }
}