import * as vscode from 'vscode';

export function activate(context: vscode.ExtensionContext) {
    const cmd = vscode.commands.registerCommand('logik-multiaicoder.start', () => {
        const panel = vscode.window.createWebviewPanel(
            'multiAiCoder',
            'Multi AI Coder',
            vscode.ViewColumn.One,
            {
                enableScripts: true
            }
        );

        panel.webview.html = getWebviewContent();

        panel.webview.onDidReceiveMessage(async msg => {
            if (msg.command === 'sendPrompt') {
                const prompt: string = msg.prompt;
                const providers: string[] = msg.providers;
                const results = providers.map(p => `${p}: ${prompt}`);
                panel.webview.postMessage({ command: 'results', results });
            }
        });
    });
    context.subscriptions.push(cmd);
}

export function deactivate() {}

function getWebviewContent(): string {
    return `<!DOCTYPE html>
<html lang="en">
<head>
<meta charset="UTF-8">
</head>
<body>
<input id="prompt" style="width:300px" placeholder="Enter your prompt" />
<div>
<label><input type="checkbox" id="openai" checked /> OpenAI</label>
<label><input type="checkbox" id="claude" checked /> Claude</label>
<label><input type="checkbox" id="gemini" checked /> Gemini</label>
<label><input type="checkbox" id="azure" checked /> Azure</label>
<button id="send">Send</button>
</div>
<div id="results"></div>
<script>
const vscode = acquireVsCodeApi();

const btn = document.getElementById('send');
btn.addEventListener('click', () => {
  const prompt = (document.getElementById('prompt')).value;
  const providers = [];
  if (document.getElementById('openai').checked) providers.push('OpenAI-gpt-4o');
  if (document.getElementById('claude').checked) providers.push('Claude-3-opus');
  if (document.getElementById('gemini').checked) providers.push('Gemini-1.5-pro');
  if (document.getElementById('azure').checked) providers.push('AzureOpenAI-gpt-4o');
  vscode.postMessage({ command: 'sendPrompt', prompt, providers });
});

window.addEventListener('message', event => {
  const message = event.data;
  if (message.command === 'results') {
    const div = document.getElementById('results');
    div.innerHTML = '';
    for (const r of message.results) {
      const pre = document.createElement('pre');
      pre.textContent = r;
      div.appendChild(pre);
    }
  }
});
</script>
</body>
</html>`;
}
