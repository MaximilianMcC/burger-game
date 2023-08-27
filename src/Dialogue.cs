using System.Text.Json;
using System.Text.Json.Serialization;

public static class Dialogue
{
	public static DialogueLines Lines { get; private set; }

	public static void LoadDialogue()
	{
		// Get the ingredients JSON file
		string dialogueJsonPath = "./assets/dialogue.json";
		string dialogueJson = File.ReadAllText(dialogueJsonPath);

		// Parse the JSON and get the dialogue lines from it
		Lines = JsonSerializer.Deserialize<DialogueLines>(dialogueJson);
	}
}


public class DialogueLines
{
	[JsonPropertyName("template1Item")] public string Template1Item { get; set; }
	[JsonPropertyName("greetings")] public List<string> Greetings { get; set; }
	[JsonPropertyName("want")] public List<string> Want { get; set; }
	[JsonPropertyName("with")] public List<string> With { get; set; }
	[JsonPropertyName("joiners")] public List<string> Joiners { get; set; }
	[JsonPropertyName("end")] public List<string> End { get; set; }
}