namespace WebHookService.Models.Events
{
	public class WebhookEvent
	{
		public WebhookEvent(string secret, Event eventType)
		{
			Secret = secret;
			EventType = eventType;
		}

		public string Secret { get; set; }
		public Event EventType { get; set; }
	}
}
