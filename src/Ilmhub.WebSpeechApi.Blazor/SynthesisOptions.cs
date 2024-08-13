namespace Ilmhub.WebSpeechApi.Blazor;

public class SynthesisOptions
{
    public string? Voice { get; set; }
    public double Volume { get; set; } = 1.0;
    public double Rate { get; set; } = 1.0;
    public double Pitch { get; set; } = 1.0;
}

