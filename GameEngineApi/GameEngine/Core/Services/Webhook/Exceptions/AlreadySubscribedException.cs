namespace GameEngine.Core.Services.Webhook.Exceptions
{
	public class AlreadySubscribedException : Exception
	{
		public AlreadySubscribedException()
		{
			
		}

		public AlreadySubscribedException(string message) : base(message)
		{
			
		}
	}
}
