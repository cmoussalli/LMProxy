namespace MLProxy.DTOs
{
    public class OllamaDTOs
    {
        public class OllamaChatRequest
        {
            public string Model { get; set; }
            public List<Message> Messages { get; set; } = new();
            public bool Stream { get; set; } = true;
            public Dictionary<string, object>? Options { get; set; }
            public string? Format { get; set; }
            public string? Template { get; set; }
            public string? Context { get; set; }
            public List<string>? Images { get; set; }
        }

        public class Message
        {
            public string Role { get; set; } = "user";
            public string Content { get; set; } = "";
        }

    }
}
