using System;
using Cysharp.Threading.Tasks;
using LLama;
using LLama.Common;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using UnityEngine.Windows;
using Debug = UnityEngine.Debug;

public class DialogueGeneration : MonoBehaviour
{
    private string ModelPath;
    
    public string ModelFileName = "llama-2-7b-guanaco-qlora.Q4_K_M.gguf";
    public TMP_Text Output;

    public string CharacterName;
    public string PlayerName;
    public float TemperatureValue = 0.6f;
    public uint ContextSizeValue = 4096;
    public uint SeedValue = 1337;
    public int GpuLayerCountValue = 100;
    public NPC_State npcState;
    public int promptOption = 1;
    public int promptWordLimit = 20;

    // Enumerations
    public NPC_State.CharacterGender CharacterGender = new NPC_State.CharacterGender();
    public NPC_State.AnimateState AnimateState = new NPC_State.AnimateState();
    public NPC_State.AttitudeState AttitudeState = new NPC_State.AttitudeState();
    public NPC_State.Genre Genre = new NPC_State.Genre();
    
    private string _submittedText = "";

    void Awake()
    {
        ModelPath = Application.dataPath + "/Generative ANI/Model/" + ModelFileName;
    }
    
    public async UniTaskVoid StartDialogueGeneration()
    {
        //Submit.interactable = false;

        //Output.text = "User: Hello!\r\n";
        var ai_prompt = createPrompt();
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

        // run the inference in a loop to chat with LLM
        while (ai_prompt != "stop")
        {
            await foreach (var token in ChatConcurrent(
                session.ChatAsync(
                    ai_prompt,
                    new InferenceParams()
                    {
                        Temperature = TemperatureValue,
                        AntiPrompts = new List<string> { PlayerName }
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
    }

    private async IAsyncEnumerable<string> ChatConcurrent(IAsyncEnumerable<string> tokens)
    {
        await UniTask.SwitchToThreadPool();
        await foreach (var token in tokens)
        {
            yield return token;
        }
    }

    private String createPrompt()
    {
        string genderString = CharacterGender.ToString();
        Debug.Log("The character's gender is: " + genderString);

        string attitudeString = AttitudeState.ToString();
        Debug.Log("The character's attitude is: " + attitudeString);

        string animateString = AnimateState.ToString();
        Debug.Log("The character's animation is: " + animateString);

        string genreString = Genre.ToString();
        Debug.Log("The genre is: " + genreString);

        return SuperPromptOptions(genderString, attitudeString, animateString, genreString);
    }

    private String SuperPromptOptions(string genderString, string attitudeString, string animateString, string genreString)
    {
        String prompt = "";
        switch(promptOption)
        {
            case 1: prompt = $"Only generate dialogue for the following non-playable character. Do not produce any additional text or instructions. " +
                             $"Limit to {promptWordLimit} words. Generate unique and creative dialogue suited for a {genderString} " +
                             $"non-playable character in a {genreString} game. Use details about the current " +
                             $"animation state such {animateString} to inform appropriate emotional tone of " +
                             $"{attitudeString} and context. Focus the dialogue on moving the gameplay narrative " +
                             $"forward by revealing interesting lore details, side quest opportunities, or " +
                             $"comments relating to the player's current objectives.";
                    break;
            case 2: prompt = $"Only generate dialogue for the following non-playable character. Do not produce any additional text or instructions. " +
                             $"Limit to {promptWordLimit} words. Generate unique and creative dialogue suited for a {genderString} " +
                             $"non-playable character in a {genreString} game. Use details about the current " +
                             $"emotional tone of {attitudeString} and context. Focus the dialogue on moving the gameplay narrative " +
                             $"forward by revealing interesting lore details, side quest opportunities, or " +
                             $"comments relating to the player's current objectives.";
                    break;
            case 3: prompt = $"Only generate dialogue for the following non-playable character. Do not produce any additional text or instructions. " +
                             $"Limit to {promptWordLimit} words. Generate unique and creative dialogue suited for a {genderString} " +
                             $"non-playable character in a {genreString} game.";
                     break;
            default: prompt = "Tell me an interesting fact"; 
                     break;
        }
        Debug.Log("The prompt is: \r\n" + prompt);

        return prompt;
    }

}