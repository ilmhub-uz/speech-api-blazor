namespace Ilmhub.WebSpeechApi.Blazor;

public interface ISpeechApi
{
    void StartRecognition(RecognitionOptions options);
    void StopRecognition();
    void StartSpeechSynthesis(string text, SynthesisOptions options);
    void StopSpeechSynthesis();
    event EventHandler<RecognitionResultEventArgs> OnSpeechRecognized;
    event EventHandler<string> OnSpeechSynthesisCompleted;
    event EventHandler<Exception> OnError;
}

