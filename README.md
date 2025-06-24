# Logik Multi AI Coder

## English
Logik Multi AI Coder is a set of extensions for Visual Studio 2022 and VS Code allowing you to send the same prompt to several AI platforms (OpenAI, Claude, Gemini, Azure OpenAI) and view results side by side. Multiple models per provider are supported and stored locally. A configuration window lets you manage prompt setups, reorder them and validate API keys. Four default prompts (one per provider with the latest model) are created at first launch. Each provider/model can receive file context so responses take your code into account.

## Français
Logik Multi AI Coder est un ensemble d'extensions pour Visual Studio 2022 et VS Code permettant d'envoyer le même prompt à plusieurs plateformes d'IA (OpenAI, Claude, Gemini, Azure OpenAI) et d'afficher les réponses côte à côte. Plusieurs modèles par fournisseur sont pris en charge et stockés localement. Une fenêtre de configuration permet de gérer l'ordre des prompts et vérifie la clé API. Quatre prompts par défaut (un par fournisseur) sont créés au premier lancement. Chaque fournisseur peut recevoir le contexte des fichiers pour des réponses plus pertinentes.

## Español
Logik Multi AI Coder es un conjunto de extensiones para Visual Studio 2022 y VS Code que permite enviar el mismo prompt a varias plataformas de IA (OpenAI, Claude, Gemini, Azure OpenAI) y mostrar los resultados en paralelo. Se admiten varios modelos por proveedor y se almacenan localmente. Una ventana de configuración permite ordenar los prompts y valida la clave API. Cuatro configuraciones por defecto (una por proveedor) se crean al iniciar. Cada proveedor recibe el contexto de los archivos para mejorar las respuestas.

Use `Logik.MultiAiCoder.sln` to open the Visual Studio 2022 extension and shared
engine in one solution. For the VS Code extension, open the `logik-multi-ai-coder.code-workspace`
file with VS Code.

### Building
1. **Visual Studio**
   - Open `Logik.MultiAiCoder.sln` with Visual Studio 2022.
   - Build the solution. The VSIX project will output a `.vsix` file that can be
     installed or published to the Marketplace.

2. **VS Code**
   - Install Node.js 18+ and run `npm install` inside `Logik.MultiAiCoder.VSCode`.
   - Run `npm run compile` to produce the extension in the `dist` folder.
   - Package with `vsce package` for publishing.

Both extensions rely on the Engine library which contains the provider logic and
prompt dispatcher.
