namespace Ilmhub.WebSpeechApi.Blazor;

public class RecognitionOptions
{
    public bool Continuous { get; set; }
    public bool InterimResults { get; set; }
    public int MaxAlternatives { get; set; } = 1;
    public string Language { get; set; } = "en-US";
    public List<SpeechGrammar> Grammars { get; set; } = [];
}

