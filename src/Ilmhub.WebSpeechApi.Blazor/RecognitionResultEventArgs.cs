namespace Ilmhub.WebSpeechApi.Blazor;

public class RecognitionResultEventArgs : EventArgs
{
    public string? Result { get; set; }
    public bool IsFinal { get; set; }
}