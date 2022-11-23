using System;
namespace ColdBanana_UmbracoTest.Models
{
	public class EventModel
	{
		public EventModel()
		{
            FullName = string.Empty;
            Email = string.Empty;
            Event = string.Empty;
            Tickets = 0;
		}

        public string FullName { get; set; }
        public string Email { get; set; }

        public string Event { get; set; }
        public int Tickets { get; set; }



    }
}

