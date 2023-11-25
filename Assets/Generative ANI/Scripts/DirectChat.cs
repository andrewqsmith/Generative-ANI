using Cysharp.Threading.Tasks;
using LLama;
using LLama.Common;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class DirectChat : MonoBehaviour
{
    private string ModelPath;
    private float TemperatureValue = 0.6f;
    private uint ContextSizeValue = 4096;
    private uint SeedValue = 1337;
    private int GpuLayerCountValue = 100;

    public string ModelFileName = "llama-2-7b-guanaco-qlora.Q4_K_M.gguf";
    public TMP_Text Output;
    public TMP_InputField Input;
    public TMP_InputField Temperature;
    public TMP_InputField Context;
    public TMP_InputField Seed;
    public TMP_InputField GpuLayerCount;
    public Button Submit;
    public NPC_State npcState;

    private string _submittedText = "";

    void Awake()
    {
        ModelPath = Application.dataPath + "/Generative ANI/Model/" + ModelFileName;
        Temperature.text = TemperatureValue.ToString();
        Context.text = ContextSizeValue.ToString();
        Seed.text = SeedValue.ToString();
        GpuLayerCount.text = GpuLayerCountValue.ToString();

    }

    async UniTaskVoid Start()
    {
        Submit.interactable = false;

        CreateListeners();
        
        Output.text = "User: Hello!\r\n";
        var ai_prompt = "Say hello"; // use the "chat-with-AI" prompt here.
        // Load a model
        var parameters = new ModelParams(ModelPath)
        {
            ContextSize = ContextSizeValue,
            Seed = SeedValue,
            GpuLayerCount = GpuLayerCountValue
        };
        // Switch to the thread pool for long-running operations
        await UniTask.SwitchToThreadPool();
        using var model = LLamaWeights.LoadFromFile(parameters);
        await UniTask.SwitchToMainThread();

        // Initialize a chat session
        using var context = model.CreateContext(parameters);
        var ex = new InteractiveExecutor(context);
        ChatSession session = new ChatSession(ex);

        Submit.interactable = true;
        // run the inference in a loop to chat with LLM
        while (ai_prompt != "stop")
        {
            await foreach (var token in ChatConcurrent(
                session.ChatAsync(
                    ai_prompt,
                    new InferenceParams()
                    {
                        Temperature = TemperatureValue,
                        AntiPrompts = new List<string> { "User:" }
                    }
                )
            ))
            {
                Output.text += token;
            }
            await UniTask.WaitUntil(() => _submittedText != "");
            ai_prompt = _submittedText;
            _submittedText = "";
            Output.text += " " + ai_prompt + "\n";
        }
        Submit.onClick.RemoveAllListeners();
    }

    private async IAsyncEnumerable<string> ChatConcurrent(IAsyncEnumerable<string> tokens)
    {
        await UniTask.SwitchToThreadPool();
        await foreach (var token in tokens)
        {
            yield return token;
        }
    }

    private void CreateListeners()
    {

        Submit.onClick.AddListener(() =>
        {
            _submittedText = Input.text;
            Input.text = "";
        });

        Temperature.onValueChanged.AddListener((string text) =>
        {
            if (!float.TryParse(Temperature.text, out TemperatureValue))
            {
                TemperatureValue = 0.6f;
            }
            Debug.Log("Temperature: " + TemperatureValue);
        });

        Context.onValueChanged.AddListener((string text) =>
        {
            if (!uint.TryParse(Context.text, out ContextSizeValue))
            {
                ContextSizeValue = 4096;
            }
            Debug.Log("Context: " + ContextSizeValue);
        });

        Seed.onValueChanged.AddListener((string text) =>
        {
            if (!uint.TryParse(Seed.text, out SeedValue))
            {
                SeedValue = 1337;
            }
            Debug.Log("Seed: " + SeedValue);
        });

        GpuLayerCount.onValueChanged.AddListener((string text) =>
        {
            if (!int.TryParse(GpuLayerCount.text, out GpuLayerCountValue))
            {
                GpuLayerCountValue = 100;
            }
            Debug.Log("GpuLayerCount: " + GpuLayerCountValue);
        });
    }

}
