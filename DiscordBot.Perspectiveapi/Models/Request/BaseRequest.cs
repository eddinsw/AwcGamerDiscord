namespace DiscordBot.Perspectiveapi.Models.Request
{
    internal class BaseRequest
    {
        public Comment comment { get; set; }
        public RequestedAttributes requestedAttributes { get; set; }

        public BaseRequest(string msg)
        {
            comment = new Comment { text = msg };
            requestedAttributes = new RequestedAttributes();
        }
    }
}