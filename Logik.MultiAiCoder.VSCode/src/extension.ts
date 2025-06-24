import * as vscode from 'vscode';

interface PromptConfiguration {
    provider: string;
    model: string;
    apiKey: string;
    order: number;
}

export function activate(context: vscode.ExtensionContext) {
    const existing = context.globalState.get<PromptConfiguration[]>('promptConfigs');
    if (!existing || existing.length === 0) {
        const defaults: PromptConfiguration[] = [
            { provider: 'OpenAI', model: 'gpt-4o', apiKey: '', order: 0 },
            { provider: 'Claude', model: '3-opus', apiKey: '', order: 1 },
            { provider: 'Gemini', model: '1.5-pro', apiKey: '', order: 2 },
            { provider: 'AzureOpenAI', model: 'gpt-4o', apiKey: '', order: 3 }
        ];
        context.globalState.update('promptConfigs', defaults);
    }

    const disposable = vscode.commands.registerCommand('logik-multiaicoder.start', () => {
        vscode.window.showInformationMessage('Multi AI Coder coming soon');
    });
    context.subscriptions.push(disposable);
}

export function deactivate() {}
