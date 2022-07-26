using System.Speech.Synthesis;
using System.Speech.Recognition;
using System.Diagnostics;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

SpeechSynthesizer speaker = new SpeechSynthesizer();
speaker.SelectVoice("Microsoft Zira Desktop"); //David, Zira, Hanhan, Huihui
SpeechRecognitionEngine recognizer = new SpeechRecognitionEngine();
Choices choice_01 = new Choices(new string[] { "謝謝", "哈咯", "現在幾點", "我是敬祐", "我好累", "講台語", "看影片", "谷歌搜尋", "今晚我想來點", "再見" });
GrammarBuilder grammar_builder = new GrammarBuilder();
grammar_builder.Append(choice_01);
grammar_builder.Culture = recognizer.RecognizerInfo.Culture;
Grammar grammar = new Grammar(grammar_builder);
recognizer.LoadGrammar(grammar);
recognizer.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(recognizer_SpeechRecognized);
recognizer.SetInputToDefaultAudioDevice();
recognizer.RecognizeAsync(RecognizeMode.Multiple);

while (true)
{
    Console.ReadLine();
}

void recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
{
    string commandName = e.Result.Text;
    Console.WriteLine("Recognized text: " + commandName);
    bool a = (commandName == "我好累");
    switch (commandName.ToLower())
    {
        case "哈咯":
            speaker.SpeakAsync("hello, how are you doing?");
            break;
        case "謝謝":
            speaker.SpeakAsync("you're welcome");
            break;
        case "我是敬祐":
            speaker.SpeakAsync("hello, Jing You.");
            break;
        case "講台語":
            speaker.SelectVoice("Microsoft Hanhan Desktop");
            speaker.SpeakAsync("利洗累供沙小");
            break;
        case "我好累":
            speaker.SpeakAsync("啊你是在累什麼");
            break;
        case "現在幾點":
            speaker.SpeakAsync(DateTime.Now.ToShortTimeString());
            break;
        case "看影片":
            var ps = new ProcessStartInfo("https://youtube.com")
            {
                UseShellExecute = true,
                Verb = "open"
            };
            Process.Start(ps);
            break;
        case "谷歌搜尋":
            var ps2 = new ProcessStartInfo("http://www.google.com")
            {
                UseShellExecute = true,
                Verb = "open"
            };
            Process.Start(ps2);
            break;

        case "今晚我想來點":
            var ps3 = new ProcessStartInfo("https://ubereats.com")
            {
                UseShellExecute = true,
                Verb = "open"
            };
            Process.Start(ps3);
            break;
        case "再見":
            speaker.Speak("慢走不送");
            System.Threading.Thread.Sleep(3);
            Environment.Exit(0);
            break;
        default:
            break;
    }
}
