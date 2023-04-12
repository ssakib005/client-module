using Authentication.Core.Interfaces;
using Authentication.Core.Models;
using System.Security.Claims;




namespace AuthenticationWebapi.Authentication
{
	public class SessionService : ISessionService
	{
		private readonly ILogger _logger;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public SessionService(ILogger<SessionService> logger, IHttpContextAccessor httpContextAccessor)
		{
			_logger = logger;
			_httpContextAccessor = httpContextAccessor;
		}


		public string GetUserId()
		{
			var userId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(CustomClaimTypes.UserId);
			if (!string.IsNullOrEmpty(userId))
				return userId;

			_logger.LogWarning($"Unable to retrieve [{CustomClaimTypes.UserId}] value from HTTP context.");
			return string.Empty;
		}
	}
}
