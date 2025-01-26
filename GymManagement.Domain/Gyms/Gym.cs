using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Domain.Gyms
{
    public class Gym
    {
        private readonly int _maxRooms;
        public Guid Id { get;  }
        public string Name { get; init; }
        public Guid SubscriptionId { get; init; }

        public Gym(string name, 
            int maxRooms,
            Guid subscriptionId,
            Guid? id=null)
        {
            Name = name;
            _maxRooms = maxRooms;
            SubscriptionId = subscriptionId;
            Id = id ?? Guid.NewGuid();
        }

        private Gym()
        {

        }
    }
}
