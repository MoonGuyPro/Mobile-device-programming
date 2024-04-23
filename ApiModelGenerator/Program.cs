using NJsonSchema;
using NJsonSchema.CodeGeneration.CSharp;
using ClientApi;

namespace ApiModelGenerator;

internal static class Program
{
	//mozna zrobic to w testach zamiast tutaj w exe ale to dziala wiec po co psuc
	private static async Task Main(string[] args)
	{
		Console.WriteLine("JSON Schema: \n\n");
		JsonSchema schema = JsonSchema.FromType<GetCandidatesCommand>();
		string schemaString = schema.ToJson();

		Console.WriteLine(schemaString);

		Console.WriteLine("C#: \n\n");

		JsonSchema loadedSchema = await JsonSchema.FromJsonAsync(schemaString);
		CSharpGenerator csharpGenerator = new CSharpGenerator(loadedSchema);
		Console.WriteLine(csharpGenerator.GenerateFile());
	}
}
