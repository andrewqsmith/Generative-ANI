# Generative-ANI

Generative-ANI is a project that aims to integrate advanced language models into a Unity-based game to generate dynamic non-player character (NPC) dialogues and state attribute updates. The project utilizes the LLamaSharp library, the LLama 2 LLM 7b model, UniTask, and NuGetForUnity.

## Technical Approach

1. **LLamaSharp**: This project uses the [LLamaSharp](https://github.com/SciSharp/LLamaSharp) library to run LLaMA/GPT models in C# within the Unity environment. This library provides higher-level APIs for inferring LLaMA models and deploying them on local devices with C#. It is compatible with Windows, Linux, and macOS and can be easily integrated with Unity.

2. **LLama 2 LLM 7b**: The [LLama 2 LLM 7b](https://huggingface.co/TheBloke/llama-2-7B-Guanaco-QLoRA-GGUF/blob/main/llama-2-7b-guanaco-qlora.Q4_K_M.gguf) model is used as the primary LLM for the project. This model, available on Hugging Face, is a variant of the LLaMA model fine-tuned for question-answering tasks. It can be used to generate contextually relevant NPC responses and dynamically update dialogues based on the game state.

3. **UniTask**: The project integrates [Cysharp/UniTask](https://github.com/Cysharp/UniTask) to provide efficient, allocation-free async/await integration for Unity. UniTask enables better performance and reduced overhead for asynchronous programming in Unity, which is essential for managing the dynamic generation of NPC dialogues and state attribute updates.

4. **NuGetForUnity**: The project uses [NuGetForUnity](https://github.com/GlitchEnzo/NuGetForUnity) to manage and install the required packages, such as LLamaSharp and UniTask, into the Unity project. NuGetForUnity is a Unity extension that allows developers to easily install and update NuGet packages within the Unity environment.

## Getting Started

To integrate these libraries and the LLM into your Unity project, follow these steps:

1. Install NuGetForUnity [GitHub repository](https://github.com/GlitchEnzo/NuGetForUnity) via Unity Package Manager.

2. Install UniTask [GitHub repository](https://github.com/Cysharp/UniTask) via Unity Package Manager

3. Use NuGetForUnity to search for and install the LLamaSharp package. This will automatically download and integrate the LLamaSharp library and its dependencies into your Unity project.

4. Download the LLama 2 LLM 7b model from the provided Hugging Face link and integrate it into your project by placing the file into the Assets/Model directory. Please note you can use any other GGUF LLM by just updating the ModelFileName in DirectChat.cs and DialogueGeneration.cs.

5. This project includes libllama.dll for CPU (v0.8.0). You may manually change it to a newer CPU version or a CUDA version by downloading one of the below packages and replacing the current file:
   - [LLAMASharp.Backend.CPU](https://www.nuget.org/packages/LLAMASharp.Backend.CPU/)
   - [LLAMASharp.Backend.CUDA11](https://www.nuget.org/packages/LLAMASharp.Backend.CUDA11/)
   - [LLAMASharp.Backend.CUDA12](https://www.nuget.org/packages/LLAMASharp.Backend.CUDA12/)

## Unity Package

## Using the Unity Package in the Repo

The repository includes a Unity package that you can import into your Unity project. This package all of the above for ease of use. Here's how you can import it:

1. Download the Unity package from the Generative-ANI repository. The package file will have a `.unitypackage` extension.

2. Open your Unity project where you want to import the package.

3. Go to the Unity Editor, click on `Assets` in the menu bar, and then select `Import Package > Custom Package...`.

4. In the file dialog that opens, navigate to the location where you downloaded the Unity package, select it, and click `Open`.

5. A new window will open in Unity showing you all the items that are included in the package. By default, all items are selected for import. If you only want to import specific items, you can deselect the ones you don't need.

6. Click on the `Import` button to import the selected items into your Unity project.

After the import is complete, the assets and scripts from the Unity package will be available in your project's `Assets` folder. You can now use these in your project as per your requirements.

## Feedback

I read every piece of feedback and take your input very seriously. Please feel free to open an issue or submit a pull request.

## License

This project is licensed under the MIT License.
