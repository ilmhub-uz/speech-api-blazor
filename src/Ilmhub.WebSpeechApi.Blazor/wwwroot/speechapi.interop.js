var SpeechRecognition = SpeechRecognition || window.webkitSpeechRecognition;
var SpeechGrammarList = SpeechGrammarList || window.webkitSpeechGrammarList;
var SpeechGrammar = SpeechGrammar || window.webkitSpeechGrammar;
var SpeechRecognitionEvent = SpeechRecognitionEvent || webkitSpeechRecognitionEvent;

let recognition;
let synthesis;


function isSpeechSynthesisSupported() {
    return 'speechSynthesis' in window;
}

function startRecognition(dotnetObject, options) {
    if (!SpeechRecognition) {
        console.warn('Speech Recognition is not supported in this browser.');
        return;
    }

    console.log(options);

    console.log('Starting speech recognition.');

    recognition = new SpeechRecognition();
    recognition.continuous = options.continuous || false;
    recognition.interimResults = options.interimResults || false;
    recognition.lang = options.language || 'en-US';
    recognition.maxAlternatives = options.maxAlternatives || 1;

    if (options.grammars && options.grammars.length > 0 && SpeechGrammarList) {
        let grammarList = new SpeechGrammarList();
        options.grammars.forEach(grammar => {
            console.log(grammar.grammar);
            grammarList.addFromString(grammar.grammar, grammar.weight);
        });
        recognition.grammars = grammarList;
    }

    recognition.onresult = (event) => {
        let result = event.results[event.results.length - 1][0].transcript;
        let isFinal = event.results[0].isFinal;

        console.log(event.results);

        console.log(`recoginition result ${result}. IsFinal = ${isFinal}. Confidence = ${event.results[0][0].confidence}`);
        dotnetObject.invokeMethodAsync('OnRecognitionResult', result, isFinal);
    };
    
    recognition.onspeechend = () => recognition.stop();
    recognition.onnomatch = () => console.log('No match found.');
    recognition.onerror = (event) => console.error('Recognition error:', event.error);

    recognition.start();
}

function stopRecognition(dotnetObject) {
    if (recognition) {
        recognition.stop();
    }
}

function startSpeechSynthesis(dotnetObject, text, options) {
    if (!isSpeechSynthesisSupported()) {
        console.warn('Speech Synthesis is not supported in this browser.');
        return;
    }

    synthesis = new SpeechSynthesisUtterance(text);
    synthesis.voice = options.voice ? window.speechSynthesis.getVoices().find(v => v.name === options.voice) : null;
    synthesis.volume = options.volume || 1;
    synthesis.rate = options.rate || 1;
    synthesis.pitch = options.pitch || 1;

    synthesis.onend = () => dotnetObject.invokeMethodAsync('Ilmhub.WebSpeechApi.Blazor', 'OnSynthesisCompleted', 'Completed');
    synthesis.onerror = (event) => console.error('Synthesis error:', event.error);
    window.speechSynthesis.speak(synthesis);
}

function stopSpeechSynthesis(dotnetObject) {
    if (synthesis) {
        window.speechSynthesis.cancel();
    }
}
