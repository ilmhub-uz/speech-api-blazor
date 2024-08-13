using Microsoft.JSInterop;

namespace Ilmhub.WebSpeechApi.Blazor;

public class SpeechApi(IJSRuntime jsRuntime) : ISpeechApi
{
    public event EventHandler<RecognitionResultEventArgs>? OnSpeechRecognized;
    public event EventHandler<string>? OnSpeechSynthesisCompleted;
    public event EventHandler<Exception>? OnError;

    public async void StartRecognition(RecognitionOptions options)
    {
        try
        {
            await jsRuntime.InvokeVoidAsync("startRecognition", DotNetObjectReference.Create(this), options);
        }
        catch (Exception ex)
        {
            OnError?.Invoke(this, ex);
        }
    }

    public async void StopRecognition()
    {
        try
        {
            await jsRuntime.InvokeVoidAsync("stopRecognition", DotNetObjectReference.Create(this));
        }
        catch (Exception ex)
        {
            OnError?.Invoke(this, ex);
        }
    }

    public async void StartSpeechSynthesis(string text, SynthesisOptions options)
    {
        try
        {
            await jsRuntime.InvokeVoidAsync("startSpeechSynthesis", DotNetObjectReference.Create(this), text, options);
        }
        catch (Exception ex)
        {
            OnError?.Invoke(this, ex);
        }
    }

    public async void StopSpeechSynthesis()
    {
        try
        {
            await jsRuntime.InvokeVoidAsync("stopSpeechSynthesis", DotNetObjectReference.Create(this));
        }
        catch (Exception ex)
        {
            OnError?.Invoke(this, ex);
        }
    }

    [JSInvokable]
    public void OnRecognitionResult(string result, bool isFinal)
    {
        OnSpeechRecognized?.Invoke(this, new RecognitionResultEventArgs { Result = result, IsFinal = isFinal });
    }

    [JSInvokable]
    public void OnSynthesisCompleted(string result)
    {
        OnSpeechSynthesisCompleted?.Invoke(this, result);
    }
}