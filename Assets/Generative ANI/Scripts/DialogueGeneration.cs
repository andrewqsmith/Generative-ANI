using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LLama.Common;
using LLama;
using UnityEditorInternal;
using UnityEngine.Animations;
using System.Net.NetworkInformation;

public class DialogueGeneration : StateMachineBehaviour
{
    private string modelPath;
    private ChatSession session;

    public Dropdown genderDropdown;
    public string ModelFileName = "llama-2-7b-guanaco-qlora.Q4_K_M.gguf";
    public string prompt = "Tell me a story about a red dragon";
    public enum NPCState { Idle, Chatting, Walking, Other } // Define your NPC states here
    public NPCState currentState; // Current state of the NPC

    void Awake()
    {
        modelPath = Application.dataPath + "/Model/" + ModelFileName;
    }

    // Start is called before the first frame update
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Initialize the dropdown options
        genderDropdown.options.Add(new Dropdown.OptionData("Male"));
        genderDropdown.options.Add(new Dropdown.OptionData("Female"));
        genderDropdown.options.Add(new Dropdown.OptionData("Other"));

        // Set the default selected option
        genderDropdown.value = 0;

        // Initialize the chat session
        var parameters = new ModelParams(modelPath)
        {
            ContextSize = 1024,
            Seed = 1337,
            GpuLayerCount = 5
        };
        using var model = LLamaWeights.LoadFromFile(parameters);
        using var context = model.CreateContext(parameters);
        var ex = new InteractiveExecutor(context);
        session = new ChatSession(ex);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Use the prompt and selected gender for your dialogue system
        Debug.Log("Prompt: " + prompt);
        Debug.Log("Selected Gender: " + genderDropdown.options[genderDropdown.value].text);
        /*
        // Use the chat session
        foreach (var text in session.Chat(prompt, new InferenceParams() { Temperature = 0.6f, AntiPrompts = new List<string> { "User:" } }))
        {
            Debug.Log(text);
        }*/

        switch (currentState)
        {
            case NPCState.Idle:
                // Code for Idle state
                break;
            case NPCState.Chatting:
                // Code for Chatting state
                break;
            case NPCState.Walking:
                // Code for Walking state
                break;
            case NPCState.Other:
                // Code for Other state
                break;
        }

    }

    public override void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Get the next state name
        string nextStateName = animator.GetNextAnimatorStateInfo(layerIndex).fullPathHash.ToString();

        // Change the currentState variable based on the next state
        switch (nextStateName)
        {
            case "Idle":
                currentState = NPCState.Idle;
                break;
            case "Chatting":
                currentState = NPCState.Chatting;
                break;
            case "Walking":
                currentState = NPCState.Walking;
                break;
            case "Other":
                currentState = NPCState.Other;
                break;
        }

        // Handle transitions between states here
        Debug.Log("Moving to state: " + currentState);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Get the name of the state that just ended
        string stateName = stateInfo.IsName("Idle") ? "Idle" :
            stateInfo.IsName("Chatting") ? "Chatting" :
            stateInfo.IsName("Walking") ? "Walking" : "Other";

        // Log the exit of the state
        Debug.Log("Exiting state: " + stateName);

        // Reset the prompt
        prompt = "";
    }
}