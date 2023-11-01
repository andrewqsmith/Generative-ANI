using LLama.Common;
using LLama;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LlamaExample : MonoBehaviour
{
    private static string Model1 = "llama-2-7b-guanaco-qlora.Q4_K_M.gguf";
    private string modelPath = Application.dataPath + "/Model/" + Model1;
    private string prompt = "Hello, world!";
    private ChatSession session;

    void Start()
    {
        var parameters = new ModelParams(modelPath)
        {
            ContextSize = 1024,
            Seed = 1337,
            GpuLayerCount = 5
        };
        using var model = LLamaWeights.LoadFromFile(parameters);
        // Initialize a chat session
        using var context = model.CreateContext(parameters);
        var ex = new InteractiveExecutor(context);
        session = new ChatSession(ex);
        // run the inference in a loop to chat with LLM
        while (prompt != "stop")
        {
            foreach (var text in session.Chat(prompt, new InferenceParams() { Temperature = 0.6f, AntiPrompts = new List<string> { "User:" } }))
            {
                Debug.Log(text);
            }
            prompt = Console.ReadLine();
        }
        // save the session
        session.SaveSession("SavedSessionPath");
    }

    void Update()
    {
        // Update code here
    }
}
