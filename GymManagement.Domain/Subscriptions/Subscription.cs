using ErrorOr;
using GymManagement.Domain.Gyms;

namespace GymManagement.Domain.Subscriptions
{
    public class Subscription
    {
        private readonly List<Guid> _gymIds = new();
        private readonly int _maxGyms;
        private readonly Guid _adminId;
        public Guid Id { get; private set;}

        public SubscriptionType SubscriptionType { get; private set; } 

        public Subscription(SubscriptionType subscriptionType,
        Guid adminId,
        Guid? id=null)
        {
            SubscriptionType = subscriptionType;
            _adminId= adminId;
            Id = id?? Guid.NewGuid();
            _maxGyms = GetMaxGyms();
        }

        public int GetMaxGyms()

        => SubscriptionType.Name switch
        {
            nameof(SubscriptionType.Free) => 1,
            nameof(SubscriptionType.Starter) => 1,
            nameof(SubscriptionType.Pro) => 3,
            _ => throw new InvalidOperationException()
        };
        public int GetMaxRooms()
        
        =>  SubscriptionType.Name switch
        {
            nameof(SubscriptionType.Free) => 1,
            nameof(SubscriptionType.Starter) => 3,
            nameof(SubscriptionType.Pro) => int.MaxValue,
            _ => throw new InvalidOperationException()
        };

        public ErrorOr<Success> AddGym(Gym gym)
        {
            if (_gymIds.Contains(gym.Id))
            {
                throw new InvalidOperationException();
            }

            if (_gymIds.Count >= _maxGyms)
            {
                return SubscriptionErrors.CannotHaveMoreGymsThanSubscriptionAllows;
            }
            _gymIds.Add(gym.Id);
            return Result.Success;
        }
        
        private Subscription()
        {

        }
    }
}