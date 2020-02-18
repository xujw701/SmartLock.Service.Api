using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartELock.Core.Domain.Models.Commands
{
    public class FeedbackCreateCommand : ICommand
    {
        public int UserId { get; set; }
        public string Content { get; set; }
    }
}
