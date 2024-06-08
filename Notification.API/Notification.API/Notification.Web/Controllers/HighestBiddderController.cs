using Microsoft.AspNetCore.Mvc;
using Notification.API.Nofication.Core.Abstraction;

namespace Notification.API.Notification.Web.Controllers
{


    [ApiController]
    [Route("api/[controller]")]
    public class HighestBiddderController : ControllerBase
    {
       
        private readonly INotification_Publisher _notificationService;
        public HighestBiddderController(INotification_Publisher notification_Service)
        {
            _notificationService = notification_Service;
        }

        [HttpGet("GetHighestBidder")]
        public async Task<IActionResult> GetHighestBidderAsync()
        { 
        var result = await _notificationService.GetHighestBidderAsync();
        return Ok(result);
        
        }
    }
}
