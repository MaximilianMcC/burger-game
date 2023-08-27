using System.Text.Json;

public static class Dialogue
{
	public static DialogueLines dialogueLines { get; private set; }

	public static void LoadDialogue()
	{
		// Get the ingredients JSON file
		string dialogueJsonPath = "./assets/dialogue.json";
		string dialogueJson = File.ReadAllText(dialogueJsonPath);

		// Parse the JSON and get the dialogue lines from it
		dialogueLines = JsonSerializer.Deserialize<DialogueLines>(dialogueJson);
	}
}


public class DialogueLines
{
	public string template1Item { get; set; }
	public List<string> greetings { get; set; }
	public List<string> want { get; set; }
	public List<string> end { get; set; }
}