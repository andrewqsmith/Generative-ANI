using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LLama.Common;
using LLama;

public class DialogueGeneration : MonoBehaviour
{
    public string prompt;
    public Dropdown genderDropdown;
    private ChatSession session;
    private string modelPath = Application.dataPath + "/Model/YourModelFileName"; // replace with your model file

    // Start is called before the first frame update
    void Start()
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

    // Update is called once per frame
    void Update()
    {
        // Use the prompt and selected gender for your dialogue system
        Debug.Log("Prompt: " + prompt);
        Debug.Log("Selected Gender: " + genderDropdown.options[genderDropdown.value].text);

        // Use the chat session
        foreach (var text in session.Chat(prompt, new InferenceParams() { Temperature = 0.6f, AntiPrompts = new List<string> { "User:" } }))
        {
            Debug.Log(text);
        }
    }
}